using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBudget.Api.Application.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    CustomerFrom = table.Column<DateTime>(nullable: true),
                    Active = table.Column<byte>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomersAccounts",
                columns: table => new
                {
                    BankAccount = table.Column<string>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    MarkAsDefault = table.Column<byte>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomersAccounts", x => new { x.CustomerId, x.BankAccount });
                    table.UniqueConstraint("AK_CustomersAccounts_BankAccount", x => x.BankAccount);
                    table.ForeignKey(
                        name: "FK_CustomersAccounts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Active", "CustomerFrom", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, (byte)1, new DateTime(2019, 6, 16, 23, 6, 18, 49, DateTimeKind.Local).AddTicks(6689), "Juan Luis", "Guerrero Minero" },
                    { 2, (byte)1, new DateTime(2019, 5, 17, 23, 6, 18, 62, DateTimeKind.Local).AddTicks(4186), "Francisco", "Ruiz Vázquez" },
                    { 3, (byte)1, new DateTime(2019, 4, 27, 23, 6, 18, 62, DateTimeKind.Local).AddTicks(4303), "Eva", "Perez Moreno" },
                    { 4, (byte)1, new DateTime(2019, 6, 16, 23, 6, 18, 62, DateTimeKind.Local).AddTicks(4310), "Maria", "Serrano Sanchez" }
                });

            migrationBuilder.InsertData(
                table: "CustomersAccounts",
                columns: new[] { "CustomerId", "BankAccount", "MarkAsDefault" },
                values: new object[] { 1, "ES12-1234-1234-12345678-1234", (byte)1 });

            migrationBuilder.InsertData(
                table: "CustomersAccounts",
                columns: new[] { "CustomerId", "BankAccount", "MarkAsDefault" },
                values: new object[] { 1, "ES13-4321-4321-12345678-4321", (byte)0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "CustomersAccounts");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
