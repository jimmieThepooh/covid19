using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Covid19.EntityModel
{
    public partial class Covid19Context : DbContext
    {
        public Covid19Context()
        {
        }

        public Covid19Context(DbContextOptions<Covid19Context> options)
            : base(options)
        {
        }

        public virtual DbSet<COVID_HEALTHCHECK> COVID_HEALTHCHECK { get; set; }
        public virtual DbSet<COVID_HEALTHCHECK_COUNTRY> COVID_HEALTHCHECK_COUNTRY { get; set; }
        public virtual DbSet<COVID_HEALTHCHECK_SYMTOMS> COVID_HEALTHCHECK_SYMTOMS { get; set; }
        public virtual DbSet<EMPLOYEE> EMPLOYEE { get; set; }
        public virtual DbSet<LUT_COVID_CAUSE> LUT_COVID_CAUSE { get; set; }
        public virtual DbSet<LUT_COVID_COUNTRY> LUT_COVID_COUNTRY { get; set; }
        public virtual DbSet<LUT_COVID_RELATION> LUT_COVID_RELATION { get; set; }
        public virtual DbSet<LUT_COVID_REPORTER> LUT_COVID_REPORTER { get; set; }
        public virtual DbSet<LUT_COVID_SYMTOM> LUT_COVID_SYMTOM { get; set; }
        public virtual DbSet<LUT_HEALTH_OCCUPATION> LUT_HEALTH_OCCUPATION { get; set; }
        public virtual DbSet<LUT_HEALTH_SYMTOM> LUT_HEALTH_SYMTOM { get; set; }
        public virtual DbSet<RPT_COVID19EX> RPT_COVID19EX { get; set; }
        public virtual DbSet<RPT_COVID19EX_COUNTRY> RPT_COVID19EX_COUNTRY { get; set; }
        public virtual DbSet<RPT_COVID19EX_SYMTOMS> RPT_COVID19EX_SYMTOMS { get; set; }
        public virtual DbSet<RPT_COVID19EX_SYMTOMS_OTHER> RPT_COVID19EX_SYMTOMS_OTHER { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<COVID_HEALTHCHECK>(entity =>
            {
                entity.Property(e => e.CREATED_DT).HasColumnType("datetime");

                entity.Property(e => e.TRAVEL_IN_14_DAYS_COUNTRY).HasMaxLength(100);

                entity.Property(e => e.TRAVEL_IN_14_DAYS_COUNTRY_OTHER).HasMaxLength(100);
            });

            modelBuilder.Entity<COVID_HEALTHCHECK_COUNTRY>(entity =>
            {
                entity.HasOne(d => d.REPORTER_)
                    .WithMany(p => p.COVID_HEALTHCHECK_COUNTRY)
                    .HasForeignKey(d => d.REPORTER_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COVID_HEALTHCHECK_COUNTRY");
            });

            modelBuilder.Entity<COVID_HEALTHCHECK_SYMTOMS>(entity =>
            {
                entity.HasOne(d => d.REPORTER_)
                    .WithMany(p => p.COVID_HEALTHCHECK_SYMTOMS)
                    .HasForeignKey(d => d.REPORTER_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COVID_HEALTHCHECK_SYMTOMS");
            });

            modelBuilder.Entity<EMPLOYEE>(entity =>
            {
                entity.HasKey(e => e.PERSON_ID);

                entity.Property(e => e.PERSON_ID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CREATED_DT).HasColumnType("datetime");

                entity.Property(e => e.EMAIL)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ENDDATE).HasColumnType("datetime");

                entity.Property(e => e.FIRSTNAME)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FULLNAME)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.JOBDESC)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LASTNAME)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ORGDESC)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.STARTDATE).HasColumnType("datetime");

                entity.Property(e => e.TEL)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LUT_COVID_CAUSE>(entity =>
            {
                entity.Property(e => e.DESCR).HasMaxLength(50);
            });

            modelBuilder.Entity<LUT_COVID_COUNTRY>(entity =>
            {
                entity.Property(e => e.COUNTRY_CODE).HasMaxLength(10);

                entity.Property(e => e.COUNTRY_NAME).HasMaxLength(100);

                entity.Property(e => e.COUNTRY_NAMT).HasMaxLength(100);

                entity.Property(e => e.COUNTRY_SH_CODE)
                    .HasMaxLength(2)
                    .IsFixedLength();
            });

            modelBuilder.Entity<LUT_COVID_RELATION>(entity =>
            {
                entity.Property(e => e.DESCR).HasMaxLength(50);
            });

            modelBuilder.Entity<LUT_COVID_REPORTER>(entity =>
            {
                entity.Property(e => e.REPORTER).HasMaxLength(50);
            });

            modelBuilder.Entity<LUT_COVID_SYMTOM>(entity =>
            {
                entity.Property(e => e.SYMTOM).HasMaxLength(50);
            });

            modelBuilder.Entity<LUT_HEALTH_OCCUPATION>(entity =>
            {
                entity.Property(e => e.DESCR).HasMaxLength(50);
            });

            modelBuilder.Entity<LUT_HEALTH_SYMTOM>(entity =>
            {
                entity.Property(e => e.SYMTOM).HasMaxLength(50);
            });

            modelBuilder.Entity<RPT_COVID19EX>(entity =>
            {
                entity.Property(e => e.ARRIVAL_DT).HasColumnType("date");

                entity.Property(e => e.ARRIVAL_FLIGHT).HasMaxLength(10);

                entity.Property(e => e.COMPANION_NAME1).HasMaxLength(100);

                entity.Property(e => e.COMPANION_NAME2).HasMaxLength(100);

                entity.Property(e => e.COMPANION_NAME3).HasMaxLength(100);

                entity.Property(e => e.COMPANION_RELATION1).HasMaxLength(100);

                entity.Property(e => e.COMPANION_RELATION2).HasMaxLength(100);

                entity.Property(e => e.COMPANION_RELATION3).HasMaxLength(100);

                entity.Property(e => e.CREATED_DT).HasColumnType("datetime");

                entity.Property(e => e.DEPARTURE_DT).HasColumnType("date");

                entity.Property(e => e.DEPARTURE_FLIGHT).HasMaxLength(10);

                entity.Property(e => e.HAS_FLU).HasMaxLength(10);

                entity.Property(e => e.HAS_FLU_OTHER).HasMaxLength(10);

                entity.Property(e => e.PERSON_ID)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.REPORT_DT).HasColumnType("datetime");

                entity.Property(e => e.TEMPERATURE).HasColumnType("decimal(3, 1)");

                entity.Property(e => e.TEMPERATURE_OTHER).HasColumnType("decimal(3, 1)");
            });

            modelBuilder.Entity<RPT_COVID19EX_COUNTRY>(entity =>
            {
                entity.HasOne(d => d.REPORTER_)
                    .WithMany(p => p.RPT_COVID19EX_COUNTRY)
                    .HasForeignKey(d => d.REPORTER_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RPT_COVIDEX__REPOR__361203C5");
            });

            modelBuilder.Entity<RPT_COVID19EX_SYMTOMS>(entity =>
            {
                entity.HasOne(d => d.REPORTER_)
                    .WithMany(p => p.RPT_COVID19EX_SYMTOMS)
                    .HasForeignKey(d => d.REPORTER_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RPT_COVIDEX__REPOR__3429BB53");
            });

            modelBuilder.Entity<RPT_COVID19EX_SYMTOMS_OTHER>(entity =>
            {
                entity.HasOne(d => d.REPORTER_)
                    .WithMany(p => p.RPT_COVID19EX_SYMTOMS_OTHER)
                    .HasForeignKey(d => d.REPORTER_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RPT_COVIDEX__REPOR__351DDF8C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
