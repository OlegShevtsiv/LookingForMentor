using LFM.DataAccess.DB.Core.Entities;
using LFM.DataAccess.DB.Core.Entities.MentorEntities;
using LFM.DataAccess.DB.Core.Entities.SubjectEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LFM.DataAccess.DB.Core.Context
{
    public static class ModelsConfiguration
    {
        public static void ConfigureRelations(ModelBuilder builder)
        {
            #region Identity

            builder.Entity<LfmUser>(m =>
            {
                m.ToTable("LfmUsers");
            });
            
            builder.Entity<LfmRole>(m =>
            {
                m.ToTable("LfmRoles");
            });
            
            builder.Entity<IdentityRoleClaim<int>>(m =>
            {
                m.ToTable("LfmRoleClaims");
            });
            
            builder.Entity<IdentityUserRole<int>>(m =>
            {
                m.ToTable("LfmUserRoles");
            });
            
            builder.Entity<IdentityUserClaim<int>>(m =>
            {
                m.ToTable("LfmUserClaims");
            });
            
            builder.Entity<IdentityUserToken<int>>(m =>
            {
                m.ToTable("LfmUserTokens");
            });
            
            builder.Entity<IdentityUserLogin<int>>(m =>
            {
                m.ToTable("LfmUserLogins");
            });

            #endregion

            #region Mentors Profile

            builder.Entity<MentorsProfile>(e =>
            {
                e.HasKey(m => m.MentorId);
                
                e.HasOne(m => m.Mentor)
                    .WithOne()
                    .HasForeignKey<MentorsProfile>(m => m.MentorId);
                
                e.HasOne(m => m.ProfileImage)
                    .WithOne()
                    .HasForeignKey<MentorsProfile>(m => m.ProfileImageId);

                e.HasMany(m => m.SubjectsInfo)
                    .WithOne()
                    .HasForeignKey(m => m.MentorId);
                
                e.HasOne(m => m.Town)
                    .WithMany()
                    .HasForeignKey(m => m.TownId);
            });

            builder.Entity<MentorsSubjectInfo>(e =>
            {
                e.HasOne(s => s.Subject)
                    .WithMany()
                    .HasForeignKey(s => s.SubjectId);
            });
            
            builder.Entity<MentorsSubjectTag>(e =>
            {
                e.HasKey(t => new { t.MentorsSubjectInfoId, t.TagId });
                
                e.HasOne(s => s.Tag)
                    .WithMany()
                    .HasForeignKey(s => s.TagId);
                
                e.HasOne(s => s.MentorsSubjectInfo)
                    .WithMany(s => s.Tags)
                    .HasForeignKey(s => s.MentorsSubjectInfoId);
            });

            #endregion

            #region Orders 

            builder.Entity<OrderRequest>(e =>
            {
                e.HasOne(p => p.Mentor)
                    .WithMany()
                    .HasForeignKey(p => p.MentorId);
                
                e.HasOne(p => p.Subject)
                    .WithMany()
                    .HasForeignKey(p => p.SubjectId);
                
                e.HasOne(p => p.SubjectsTag)
                    .WithMany()
                    .HasForeignKey(p => p.TagId);
            });
            
            builder.Entity<ApprovedOrder>(e =>
            {
                e.HasOne(p => p.Mentor)
                    .WithMany()
                    .HasForeignKey(p => p.MentorId);
                
                e.HasOne(p => p.Subject)
                    .WithMany()
                    .HasForeignKey(p => p.SubjectId);
                
                e.HasOne(p => p.SubjectsTag)
                    .WithMany()
                    .HasForeignKey(p => p.TagId);
            });
            
            builder.Entity<RejectedOrder>(e =>
            {
                e.HasOne(p => p.Mentor)
                    .WithMany()
                    .HasForeignKey(p => p.MentorId);
                
                e.HasOne(p => p.Student)
                    .WithMany()
                    .HasForeignKey(p => p.StudentId);
                
                e.HasOne(p => p.Subject)
                    .WithMany()
                    .HasForeignKey(p => p.SubjectId);
                
                e.HasOne(p => p.SubjectsTag)
                    .WithMany()
                    .HasForeignKey(p => p.TagId);
            });

            #endregion

            #region Master Data

            builder.Entity<Subject>(e =>
            {
                e.HasMany(s => s.Tags)
                    .WithMany(t => t.Subjects)
                    .UsingEntity<TagSubjectRelation>(
                        j => j
                            .HasOne(pt => pt.Tag)
                            .WithMany(p => p.SubjectsRelations)
                            .HasForeignKey(pt => pt.TagId),
                        j => j
                            .HasOne(pt => pt.Subject)
                            .WithMany(t => t.TagsRelations)
                            .HasForeignKey(pt => pt.SubjectId),
                        j =>
                        {
                            j.HasKey(t => new { t.SubjectId, t.TagId });
                        });
            });

            #endregion
        }
    }
}