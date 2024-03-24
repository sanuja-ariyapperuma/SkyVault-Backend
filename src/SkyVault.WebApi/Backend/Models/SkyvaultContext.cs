using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using SkyVault.WebApi.Backend.Seeds;

namespace SkyVault.WebApi.Backend.Models;

public partial class SkyvaultContext : DbContext
{
    public SkyvaultContext()
    {
    }

    public SkyvaultContext(DbContextOptions<SkyvaultContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CommunicationMethod> CommunicationMethods { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<CustomerProfile> CustomerProfiles { get; set; }

    public virtual DbSet<FrequentFlyerNumber> FrequentFlyerNumbers { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Nationality> Nationalities { get; set; }

    public virtual DbSet<NotificationTemplate> NotificationTemplates { get; set; }

    public virtual DbSet<NotificationType> NotificationTypes { get; set; }

    public virtual DbSet<Passport> Passports { get; set; }

    public virtual DbSet<Salutation> Salutations { get; set; }

    public virtual DbSet<SystemUser> SystemUsers { get; set; }

    public virtual DbSet<Visa> Visas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<CommunicationMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("communication_methods");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommTitle)
                .HasMaxLength(50)
                .HasColumnName("comm_title");

            // Seed data
            modelBuilder.Entity<Country>().HasData(CountrySeedData.countries);
            modelBuilder.Entity<Salutation>().HasData(SalutationSeedData.salutations);
            modelBuilder.Entity<Nationality>().HasData(NationalitySeedData.nationalities);

        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("countries");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(5)
                .HasColumnName("country_code");
            entity.Property(e => e.CountryName)
                .HasMaxLength(100)
                .HasColumnName("country_name");
        });

        modelBuilder.Entity<CustomerProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("customer_profiles");

            entity.HasIndex(e => e.PreferredCommId, "fk_customerprofiles_communication_methods");

            entity.HasIndex(e => e.ParentId, "fk_customerprofiles_customerprofiles");

            entity.HasIndex(e => e.SalutationId, "fk_customerprofiles_salutations");

