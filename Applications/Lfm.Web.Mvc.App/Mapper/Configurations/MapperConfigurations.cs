using AutoMapper;
using LFM.Domain.Write.CommandHandlers.Auth;
using LFM.Domain.Write.Commands.Auth;
using Lfm.Web.Mvc.App.Models.ViewModels.Auth;

namespace Lfm.Web.Mvc.App.Mapper.Configurations
{
    internal class ViewModelsCommandsMapperConfigurations : Profile
    {
        public ViewModelsCommandsMapperConfigurations()
        {
            CreateAuthModelsMaps();
        }

        private void CreateAuthModelsMaps()
        {
            CreateMap<LoginVM, LoginUserCommand>()
                .ForMember(x => x.Email, o => o.MapFrom(p => p.Email))
                .ForMember(x => x.Password, o => o.MapFrom(p => p.Password))
                .ForMember(x => x.RememberMe, o => o.MapFrom(p => p.RememberMe));
            
            CreateMap<RegisterMentorVM, RegisterMentorCommand>()
                .ForMember(x => x.Name, o => o.MapFrom(p => p.Name))
                .ForMember(x => x.Surname, o => o.MapFrom(p => p.Surname))
                .ForMember(x => x.MiddleName, o => o.MapFrom(p => p.MiddleName))
                .ForMember(x => x.Email, o => o.MapFrom(p => p.Email))
                .ForMember(x => x.Password, o => o.MapFrom(p => p.Password));
            
            CreateMap<RegisterStudentVM, RegisterStudentCommand>()
                .ForMember(x => x.Name, o => o.MapFrom(p => p.Name))
                .ForMember(x => x.PhoneNumber, o => o.MapFrom(p => p.PhoneNumber))
                .ForMember(x => x.Password, o => o.MapFrom(p => p.Password));
        }
    }
}