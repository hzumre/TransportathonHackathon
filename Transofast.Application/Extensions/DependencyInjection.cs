using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transofast.Application.Services.AppRoleService;
using Transofast.Application.Services.AppUserService;
using Transofast.Application.Services.CommentService;
using Transofast.Application.Services.CompanyService;
using Transofast.Application.Services.MessageService;
using Transofast.Application.Services.TransportVehicleService;
using Transofast.Application.Services.TrasportService;


namespace Transofast.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IAppRoleService, AppRoleService>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<ITransportService, TransportService>();
            services.AddScoped<ITransportVehicleService, TransportVehicleService>();
            return services; 
        }
    }
}
