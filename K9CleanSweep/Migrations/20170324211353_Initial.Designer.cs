using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using K9CleanSweep.Models;

namespace K9CleanSweep.Migrations
{
    [DbContext(typeof(CleanSweepContext))]
    [Migration("20170324211353_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("K9CleanSweep.Models.Client", b =>
                {
                    b.Property<int>("ClientID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<int>("AmountOfDogs");

                    b.Property<string>("ClientName")
                        .IsRequired();

                    b.Property<string>("ClientUserName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<DateTime>("JoinDate");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasMaxLength(400);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("PostalCode")
                        .IsRequired();

                    b.Property<string>("Service")
                        .IsRequired();

                    b.Property<string>("ServiceDateTime");

                    b.Property<decimal>("YardSqFootage");

                    b.HasKey("ClientID");

                    b.ToTable("clients");
                });

            modelBuilder.Entity("K9CleanSweep.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EmpAddress");

                    b.Property<string>("EmpName");

                    b.Property<string>("EmpPassword");

                    b.Property<string>("EmpPostalCode");

                    b.Property<DateTime>("EmpStartDate");

                    b.Property<string>("EmpUserName");

                    b.HasKey("EmployeeID");

                    b.ToTable("employees");
                });

            modelBuilder.Entity("K9CleanSweep.Models.Review", b =>
                {
                    b.Property<int>("ReviewID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<int>("ClientID");

                    b.Property<string>("Message")
                        .IsRequired();

                    b.Property<DateTime>("ReviewDate");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("starRating");

                    b.HasKey("ReviewID");

                    b.HasIndex("ClientID");

                    b.ToTable("reviews");
                });

            modelBuilder.Entity("K9CleanSweep.Models.Review", b =>
                {
                    b.HasOne("K9CleanSweep.Models.Client")
                        .WithMany("reviews")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
