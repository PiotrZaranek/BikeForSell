using BikeForSell.Application.Interfaces;
using BikeForSell.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IBikeService, BikeService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IErrorService, ErrorService>();
            return services;
        }
    }
}
