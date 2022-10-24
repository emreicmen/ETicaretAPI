using Microsoft.EntityFrameworkCore;
using ETicaretAPI.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ETicaretAPI.Persistence.Repositories;
using ETicaretAPI.Application.Repositories;

namespace ETicaretAPI.Persistence
{
    public static class ServiceRegistration
    {
        //API kısmında IOC Container'ı eklediğimiz için bunu ekledik
        public static void AddPersistenceServices(this IServiceCollection services)
        {

            //Burada database'i çağıracağoız.Hanfgi DB'yi kullnacaksak onun kütüphanesini yüklememiz gerekir (Persistance katmanına) yoksa options. kısmında çıkmaz
            services.AddDbContext<EticaretAPIDbContext>(options => options.UseNpgsql(Configuration.ConnectionString),ServiceLifetime.Singleton);

            //Burdaki repository'leri IOC service'e eklemiş oluyoruz.İlk parametre istenen repository, ikincisi ise dönecek olan Repository türüdür.
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
                     
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
                     
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        }
    }
}
