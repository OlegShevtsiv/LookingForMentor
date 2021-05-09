using System;
using System.Threading.Tasks;
using LFM.Core.Common.Exceptions;
using LFM.Domain.Write.Declarations;
using LFM.Domain.Write.Models;
using Microsoft.Extensions.DependencyInjection;

namespace LFM.Domain.Write.Mediator
{
    public class CommandBus : ICommandBus
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public async Task<TCommandResult> ExecuteCommand<TCommand, TCommandResult>(TCommand command) 
            where TCommand : ICommand 
            where TCommandResult : CommandResult
        {
            ICommandHandler<TCommand, TCommandResult> handler = this.RetrieveCommandHandler<TCommand, TCommandResult>();
            return await handler.ExecuteAsync(command);
        }
        
        protected virtual ICommandHandler<TCommand, TCommandResult> RetrieveCommandHandler<TCommand, TCommandResult>()
            where TCommand : ICommand
            where TCommandResult : CommandResult
        {
            ICommandHandler<TCommand, TCommandResult> handler = _serviceProvider.GetService<ICommandHandler<TCommand, TCommandResult>>();
            
            if (handler == null) 
                throw new Exception($"Command handler for {nameof(TCommand)} not found");

            return handler;
        }
    }
}