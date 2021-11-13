using System;
using System.Threading.Tasks;
using LFM.Core.Common.Data;
using LFM.Core.Common.Exceptions;
using Lfm.Domain.Common.Extensions;
using LFM.Domain.Write.Declarations;
using LFM.Domain.Write.ResultModels;
using LFM.Domain.Write.ToDo;
using LFM.Domain.Write.ToDo.CreateService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LFM.Domain.Write.Mediator
{
    internal sealed class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TCommandResult> ExecuteCommand<TCommand, TCommandResult>(TCommand command) 
            where TCommand : ICommand, new()
            where TCommandResult : CommandResult
        {
            if (command is NeedsApproveCommand)
                throw new LfmException(Messages.SystemError);

            ICommandHandler<TCommand, TCommandResult> handler = this.RetrieveCommandHandler<TCommand, TCommandResult>();
            return await handler.ExecuteAsync(command);
        }

        public async Task CreateToDo<TCommand>(TCommand command)
            where TCommand : NeedsApproveCommand, new()
        {
            var createToDoService = _serviceProvider.GetRequiredService<ICreateToDoService>();
            int userId = _serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext.User.GetId();
            
            await createToDoService.CreateToDo(command, userId, command.Operation.Id);
        }

        private ICommandHandler<TCommand, TCommandResult> RetrieveCommandHandler<TCommand, TCommandResult>()
            where TCommand : ICommand, new()
            where TCommandResult : CommandResult
        {
            ICommandHandler<TCommand, TCommandResult> handler = _serviceProvider.GetService<ICommandHandler<TCommand, TCommandResult>>();
            
            if (handler == null) 
                throw new Exception($"Command handler for {nameof(TCommand)} not found");

            return handler;
        }
    }
}