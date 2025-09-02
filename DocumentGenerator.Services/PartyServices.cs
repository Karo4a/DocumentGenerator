using AutoMapper;
using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts.ReadRepositories;
using DocumentGenerator.Repositories.Contracts.WriteRepositories;
using DocumentGenerator.Services.Contracts.Exceptions;
using DocumentGenerator.Services.Contracts.Models.Party;

namespace DocumentGenerator.Services
{

    /// <inheritdoc cref="IPartyServices"/>
    public class PartyServices : IPartyServices
    {
        private readonly IPartyReadRepository partyReadRepository;
        private readonly IPartyWriteRepository partyWriteRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Конструктор
        /// </summary>
        public PartyServices(IPartyReadRepository partyReadRepository,
            IPartyWriteRepository partyWriteRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.partyReadRepository = partyReadRepository;
            this.partyWriteRepository = partyWriteRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        async Task<IReadOnlyCollection<PartyModel>> IPartyServices.GetAll(CancellationToken cancellationToken)
        {
            var items = await partyReadRepository.GetAll(cancellationToken);
            return mapper.Map<IReadOnlyCollection<PartyModel>>(items);
        }

        async Task<PartyModel> IPartyServices.Create(PartyCreateModel model, CancellationToken cancellationToken)
        {
            var result = new Party
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Job = model.Job,
                TaxId = model.TaxId,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow,
                DeletedAt = null,
            };
            partyWriteRepository.Add(result);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<PartyModel>(result);
        }

        async Task<PartyModel> IPartyServices.Edit(PartyModel model, CancellationToken cancellationToken)
        {
            var entity = await partyReadRepository.GetById(model.Id, cancellationToken);
            if (entity == null)
            {
                throw new DocumentGeneratorNotFoundException($"Не удалось найти товар с иденитификатором {model.Id}");
            }

            entity.Name = model.Name;
            entity.Job = model.Job;
            entity.TaxId = model.TaxId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            partyWriteRepository.Edit(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<PartyModel>(entity);
        }
    }
}
