using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IDSEmpty.sakila
{
    public partial class CSATMContext : DbContext
    {
        public CSATMContext()
        {
        }

        public CSATMContext(DbContextOptions<CSATMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Idstable> Idstable { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<PersistantGrantItem> PGI { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("Redacted");

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Idstable>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("idstable");

                entity.Property(e => e.Balance)
                    .HasColumnName("BALANCE")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Pword)
                    .HasColumnName("PWORD")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Salt)
                    .HasColumnName("SALT")
                    .HasMaxLength(255)
                    .IsUnicode(false);
                
                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasMaxLength(255)
                    .IsUnicode(false);
                
                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("USERID")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("users");

                entity.Property(e => e.Balance).HasColumnType("int(11)");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<PersistantGrantItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("persistedgrantsss");

                entity.Property(e => e.CLIENT_ID)
                   .HasColumnName("CLIENT_ID")
                   .HasMaxLength(255);

                entity.Property(e => e.CREATION_TIME)
                    .HasColumnName("CREATION_TIME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DATAI)
                    .HasColumnName("DATAI")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EXPIRATION)
                    .HasColumnName("EXPIRATION")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ID)
                    .HasColumnName("Key")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SUBJECT_ID)
                    .HasColumnName("SUBJECT_ID")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
