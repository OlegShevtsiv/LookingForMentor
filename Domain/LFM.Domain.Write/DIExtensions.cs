using LFM.Core.Common.Command;
using LFM.Domain.Write.CommandHandlers.Auth;
using LFM.Domain.Write.Commands.Auth;
using LFM.Domain.Write.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace LFM.Domain.Write
{
    public static class DIExtensions
    {
        public static void AddCommands(this IServiceCollection services)
        {
            services.AddTransient<ICommandBus, CommandBus>();

            AddCommandHandlers(services);
        }

        private static void AddCommandHandlers(IServiceCollection services)
        {
            services.AddTransient<ICommandHandler<LoginUserCommand, CommandResult>, LoginUserCommandHandler>();
            services.AddTransient<ICommandHandler<LogoutUserCommand, CommandResult>, LogoutUserCommandHandler>();
        }
    }
}