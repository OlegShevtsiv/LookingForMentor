﻿using LFM.DataAccess.DB.Core.Entities;
using LFM.DataAccess.DB.Core.Entities.MentorEntities;
using LFM.DataAccess.DB.Core.Entities.SubjectEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LFM.DataAccess.DB.Core.Context
{
    public abstract class LfmDbContext : IdentityDbContext<LfmUser, LfmRole, int>
    {
        public DbSet<LfmUser> LfmUsers { get; set; }
        
        public DbSet<LfmRole> LfmRoles { get; set; }
        
        public DbSet<MentorsProfile> MentorsProfiles { get; set; }
        
        public DbSet<MentorsProfileImage> MentorsProfileImages { get; set; }
        
        public DbSet<MentorsSubjectInfo> MentorsSubjectsInfo { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<SubjectsTag> SubjectsTags { get; set; }


        public LfmDbContext()
        {
            Database.EnsureCreated();
        }
        
        public LfmDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ModelsConfiguration.ConfigureRelations(builder);
            MasterData.Register(builder);
        }
    }
}
