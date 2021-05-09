using System.Collections.Generic;
using System.Linq;
using LFM.DataAccess.DB.Core.Entities;
using LFM.DataAccess.DB.Core.Entities.SubjectEntities;
using LFM.DataAccess.DB.Core.MasterDataProviders;
using LFM.DataAccess.DB.Core.Resources;
using Microsoft.EntityFrameworkCore;

namespace LFM.DataAccess.DB.Core.Context
{
    public static class MasterData
    {
        public static void Register(ModelBuilder modelBuilder)
        {
            List<SubjectsTag> Tags = new List<SubjectsTag>
            {
                new SubjectsTag
                {
                    Id = 1,
                    Name = "Base level"
                },
                new SubjectsTag
                {
                    Id = 2,
                    Name = "Elementary School"
                },
                new SubjectsTag
                {
                    Id = 3,
                    Name = "Secondary School"
                },
                new SubjectsTag
                {
                    Id = 4,
                    Name = "High School"
                },
                new SubjectsTag
                {
                    Id = 5,
                    Name = "For beginners"
                },
                new SubjectsTag
                {
                    Id = 6,
                    Name = "For professionals"
                },
                new SubjectsTag
                {
                    Id = 7,
                    Name = "University courses"
                }
            };
            
            modelBuilder.Entity<SubjectsTag>().HasData(Tags.ToArray());
            
            modelBuilder.Entity<Subject>().HasData(new[]
            {
                new Subject
                {
                    Id = 1,
                    Name = "Math"
                },
                new Subject
                {
                    Id = 2,
                    Name = "Programing C#"
                },
                new Subject
                {
                    Id = 3,
                    Name = "English"
                }
            });
            
            modelBuilder.Entity<TagSubjectRelation>().HasData(new[]
            {
                new TagSubjectRelation
                {
                    SubjectId = 1,
                    TagId = 2
                },
                new TagSubjectRelation
                {
                    SubjectId = 1,
                    TagId = 3
                },
                new TagSubjectRelation
                {
                    SubjectId = 1,
                    TagId = 4
                },
                new TagSubjectRelation
                {
                    SubjectId = 2,
                    TagId = 1
                },
                new TagSubjectRelation
                {
                    SubjectId = 2,
                    TagId = 5
                },
                new TagSubjectRelation
                {
                    SubjectId = 2,
                    TagId = 6
                },
                new TagSubjectRelation
                {
                    SubjectId = 2,
                    TagId = 7
                },
                new TagSubjectRelation
                {
                    SubjectId = 3,
                    TagId = 1
                },
                new TagSubjectRelation
                {
                    SubjectId = 3,
                    TagId = 2
                },
                new TagSubjectRelation
                {
                    SubjectId = 3,
                    TagId = 3
                },
                new TagSubjectRelation
                {
                    SubjectId = 3,
                    TagId = 4
                },
                new TagSubjectRelation
                {
                    SubjectId = 3,
                    TagId = 5
                },
                new TagSubjectRelation
                {
                    SubjectId = 3,
                    TagId = 6
                },
                new TagSubjectRelation
                {
                    SubjectId = 3,
                    TagId = 7
                }
            });
            
            modelBuilder.Entity<Town>().HasData(GetTowns());
        }

        private static Town[] GetTowns()
        {
            TownsResourceProvider townsResourceProvider = new TownsResourceProvider();
            var towns = townsResourceProvider.GetAllTowns().Result.ToArray();
            return towns;
        }
    }
}