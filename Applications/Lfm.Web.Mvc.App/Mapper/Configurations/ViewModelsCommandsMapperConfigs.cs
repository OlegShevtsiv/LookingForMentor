using AutoMapper;
using LFM.DataAccess.DB.Core.Entities.MentorEntities;
using LFM.Domain.Write.Commands.Auth;
using LFM.Domain.Write.Commands.MentorProfile;
using Lfm.Web.Mvc.Models.ViewsModels.Auth;
using Lfm.Web.Mvc.Models.ViewsModels.UserCabinet.Mentor;

namespace Lfm.Web.Mvc.App.Mapper.Configurations
{
    internal class ViewModelsCommandsMapperConfigs : Profile
    {
        public ViewModelsCommandsMapperConfigs()
        {
            CreateAuthModelsMaps();
            CreateMentorProfileModelsMaps();
        }

        private void CreateAuthModelsMaps()
        {
            CreateMap<LoginVM, LoginUserCommand>()
                .ForMember(x => x.Email, o => o.MapFrom(p => p.Email))
                .ForMember(x => x.Password, o => o.MapFrom(p => p.Password))
                .ForMember(x => x.RememberMe, o => o.MapFrom(p => p.RememberMe));
            
            CreateMap<RegisterMentorVM, RegisterMentorCommand>()
                .ForMember(x => x.Name, o => o.MapFrom(p => p.Name))
                .ForMember(x => x.Email, o => o.MapFrom(p => p.Email))
                .ForMember(x => x.PhoneNumber, o => o.MapFrom(p => p.PhoneNumber))
                .ForMember(x => x.Password, o => o.MapFrom(p => p.Password));
            
            CreateMap<RegisterStudentVM, RegisterStudentCommand>()
                .ForMember(x => x.Name, o => o.MapFrom(p => p.Name))
                .ForMember(x => x.Email, o => o.MapFrom(p => p.Email))
                .ForMember(x => x.PhoneNumber, o => o.MapFrom(p => p.PhoneNumber))
                .ForMember(x => x.Password, o => o.MapFrom(p => p.Password));
        }
        
        private void CreateMentorProfileModelsMaps()
        {
            CreateMap<MentorsProfile, EditMentorsProfileVM>()
                .ForMember(x => x.Name, o => o.Ignore())
                .ForMember(x => x.Surname, o => o.MapFrom(p => p.Surname))
                .ForMember(x => x.MiddleName, o => o.MapFrom(p => p.MiddleName))
                .ForMember(x => x.ProfileImageId, o => o.MapFrom(p => p.ProfileImageId))
                .ForMember(x => x.ProfileImageFormFile, o => o.Ignore())
                .ForMember(x => x.AboutMe, o => o.MapFrom(p => p.AboutMe))
                .ForMember(x => x.LocationInfo, o => o.MapFrom(p => p.LocationInfo))
                .ForMember(x => x.StudyingPlace, o => o.MapFrom(p => p.StudyingPlace))
                .ForMember(x => x.Education, o => o.MapFrom(p => p.Education));
            
            CreateMap<EditMentorsProfileVM, EditMentorProfileCommand>()
                .ForMember(x => x.MentorId, o => o.Ignore())
                .ForMember(x => x.Name, o => o.MapFrom(p => p.Name))
                .ForMember(x => x.Surname, o => o.MapFrom(p => p.Surname))
                .ForMember(x => x.MiddleName, o => o.MapFrom(p => p.MiddleName))
                .ForMember(x => x.ProfileImageBytes, o => o.Ignore())
                .ForMember(x => x.AboutMe, o => o.MapFrom(p => p.AboutMe))
                .ForMember(x => x.LocationInfo, o => o.MapFrom(p => p.LocationInfo))
                .ForMember(x => x.StudyingPlace, o => o.MapFrom(p => p.StudyingPlace))
                .ForMember(x => x.Education, o => o.MapFrom(p => p.Education));
        }
    }
}