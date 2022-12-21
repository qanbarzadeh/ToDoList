using Application.Tests;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DependencyProviderExtension
    {
        public static void AddTodoTaskServices(this IServiceCollection services)
        {
            services.AddScoped<ITodoTasksService, TodoTasksService>();
        }
    }
}
