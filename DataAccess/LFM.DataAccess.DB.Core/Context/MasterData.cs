using System.Collections.Generic;
using LFM.DataAccess.DB.Core.Entities.SubjectEntities;
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
                    Id = 100,
                    Name = "Base level"
                },
                new SubjectsTag
                {
                    Id = 101,
                    Name = "Elementary School"
                },
                new SubjectsTag
                {
                    Id = 102,
                    Name = "Secondary School"
                },
                new SubjectsTag
                {
                    Id = 103,
                    Name = "High School"
                },
                new SubjectsTag
                {
                    Id = 104,
                    Name = "For beginners"
                },
                new SubjectsTag
                {
                    Id = 105,
                    Name = "For professionals"
                },
                new SubjectsTag
                {
                    Id = 106,
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
                    TagId = 101
                },
                new TagSubjectRelation
                {
                    SubjectId = 1,
                    TagId = 102
                },
                new TagSubjectRelation
                {
                    SubjectId = 1,
                    TagId = 103
                },
                new TagSubjectRelation
                {
                    SubjectId = 2,
                    TagId = 100
                },
                new TagSubjectRelation
                {
                    SubjectId = 2,
                    TagId = 104
                },
                new TagSubjectRelation
                {
                    SubjectId = 2,
                    TagId = 105
                }
                ,new TagSubjectRelation
                {
                    SubjectId = 2,
                    TagId = 106
                },
                new TagSubjectRelation
                {
                    SubjectId = 3,
                    TagId = 102
                },
                new TagSubjectRelation
                {
                    SubjectId = 3,
                    TagId = 103
                }
                ,new TagSubjectRelation
                {
                    SubjectId = 3,
                    TagId = 106
                }
            });
        }
    }
}