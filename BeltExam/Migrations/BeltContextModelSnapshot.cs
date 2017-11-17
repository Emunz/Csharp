using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BeltExam.Models;

namespace BeltExam.Migrations
{
    [DbContext(typeof(BeltContext))]
    partial class BeltContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("BeltExam.Models.Connection", b =>
                {
                    b.Property<int>("ConnectionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ConnectionStatus");

                    b.Property<int>("ReceiverId");

                    b.Property<int>("SenderId");

                    b.HasKey("ConnectionId");

                    b.HasIndex("SenderId");

                    b.ToTable("Connections");
                });

            modelBuilder.Entity("BeltExam.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BeltExam.Models.Connection", b =>
                {
                    b.HasOne("BeltExam.Models.User", "Sender")
                        .WithMany("Connections")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
