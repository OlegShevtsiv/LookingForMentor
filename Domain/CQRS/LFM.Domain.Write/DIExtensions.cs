using LFM.Domain.Write.CommandHandlers.Auth;
using LFM.Domain.Write.CommandHandlers.MentorProfile;
using LFM.Domain.Write.CommandHandlers.Order;
using LFM.Domain.Write.Commands.Auth;
using LFM.Domain.Write.Commands.MentorProfile;
using LFM.Domain.Write.Commands.Order;
using LFM.Domain.Write.CommandServices.Auth;
using LFM.Domain.Write.Declarations;
using LFM.Domain.Write.Mapper;
using LFM.Domain.Write.Mediator;
using LFM.Domain.Write.Models;
using Microsoft.Extensions.DependencyInjection;

namespace LFM.Domain.Write
{
    public static class DIExtensions
    {
        public static void AddCommands(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CommandToEntityMapperConfig));
            services.AddTransient<ICommandBus, CommandBus>();

            AddCommandHandlers(services);
            AddInternalServices(services);
        }

        private static void AddCommandHandlers(IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<LoginUserCommand, CommandResult>, LoginUserCommandHandler>();
            services.AddScoped<ICommandHandler<LogoutUserCommand, CommandResult>, LogoutUserCommandHandler>();
            services.AddScoped<ICommandHandler<RegisterStudentCommand, CommandResult>, RegisterStudentCommandHandler>();
            services.AddScoped<ICommandHandler<RegisterMentorCommand, CommandResult>, RegisterMentorCommandHandler>();
            services.AddScoped<ICommandHandler<EditMentorProfileCommand, CommandResult>, EditMentorProfileCommandHandler>();
            services.AddScoped<ICommandHandler<AddMentorSubjectCommand, CommandResult>, AddMentorSubjectCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteMentorSubjectCommand, CommandResult>, DeleteMentorSubjectCommandHandler>();
            services.AddScoped<ICommandHandler<EditMentorSubjectCommand, CommandResult>, EditMentorSubjectCommandHandler>();
            services.AddScoped<ICommandHandler<CreatePersonalOrderToMentorCommand, CreatePersonalOrderResult>, CreatePersonalOrderToMentorCommandHandler>();
            services.AddScoped<ICommandHandler<ApprovePersonalOrderCommand, CommandResult>, ApprovePersonalOrderCommandHandler>();
            services.AddScoped<ICommandHandler<RejectPersonalOrderCommand, CommandResult>, RejectPersonalOrderCommandHandler>();

        }
        
        private static void AddInternalServices(IServiceCollection services)
        {
            services.AddScoped<RegistrationService>();
        }
    }
}