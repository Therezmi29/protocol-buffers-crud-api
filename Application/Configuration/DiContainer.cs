using Application.IOC.Interface;
using Application.IOC.Service;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Configuration
{
    public static class DiContainer
    {
        public static void RegisterService(this IServiceCollection services)
        {
            services.AddScoped<UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
