using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TestFramework.Application;
using TestFramework.Application.BackgroundTasks;
using TestFramework.Application.Handler;
using TestFramework.Domain.UITesting.Template;
using TestFramework.Infrastructure.BackgroundTasks;

namespace TestFramework.WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHandlers(this IServiceCollection services)
        {
            var handler = ReflectiveEnumerator.GetSubClassTypes(typeof(AbstractHandler<,,>))
                .ToList();
                
            handler.ForEach(x => services.AddTransient(x));

            services.AddTransient(typeof(DeleteHandler<>), typeof(DeleteHandler<>));
        }
    }
}
