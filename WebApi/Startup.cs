using Application.Services.ProductServices.GetProductService;
using Application.Services.ProductServices.GetProductService.Interface;
using Data.Repositories.Base;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using AutoMapper;
using Application.Mapping;
using Application.Services.ProductServices.CreateProductService.Interface;
using Application.Services.CreateProductService;
using Application.Services.ProductServices.UpdateProductService.Interface;
using Application.Services.ProductServices.UpdateProductService;
using Application.Services.ProductServices.DeleteProductService;
using Application.Services.ProductServices.DeleteProductService.Interface;
using Application.Services.SupplierServices.GetSupllierService;
using Application.Services.SupplierServices.GetSupllierService.Interface;
using Infrastructure.Repositories.SupplierRepository;
using Application.Services.SupplierServices.CreateSupllierService.Interface;
using Application.Services.SupplierServices.CreateSupllierService;
using Application.Services.SupplierServices.UpdateSupllierService.Interface;
using Application.Services.SupplierServices.UpdateSupllierService;

namespace GestaoProdutos
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GestaoProdutos", Version = "v1" });
            });
            // Configuração do AutoMapper
            AutoMapperConfiguration.Configure(services);


            // Registro de DbSession
            services.AddScoped<DbSession>();

            services.AddScoped<IGetProductService, GetProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICreateProductServices, CreateProductServices>();
            services.AddScoped<IUpdateProductService, UpdateProductService>();
            services.AddScoped<IDeleteProductService, DeleteProductService>();

            services.AddScoped<IGetSupllierService, GetSupllierService>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ICreateSupllierService, CreateSupllierService>();
            services.AddScoped<IUpdateSupllierService, UpdateSupllierService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GestaoProdutos v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
