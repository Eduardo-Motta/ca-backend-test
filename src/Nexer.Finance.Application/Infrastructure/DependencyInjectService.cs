using Microsoft.EntityFrameworkCore;
using Nexer.Finance.Domain.Commands.Customer;
using Nexer.Finance.Domain.Commands.Products;
using Nexer.Finance.Domain.Handlers.Customers;
using Nexer.Finance.Domain.Handlers.Products;
using Nexer.Finance.Domain.Repositories;
using Nexer.Finance.Domain.Services.Customers;
using Nexer.Finance.Domain.Services.Products;
using Nexer.Finance.Infrastructure.Context;
using Nexer.Finance.Infrastructure.Repositories;
using Shared.Handlers;

namespace Nexer.Finance.Application.Infrastructure
{
    public static class DependencyInjectService
    {
        public static void AddDependencyInject(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.RegisterRepository();
            services.RegisterService();
            services.RegisterHandle();
        }

        private static void RegisterRepository(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }

        private static void RegisterService(this IServiceCollection services)
        {
            services.AddScoped<ICreateCustomerService, CreateCustomerService>();
            services.AddScoped<IUpdateCustomerService, UpdateCustomerService>();
            services.AddScoped<IFindCustomerService, FindCustomerService>();

            services.AddScoped<ICreateProductService, CreateProductService>();
            services.AddScoped<IUpdateProductService, UpdateProductService>();
            services.AddScoped<IFindProductService, FindProductService>();
        }

        private static void RegisterHandle(this IServiceCollection services)
        {
            services.AddScoped<IHandler<FindCustomerByIdCommand>, FindCustomerHandle>();
            services.AddScoped<IHandler<FindAllCustomersCommand>, FindCustomerHandle>();
            services.AddScoped<IHandler<CreateCustomerCommand>, CreateCustomerHandle>();
            services.AddScoped<IHandler<UpdateCustomerCommand>, UpdateCustomerHandle>();

            services.AddScoped<IHandler<FindProductByIdCommand>, FindProductHandle>();
            services.AddScoped<IHandler<FindAllProductsCommand>, FindProductHandle>();
            services.AddScoped<IHandler<CreateProductCommand>, CreateProductHandle>();
            services.AddScoped<IHandler<UpdateProductCommand>, UpdateProductHandle>();
        }
    }
}
