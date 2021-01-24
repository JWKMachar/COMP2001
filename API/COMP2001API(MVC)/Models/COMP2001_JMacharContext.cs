using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace COMP2001API_MVC_.Models
{
    public partial class COMP2001_JMacharContext : DbContext
    {
        public COMP2001_JMacharContext()
        {
        }

        public COMP2001_JMacharContext(DbContextOptions<COMP2001_JMacharContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Courseworkdataset> Courseworkdatasets { get; set; }
        public virtual DbSet<Courseworkpassword> Courseworkpasswords { get; set; }
        public virtual DbSet<Courseworksession> Courseworksessions { get; set; }
        public virtual DbSet<Courseworkusername> Courseworkusernames { get; set; }
        public virtual DbSet<VisitsView> VisitsViews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= socem1.uopnet.plymouth.ac.uk;Database=COMP2001_JMachar;User Id=JMachar;Password=RgaD433+");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Courseworkdataset>(entity =>
            {
                entity.HasKey(e => new { e.CrimeType, e.CrimeYear })
                    .HasName("PK__COURSEWO__310C08AE5CEAF437");

                entity.ToTable("COURSEWORKDataset");

                entity.Property(e => e.CrimeType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("crimeType");

                entity.Property(e => e.CrimeYear).HasColumnName("crimeYear");

                entity.Property(e => e.CrimeCount).HasColumnName("crimeCount");
            });

            modelBuilder.Entity<Courseworkpassword>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.DateChanged })
                    .HasName("PK__COURSEWO__B5DF0242FE8F7A8D");

                entity.ToTable("COURSEWORKPasswords");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.DateChanged)
                    .HasColumnType("datetime")
                    .HasColumnName("dateChanged");

                entity.Property(e => e.OldPassword)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("oldPassword");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Courseworkpasswords)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__COURSEWOR__userI__208CD6FA");
            });

            modelBuilder.Entity<Courseworksession>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.TimeIssued })
                    .HasName("PK__COURSEWO__2FD56C4D2BE65434");

                entity.ToTable("COURSEWORKSession");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.TimeIssued)
                    .HasColumnType("datetime")
                    .HasColumnName("timeIssued");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Courseworksessions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__COURSEWOR__userI__236943A5");
            });

            modelBuilder.Entity<Courseworkusername>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__COURSEWO__CB9A1CDF8A92A3CB");

                entity.ToTable("COURSEWORKUsername");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.CurrentPassword)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("currentPassword");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("emailAddress");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("lastName");
            });

            modelBuilder.Entity<VisitsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VisitsView");

                entity.Property(e => e.UserId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("userID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
