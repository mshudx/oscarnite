using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace Mshudx.OscarNite.Web.Models
{
    public class OscarNiteDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Question>()
                .HasKey(q => q.Id);

            builder.Entity<Question>()
                .Property(q => q.Id)
                .IsRequired();

            builder.Entity<Question>()
                .Property(q => q.Text)
                .IsRequired();

            builder.Entity<Question>()
                .Property(q => q.Order)
                .IsRequired();

            builder.Entity<Option>()
                .Property(q => q.Text)
                .IsRequired();


            builder.Entity<Option>()
                .Property(q => q.Text)
                .IsRequired();

            builder.Entity<Vote>()
                .HasMany(v => v.Answers)
                .WithOne(a => a.Vote)
                .IsRequired();

            builder.Entity<Vote>()
                .Property(q => q.Voter)
                .IsRequired();

            builder.Entity<Vote>()
                .HasKey(q => q.Id);

            builder.Entity<Vote>()
                .Property(q => q.Id)
                .IsRequired();

            builder.Entity<Vote>()
                .Property(q => q.Created)
                .IsRequired();

            builder.Entity<Answer>()
                .HasKey(q => q.Id);

            builder.Entity<Answer>()
                .Property(q => q.Id)
                .IsRequired();

            builder.Entity<Answer>()
                .HasOne(a => a.Vote)
                .WithMany(v => v.Answers)
                .IsRequired();

            builder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany()
                .IsRequired();

            builder.Entity<Answer>()
                .HasOne(a => a.Option)
                .WithMany()
                .IsRequired();
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Option> Options { get; set; }
    }
}
