using LFM.Domain.Write.CommandHandlers.Auth;
using LFM.Domain.Write.CommandHandlers.MentorProfile;
using LFM.Domain.Write.Commands.Auth;
using LFM.Domain.Write.Commands.MentorProfile;
using LFM.Domain.Write.CommandServices.Auth;
using LFM.Domain.Write.Declarations;
using LFM.Domain.Write.Mediator;
using LFM.Domain.Write.Models;
using Microsoft.Extensions.DependencyInjection;

namespace LFM.Domain.Write
{
    public static class DIExtensions
    {
        public static void AddCommands(this IServiceCollection services)
        {
            services.AddTransient<ICommandBus, CommandBus>();

            AddCommandHandlers(services);
            AddCommandServices(services);
        }

        private static void AddCommandHandlers(IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<LoginUserCommand, CommandResult>, LoginUserCommandHandler>();
            services.AddScoped<ICommandHandler<LogoutUserCommand, CommandResult>, LogoutUserCommandHandler>();
            services.AddScoped<ICommandHandler<RegisterStudentCommand, CommandResult>, RegisterStudentCommandHandler>();
            services.AddScoped<ICommandHandler<RegisterMentorCommand, CommandResult>, RegisterMentorCommandHandler>();
            services.AddScoped<ICommandHandler<EditMentorProfileCommand, CommandResult>, EditMentorProfileCommandHandler>();

        }
        
        private static void AddCommandServices(IServiceCollection services)
        {
            services.AddScoped<RegistrationService>();
        }
    }
}