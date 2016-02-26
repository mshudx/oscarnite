using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Mshudx.OscarNite.Web.Models;

namespace Mshudx.OscarNite.Web.Migrations
{
    [DbContext(typeof(OscarNiteDbContext))]
    [Migration("20160226120842_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Mshudx.OscarNite.Web.Models.Answer", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("OptionId")
                        .IsRequired();

                    b.Property<string>("QuestionId")
                        .IsRequired();

                    b.Property<string>("VoteId")
                        .IsRequired();

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Mshudx.OscarNite.Web.Models.Option", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Mshudx.OscarNite.Web.Models.Question", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Mshudx.OscarNite.Web.Models.Vote", b =>
                {
                    b.Property<string>("Id");

                    b.Property<DateTimeOffset>("Created");

                    b.Property<string>("Voter")
                        .IsRequired();

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Mshudx.OscarNite.Web.Models.Answer", b =>
                {
                    b.HasOne("Mshudx.OscarNite.Web.Models.Option")
                        .WithMany()
                        .HasForeignKey("OptionId");

                    b.HasOne("Mshudx.OscarNite.Web.Models.Question")
                        .WithMany()
                        .HasForeignKey("QuestionId");

                    b.HasOne("Mshudx.OscarNite.Web.Models.Vote")
                        .WithMany()
                        .HasForeignKey("VoteId");
                });
        }
    }
}
