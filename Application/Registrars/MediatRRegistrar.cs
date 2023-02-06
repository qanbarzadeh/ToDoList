using Application.Handlers.CreateCommands;
using Application.Handlers.GetTasks;
using Application.Handlers.Update;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Registrars
{
    public static class MediatRRegistrar
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(typeof(GetPendingTaskQuery).Assembly);
            serviceCollection.AddMediatR(typeof(GetOverDueTaskQuery).Assembly);
            serviceCollection.AddMediatR(typeof(CreateTaskCommand).Assembly);
            serviceCollection.AddMediatR(typeof(UpdateTaskCommand).Assembly);
            
        }                
    }
}
