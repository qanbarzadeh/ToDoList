using Application;
using Application.Tests;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyProviderExtension
    {
        public static void AddTodoTaskRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITodoTaskRepository, TodoTaskRepository>();
        }
    }
}
