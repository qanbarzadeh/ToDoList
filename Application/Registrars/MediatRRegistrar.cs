using Application.Handlers.CreateCommands;
using Application.Handlers.GetTasks;
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
            serviceCollection.AddMediatR(typeof(GetPendingTaskCommand).Assembly);
            serviceCollection.AddMediatR(typeof(GetOverDueTaskCommand).Assembly);
            serviceCollection.AddMediatR(typeof(CreateTaskCommand).Assembly); 
            //add the rest of MediatR requried classes 
        }                
    }
}
