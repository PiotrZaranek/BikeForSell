using BikeForSell.Domain.Interfaces;
using BikeForSell.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IBikeRepository, BikeRepository>();
            services.AddTransient<IProfileRepository, ProfileRepository>();
            return services;
        }
    }
}
