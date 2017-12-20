using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace K9CleanSweep.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    ClientID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: false),
                    AmountOfDogs = table.Column<int>(nullable: false),
                    ClientName = table.Column<string>(nullable: false),
                    ClientUserName = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 150, nullable: false),
                    JoinDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    Notes = table.Column<string>(maxLength: 400, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    PostalCode = table.Column<string>(nullable: false),
                    Service = table.Column<string>(nullable: false),
                    ServiceDateTime = table.Column<string>(nullable: true, defaultValue: "no date"),
                    YardSqFootage = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.ClientID);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmpAddress = table.Column<string>(nullable: true),
                    EmpName = table.Column<string>(nullable: true),
                    EmpPassword = table.Column<string>(nullable: true),
                    EmpPostalCode = table.Column<string>(nullable: true),
                    EmpStartDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    EmpUserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    ReviewID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Author = table.Column<string>(nullable: true),
                    ClientID = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: false),
                    ReviewDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    Title = table.Column<string>(nullable: false),
                    starRating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reviews", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK_reviews_clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reviews_ClientID",
                table: "reviews",
                column: "ClientID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "clients");
        }
    }
}