            entity.HasIndex(e => e.SystemUserId, "fk_customerprofiles_system_users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.PreferredCommId).HasColumnName("preferred_comm_id");
            entity.Property(e => e.SalutationId).HasColumnName("salutation_id");
            entity.Property(e => e.SystemUserId).HasColumnName("system_user_id");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("fk_customerprofiles_customerprofiles");

            entity.HasOne(d => d.PreferredComm).WithMany(p => p.CustomerProfiles)
                .HasForeignKey(d => d.PreferredCommId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_customerprofiles_communication_methods");

            entity.HasOne(d => d.Salutation).WithMany(p => p.CustomerProfiles)
                .HasForeignKey(d => d.SalutationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_customerprofiles_salutations");

            entity.HasOne(d => d.SystemUser).WithMany(p => p.CustomerProfiles)
                .HasForeignKey(d => d.SystemUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_customerprofiles_system_users");
        });

        modelBuilder.Entity<FrequentFlyerNumber>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("frequent_flyer_numbers");

            entity.HasIndex(e => e.CustomerProfileId, "fk_frequentflyernumbers_customerprofiles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerProfileId).HasColumnName("customer_profile_id");
            entity.Property(e => e.FlyerNumber)
                .HasMaxLength(50)
                .HasColumnName("flyer_number");

            entity.HasOne(d => d.CustomerProfile).WithMany(p => p.FrequentFlyerNumbers)
                .HasForeignKey(d => d.CustomerProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_frequentflyernumbers_customerprofiles");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("jobs");

            entity.HasIndex(e => e.CustomerProfileId, "fk_jobs_customerprofiles");

            entity.HasIndex(e => e.TemplateId, "fk_jobs_notification_templates");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerProfileId).HasColumnName("customer_profile_id");
            entity.Property(e => e.DateTime).HasColumnName("date_time");
            entity.Property(e => e.Log)
                .HasColumnType("text")
                .HasColumnName("log");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("status");
            entity.Property(e => e.TemplateId).HasColumnName("template_id");

            entity.HasOne(d => d.CustomerProfile).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.CustomerProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_jobs_customerprofiles");

            entity.HasOne(d => d.Template).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.TemplateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_jobs_notification_templates");
        });

        modelBuilder.Entity<Nationality>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("nationalities");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NationalityName)
                .HasMaxLength(100)
                .HasColumnName("nationality_name");
        });

        modelBuilder.Entity<NotificationTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("notification_templates");

            entity.HasIndex(e => e.NotificationType, "fk_notification_templates_notification_types");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.File).HasColumnName("file");
            entity.Property(e => e.NotificationType).HasColumnName("notification_type");

            entity.HasOne(d => d.NotificationTypeNavigation).WithMany(p => p.NotificationTemplates)
                .HasForeignKey(d => d.NotificationType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_notification_templates_notification_types");
        });

        modelBuilder.Entity<NotificationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("notification_types");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TypeName)
                .HasMaxLength(100)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<Passport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("passports");

            entity.HasIndex(e => e.CustomerProfileId, "fk_passports_customerprofiles");

            entity.HasIndex(e => e.NationalityId, "fk_passports_nationalities");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerProfileId).HasColumnName("customer_profile_id");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .HasColumnName("gender");
            entity.Property(e => e.IsPrimary)
                .HasMaxLength(1)
                .HasDefaultValueSql("'0'")
                .IsFixedLength()
                .HasColumnName("is_primary");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.NationalityId).HasColumnName("nationality_id");
            entity.Property(e => e.OtherNames)
                .HasMaxLength(100)
                .HasColumnName("other_names");
            entity.Property(e => e.PassportNumber)
                .HasMaxLength(100)
                .HasColumnName("passport_number");
            entity.Property(e => e.PlaceOfBirth)
                .HasMaxLength(100)
                .HasColumnName("place_of_birth");

            entity.HasOne(d => d.CustomerProfile).WithMany(p => p.Passports)
                .HasForeignKey(d => d.CustomerProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_passports_customerprofiles");

            entity.HasOne(d => d.Nationality).WithMany(p => p.Passports)
                .HasForeignKey(d => d.NationalityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_passports_nationalities");
        });

        modelBuilder.Entity<Salutation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("salutations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SalutationName)
                .HasMaxLength(100)
                .HasColumnName("salutation_name");
        });

        modelBuilder.Entity<SystemUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("system_users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasMaxLength(1)
                .HasDefaultValueSql("'1'")
                .IsFixedLength()
                .HasColumnName("active");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.ProfilePicture)
                .HasColumnType("text")
                .HasColumnName("profile_picture");
            entity.Property(e => e.SamProfileId)
                .HasMaxLength(50)
                .HasColumnName("sam_profile_id");
            entity.Property(e => e.UserRole)
                .HasMaxLength(10)
                .HasColumnName("user_role");
        });

        modelBuilder.Entity<Visa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("visas");

            entity.HasIndex(e => e.CountryId, "fk_visas_countries");

            entity.HasIndex(e => e.PassportId, "fk_visas_passports");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.ExpireDate).HasColumnName("expire_date");
            entity.Property(e => e.IssuedDate).HasColumnName("issued_date");
            entity.Property(e => e.IssuedPlace)
                .HasMaxLength(100)
                .HasColumnName("issued_place");
            entity.Property(e => e.PassportId).HasColumnName("passport_id");
            entity.Property(e => e.VisaNumber)
                .HasMaxLength(100)
                .HasColumnName("visa_number");

            entity.HasOne(d => d.Country).WithMany(p => p.Visas)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_visas_countries");

            entity.HasOne(d => d.Passport).WithMany(p => p.Visas)
                .HasForeignKey(d => d.PassportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_visas_passports");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
