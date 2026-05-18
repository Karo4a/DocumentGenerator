using AutoMapper;
using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts.ReadRepositories;
using DocumentGenerator.Repositories.Contracts.WriteRepositories;
using DocumentGenerator.Services.Contracts.Exceptions;
using DocumentGenerator.Services.Contracts.Models.Party;
using DocumentGenerator.Services.Contracts.IServices;

namespace DocumentGenerator.Services;


/// <inheritdoc cref="IPartyServices"/>
public class PartyService : IPartyServices, IServiceAnchor
{
    private readonly IPartyReadRepository partyReadRepository;
    private readonly IPartyWriteRepository partyWriteRepository;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    /// <summary>
    /// Конструктор
    /// </summary>
    public PartyService(IPartyReadRepository partyReadRepository,
        IPartyWriteRepository partyWriteRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        this.partyReadRepository = partyReadRepository;
        this.partyWriteRepository = partyWriteRepository;
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    async Task<PartyModel> IPartyServices.GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await partyReadRepository.GetById(id, cancellationToken)
            ?? throw new DocumentGeneratorNotFoundException($"Не удалось найти сторону акта с идентификатором {id}");
        return mapper.Map<PartyModel>(item);
    }

    async Task<IReadOnlyCollection<PartyModel>> IPartyServices.GetAll(CancellationToken cancellationToken)
    {
        var items = await partyReadRepository.GetAll(cancellationToken);
        return mapper.Map<IReadOnlyCollection<PartyModel>>(items);
    }

    async Task<PartyModel> IPartyServices.Create(PartyCreateModel model, CancellationToken cancellationToken)
    {
        if (await partyReadRepository.Any(x => x.TaxId == model.TaxId, cancellationToken))
            throw new DocumentGeneratorDuplicateException($"Сторона акта с ИНН {model.TaxId} уже существует");

            var result = new Party
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Job = model.Job,
                TaxId = model.TaxId,
            };
        partyWriteRepository.Add(result);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<PartyModel>(result);
    }

    async Task<PartyModel> IPartyServices.Edit(Guid id, PartyCreateModel model, CancellationToken cancellationToken)
    {
        if (await partyReadRepository.Any(x => x.TaxId == model.TaxId && x.Id != id, cancellationToken))
            throw new DocumentGeneratorDuplicateException($"Сторона акта с ИНН {model.TaxId} уже существует");

        var entity = await partyReadRepository.GetById(id, cancellationToken)
            ?? throw new DocumentGeneratorNotFoundException($"Не удалось найти сторону акта с идентификатором {id}");

        entity.Name = model.Name;
        entity.Job = model.Job;
        entity.TaxId = model.TaxId;
        entity.UpdatedAt = DateTimeOffset.UtcNow;

        partyWriteRepository.Edit(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<PartyModel>(entity);
    }

    async Task IPartyServices.Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await partyReadRepository.GetById(id, cancellationToken)
            ?? throw new DocumentGeneratorNotFoundException($"Не удалось найти сторону акта с идентификатором {id}");
        partyWriteRepository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
