using AutoMapper;
using DocumentGenerator.Common;
using DocumentGenerator.Common.Contracts;
using DocumentGenerator.Context;
using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Repositories.Contracts.ReadRepositories;
using DocumentGenerator.Repositories.Contracts.WriteRepositories;
using DocumentGenerator.Repositories.ReadRepositories;
using DocumentGenerator.Repositories.WriteRepositories;
using DocumentGenerator.Services;
using DocumentGenerator.Services.Contracts;
using DocumentGenerator.Services.Contracts.IServices;
using DocumentGenerator.Services.Infrastructure;
using DocumentGenerator.Services.Services;
using DocumentGenerator.Web.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DocumentGenerator.Web
{
    /// <summary>
    /// Класс программы
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Точка входа программы
        /// </summary>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var controllers = builder.Services.AddControllers(opt =>
            {
                opt.Filters.Add<DocumentGeneratorExceptionFilter>();
            });

            if (builder.Environment.EnvironmentName == "integration")
            {
                controllers.AddControllersAsServices();
            }

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                var baseDirectory = AppContext.BaseDirectory;
                c.IncludeXmlComments(Path.Combine(baseDirectory, "DocumentGenerator.Web.xml"));
                c.IncludeXmlComments(Path.Combine(baseDirectory, "DocumentGenerator.Entities.xml"));
            });

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<DocumentGeneratorContext>(options =>
                options.UseNpgsql(connectionString)
                    .LogTo(Console.WriteLine)
            );
            builder.Services.AddScoped<IReader>(x => x.GetRequiredService<DocumentGeneratorContext>());
            builder.Services.AddScoped<IWriter>(x => x.GetRequiredService<DocumentGeneratorContext>());
            builder.Services.AddScoped<IUnitOfWork>(x => x.GetRequiredService<DocumentGeneratorContext>());
            builder.Services.AddScoped<IProductServices, ProductServices>();
            builder.Services.AddScoped<IPartyServices, PartyServices>();
            builder.Services.AddScoped<IDocumentServices, DocumentServices>();
            builder.Services.AddScoped<IExcelServices, ExcelServices>();

            builder.Services.AddSingleton<IValidateService, ValidateService>();
            builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            builder.Services.AddSingleton<IVatRateProvider, VatRateProvider>();

            builder.Services.AddSingleton(_ =>
            {
                var mapConfig = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<ServiceProfile>();
                    cfg.AddProfile<ApiMapper>();
                });

                var mapper = mapConfig.CreateMapper();
                return mapper;
            });
            builder.Services.AddScoped<IProductReadRepository, ProductReadRepository>();
            builder.Services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            builder.Services.AddScoped<IPartyReadRepository, PartyReadRepository>();
            builder.Services.AddScoped<IPartyWriteRepository, PartyWriteRepository>();

            builder.Services.AddScoped<IDocumentProductWriteRepository, DocumentProductWriteRepository>();

            builder.Services.AddScoped<IDocumentReadRepository, DocumentReadRepository>();
            builder.Services.AddScoped<IDocumentWriteRepository, DocumentWriteRepository>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
