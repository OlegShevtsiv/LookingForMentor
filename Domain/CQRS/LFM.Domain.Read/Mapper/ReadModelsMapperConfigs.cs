using System.Linq;
using AutoMapper;
using LFM.DataAccess.DB.Core.Entities;
using LFM.DataAccess.DB.Core.Entities.MentorEntities;
using LFM.DataAccess.DB.Core.Entities.SubjectEntities;
using Lfm.Domain.ReadModels.ReviewModels.Mentor;
using Lfm.Domain.ReadModels.ReviewModels.MentorProfile;
using Lfm.Domain.ReadModels.ReviewModels.StudentProfile;
using Lfm.Domain.ReadModels.ReviewModels.Subject;

namespace LFM.Domain.Read.Mapper
{
    internal class ReadModelsMapperConfigs : Profile
    {
        public ReadModelsMapperConfigs()
        {
            CreateMentorProfileMaps();
            CreateSubjectMaps();
            CreateStudentProfileMaps();
        }

        private void CreateMentorProfileMaps()
        {
            CreateMap<MentorsSubjectInfo, MentorProfilePreviewModel.SubjectInfo>()
                .ForMember(x => x.CostPerHour, o => o.MapFrom(p => p.CostPerHour))
                .ForMember(x => x.SubjectName, o => o.MapFrom(p => p.Subject.Name))
                .ForMember(x => x.Tags, o => o.MapFrom(p => p.Subject.Tags));
            
            CreateMap<MentorsSubjectTag, TagReviewModel>()
                .ForMember(x => x.Id, o => o.MapFrom(p => p.TagId))
                .ForMember(x => x.Name, o => o.MapFrom(p => p.Tag.Name));

            CreateMap<MentorsProfile, MentorProfilePreviewModel>()
                .ForMember(x => x.Surname, o => o.MapFrom(p => p.Surname))
                .ForMember(x => x.MiddleName, o => o.MapFrom(p => p.MiddleName))
                .ForMember(x => x.ProfileImageId, o => o.MapFrom(p => p.ProfileImageId))
                .ForMember(x => x.AboutMe, o => o.MapFrom(p => p.AboutMe))
                .ForMember(x => x.SubjectsInfos, o => o.MapFrom(p => p.SubjectsInfo))
                .ForMember(x => x.TownName, o => o.MapFrom(p => p.Town.Name))
                .ForMember(x => x.StudyingPlace, o => o.MapFrom(p => p.StudyingPlace))
                .ForMember(x => x.Education, o => o.MapFrom(p => p.Education));
            
            CreateMap<MentorsSubjectInfo, MentorSubjectReviewModel>()
                .ForMember(x => x.CostPerHour, o => o.MapFrom(p => p.CostPerHour))
                .ForMember(x => x.Description, o => o.MapFrom(p => p.Description))
                .ForMember(x => x.SubjectId, o => o.MapFrom(p => p.SubjectId))
                .ForMember(x => x.SubjectName, o => o.MapFrom(p => p.Subject.Name))
                .ForMember(x => x.SelectedTags, o => o.MapFrom(p => p.Tags));

            CreateMap<MentorsProfile, MentorPreviewModel>()
                .ForMember(x => x.MentorId, o => o.MapFrom(p => p.MentorId))
                .ForMember(x => x.Name, o => o.MapFrom(p => p.Mentor.Name))
                .ForMember(x => x.SubjectsList, o => o.MapFrom(p => p.SubjectsInfo))
                .ForMember(x => x.TownName, o => o.MapFrom(p => p.Town.Name))
                .ForMember(x => x.TownId, o => o.MapFrom(p => p.TownId))
                .ForMember(x => x.StudyingPlace, o => o.MapFrom(p => p.StudyingPlace));

            CreateMap<MentorsSubjectInfo, MentorPreviewModel.Subject>()
                .ForMember(x => x.Id, o => o.MapFrom(p => p.SubjectId))
                .ForMember(x => x.Name, o => o.MapFrom(p => p.Subject.Name));
            
            CreateMap<MentorsProfile, MentorDetailedPreviewModel>()
                .ForMember(x => x.MentorId, o => o.MapFrom(p => p.MentorId))
                .ForMember(x => x.Name, o => o.MapFrom(p => p.Mentor.Name))
                .ForMember(x => x.TownName, o => o.MapFrom(p => p.Town.Name))
                .ForMember(x => x.StudyingPlace, o => o.MapFrom(p => p.StudyingPlace))
                .ForMember(x => x.EducationInfo, o => o.MapFrom(p => p.Education))
                .ForMember(x => x.AboutInfo, o => o.MapFrom(p => p.AboutMe))
                .ForMember(x => x.SubjectsList, o => o.MapFrom(p => p.SubjectsInfo));
            
            CreateMap<MentorsSubjectInfo, MentorDetailedPreviewModel.SubjectInfo>()
                .ForMember(x => x.SubjectId, o => o.MapFrom(p => p.SubjectId))
                .ForMember(x => x.SubjectName, o => o.MapFrom(p => p.Subject.Name))
                .ForMember(x => x.CostPerHour, o => o.MapFrom(p => p.CostPerHour))
                .ForMember(x => x.Tags, o => o.MapFrom(p => p.Tags.Select(t => t.Tag.Name)));

            CreateMap<MentorsProfile, ContactMentorInfo>()
                .ForMember(x => x.MentorId, o => o.MapFrom(p => p.MentorId))
                .ForMember(x => x.Name, o => o.MapFrom(p => p.Mentor.Name))
                .ForMember(x => x.StudyingPlace, o => o.MapFrom(p => p.StudyingPlace))
                .ForMember(x => x.Subject, o => o.Ignore());

            CreateMap<MentorsSubjectInfo, ContactMentorInfo.SubjectInfo>()
                .ForMember(x => x.SubjectId, o => o.MapFrom(p => p.SubjectId))
                .ForMember(x => x.SubjectName, o => o.MapFrom(p => p.Subject.Name))
                .ForMember(x => x.CostPerHour, o => o.MapFrom(p => p.CostPerHour))
                .ForMember(x => x.Tags, o => o.MapFrom(p => p.Tags));

            CreateMap<OrderRequest, MentorPersonalOrderDetailedReviewModel>()
                .ForMember(x => x.SubjectName, o => o.MapFrom(p => p.Subject.Name))
                .ForMember(x => x.TagName, o => o.MapFrom(p => p.SubjectsTag.Name));
            
            CreateMap<OrderRequest, MentorsOrderMinReviewModel>()
                .ForMember(x => x.SubjectName, o => o.MapFrom(p => p.Subject.Name))
                .ForMember(x => x.TagName, o => o.MapFrom(p => p.SubjectsTag.Name))
                .ForMember(x => x.DateTime, o => o.MapFrom(p => p.CreationDateTime));

            CreateMap<ApprovedOrder, MentorsOrderMinReviewModel>()
                .ForMember(x => x.SubjectName, o => o.MapFrom(p => p.Subject.Name))
                .ForMember(x => x.TagName, o => o.MapFrom(p => p.SubjectsTag.Name))
                .ForMember(x => x.DateTime, o => o.MapFrom(p => p.ApprovedDateTime));
            
            CreateMap<ApprovedOrder, MentorsOrderDetailedReviewModel>()
                .ForMember(x => x.SubjectName, o => o.MapFrom(p => p.Subject.Name))
                .ForMember(x => x.TagName, o => o.MapFrom(p => p.SubjectsTag.Name));
        }

        private void CreateStudentProfileMaps()
        {
            CreateMap<OrderRequest, FindMentorRequestReviewModel>()
                .ForMember(x => x.SubjectName, o => o.MapFrom(p => p.Subject.Name))
                .ForMember(x => x.TagName, o => o.MapFrom(p => p.SubjectsTag.Name))
                .ForMember(x => x.DateTime, o => o.MapFrom(p => p.CreationDateTime));
        }

        private void CreateSubjectMaps()
        {
            CreateMap<SubjectsTag, TagReviewModel>()
                .ForMember(x => x.Id, o => o.MapFrom(p => p.Id))
                .ForMember(x => x.Name, o => o.MapFrom(p => p.Name));
            
            CreateMap<Subject, SubjectReviewModel>()
                .ForMember(x => x.Id, o => o.MapFrom(p => p.Id))
                .ForMember(x => x.Name, o => o.MapFrom(p => p.Name))
                .ForMember(x => x.Tags, o => o.MapFrom(p => p.Tags));

            CreateMap<SubjectReviewModel, SubjectListItem>()
                .ForMember(x => x.Id, o => o.MapFrom(p => p.Id))
                .ForMember(x => x.Name, o => o.MapFrom(p => p.Name));
        }
    }
}