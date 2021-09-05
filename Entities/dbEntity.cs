
using ideadune_pos.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ideadune_pos.Entities
{
    public class dbEntity:DbContext
    {
        public dbEntity()
        {

        }
        public dbEntity(DbContextOptions<dbEntity> options) : base(options)
        {
        }
        public virtual DbSet<Login> login { get; set; }
        public virtual DbSet<BOAccounts> accountsInfomation { get; set; }
        public virtual DbSet<BONote> notes { get; set; }
        public virtual DbSet<BOComment> commentInfo { get; set; }
        public virtual DbSet<Choice> mentionInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BONote>();
            modelBuilder.Entity<BOAccounts>().HasNoKey();
            modelBuilder.Entity<BOComment>();
            modelBuilder.Entity<Choice>();

            modelBuilder.Entity<Login>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.firstName)
                    .HasColumnName("firstName")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsRequired(true);

                entity.Property(e => e.lastName)
                    .HasColumnName("lastName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.createDt)
                    .HasColumnName("createDt")
                    .IsUnicode(false);

                entity.Property(e => e.userId)
                    .HasColumnName("userId")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .IsRequired(true);
                
            });
        }
    }
}
