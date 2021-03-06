﻿using Microsoft.EntityFrameworkCore;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext : DbContext
    {
        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistories { get; set; }
        public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        public DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobskills { get; set; }
        public DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        public DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ConfigurationBuilder config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            string ConnectionString = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            optionsBuilder.UseSqlServer(ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicantEducationPoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
                entity.HasOne(d => d.ApplicantProfile)
                    .WithMany(p => p.ApplicantEducations)
                    .HasForeignKey(d => d.Applicant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applicant_Educations_Applicant_Profiles");
            });
            modelBuilder.Entity<ApplicantJobApplicationPoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
                entity.HasOne(d => d.ApplicantProfile)
                    .WithMany(p => p.ApplicantJobApplications)
                    .HasForeignKey(d => d.Applicant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applicant_Job_Applications_Applicant_Profiles");

                entity.HasOne(d => d.CompanyJob)
                    .WithMany(p => p.ApplicantJobApplications)
                    .HasForeignKey(d => d.Job)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applicant_Job_Applications_Company_Jobs");
            });
            modelBuilder.Entity<ApplicantProfilePoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
                entity.HasOne(d => d.SystemCountryCode)
                    .WithMany(p => p.ApplicantProfiles)
                    .HasForeignKey(d => d.Country)
                    .HasConstraintName("FK_Applicant_Profiles_System_Country_Codes");

                entity.HasOne(d => d.SecurityLogin)
                    .WithMany(p => p.ApplicantProfiles)
                    .HasForeignKey(d => d.Login)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applicant_Profiles_Security_Logins");
            });

            modelBuilder.Entity<ApplicantResumePoco>(entity =>
            {
                 entity.HasOne(d => d.ApplicantProfile)
                    .WithMany(p => p.ApplicantResumes)
                    .HasForeignKey(d => d.Applicant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applicant_Resumes_Applicant_Profiles");
            });

            modelBuilder.Entity<ApplicantSkillPoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
                entity.HasOne(d => d.ApplicantProfile)
                    .WithMany(p => p.ApplicantSkills)
                    .HasForeignKey(d => d.Applicant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applicant_Skills_Applicant_Profiles");
            });

            modelBuilder.Entity<ApplicantWorkHistoryPoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
                entity.HasOne(d => d.ApplicantProfile)
                    .WithMany(p => p.ApplicantWorkHistorys)
                    .HasForeignKey(d => d.Applicant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applicant_Work_Experiences_Applicant_Profiles");

                entity.HasOne(d => d.SystemCountryCode)
                    .WithMany(p => p.ApplicantWorkHistorys)
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applicant_Work_History_System_Country_Codes");
            });

            modelBuilder.Entity<CompanyDescriptionPoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
                entity.HasOne(d => d.CompanyProfile)
                    .WithMany(p => p.CompanyDescriptions)
                    .HasForeignKey(d => d.Company)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_Descriptions_Company_Profiles");

                entity.HasOne(d => d.SystemLanguageCode)
                    .WithMany(p => p.CompanyDescriptions)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_Descriptions_System_Language_Codes");
            });

            modelBuilder.Entity<CompanyJobPoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
                entity.HasOne(d => d.CompanyProfile)
                    .WithMany(p => p.CompanyJobs)
                    .HasForeignKey(d => d.Company)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_Jobs_Company_Profiles");
            });

            modelBuilder.Entity<CompanyJobEducationPoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
                entity.HasOne(d => d.CompanyJob)
                    .WithMany(p => p.CompanyJobEducations)
                    .HasForeignKey(d => d.Job)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_Job_Educations_Company_Jobs");
            });

            modelBuilder.Entity<CompanyJobSkillPoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
                entity.HasOne(d => d.CompanyJob)
                     .WithMany(p => p.CompanyJobSkills)
                     .HasForeignKey(d => d.Job)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_Company_Job_Skills_Company_Jobs");
            });

            modelBuilder.Entity<CompanyJobDescriptionPoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
                entity.HasOne(d => d.CompanyJob)
                    .WithMany(p => p.CompanyJobDescriptions)
                    .HasForeignKey(d => d.Job)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_Jobs_Descriptions_Company_Jobs");
            });

            modelBuilder.Entity<CompanyLocationPoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
                entity.HasOne(d => d.CompanyProfile)
                    .WithMany(p => p.CompanyLocations)
                    .HasForeignKey(d => d.Company)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_Locations_Company_Profiles");
            });

            modelBuilder.Entity<CompanyProfilePoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
            });

            modelBuilder.Entity<SecurityLoginPoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();

            });

            modelBuilder.Entity<SecurityLoginsLogPoco>(entity =>
            {
                entity.HasOne(d => d.SecurityLogin)
                    .WithMany(p => p.SecurityLoginsLogs)
                    .HasForeignKey(d => d.Login)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Security_Logins_Log_Security_Logins");
            });

            modelBuilder.Entity<SecurityLoginsRolePoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
                entity.HasOne(d => d.SecurityLogin)
                    .WithMany(p => p.SecurityLoginsRoles)
                    .HasForeignKey(d => d.Login)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Security_Logins_Roles_Security_Logins");

                entity.HasOne(d => d.SecurityRole)
                    .WithMany(p => p.SecurityLoginsRoles)
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Security_Logins_Roles_Security_Roles");
            });

            modelBuilder.Entity<SecurityRolePoco>(entity =>
            {
                
            });

            modelBuilder.Entity<SystemCountryCodePoco>(entity =>
            {
                

            });

            modelBuilder.Entity<SystemLanguageCodePoco>(entity =>
            {
               
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
