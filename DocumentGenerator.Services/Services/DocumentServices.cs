using AutoMapper;
using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts.ReadRepositories;
using DocumentGenerator.Repositories.Contracts.WriteRepositories;
using DocumentGenerator.Services.Contracts.Exceptions;
using DocumentGenerator.Services.Contracts.IServices;
using DocumentGenerator.Services.Contracts.Models.Document;

namespace DocumentGenerator.Services
{

    /// <inheritdoc cref="IDocumentServices"/>
    public class DocumentServices : IDocumentServices
    {
        private readonly IDocumentReadRepository documentReadRepository;
        private readonly IDocumentWriteRepository documentWriteRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentServices(IDocumentReadRepository documentReadRepository,
            IDocumentWriteRepository documentWriteRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.documentReadRepository = documentReadRepository;
            this.documentWriteRepository = documentWriteRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        async Task<IReadOnlyCollection<DocumentModel>> IDocumentServices.GetAll(CancellationToken cancellationToken)
        {
            var items = await documentReadRepository.GetAll(cancellationToken);
            return mapper.Map<IReadOnlyCollection<DocumentModel>>(items);
        }

        async Task<DocumentModel> IDocumentServices.Create(DocumentCreateModel model, CancellationToken cancellationToken)
        {
            var result = new Document
            {
                Id = Guid.NewGuid(),
                //Name = model.Name,
                //Job = model.Job,
                //TaxId = model.TaxId,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow,
                DeletedAt = null,
            };
            documentWriteRepository.Add(result);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<DocumentModel>(result);
        }

        async Task<DocumentModel> IDocumentServices.Edit(Guid id, DocumentCreateModel model, CancellationToken cancellationToken)
        {
            var entity = await documentReadRepository.GetById(id, cancellationToken)
                ?? throw new DocumentGeneratorNotFoundException($"Не удалось найти сторону акта с идентификатором {id}");

            //entity.Name = model.Name;
            //entity.Job = model.Job;
            //entity.TaxId = model.TaxId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            documentWriteRepository.Edit(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<DocumentModel>(entity);
        }

        async Task IDocumentServices.Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await documentReadRepository.GetById(id, cancellationToken)
                ?? throw new DocumentGeneratorNotFoundException($"Не удалось найти сторону акта с идентификатором {id}");
            documentWriteRepository.Delete(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
