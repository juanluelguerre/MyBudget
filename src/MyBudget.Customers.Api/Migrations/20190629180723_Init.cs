using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBudget.Customers.Api.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
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
                columns: new[] { "Id", "Active", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, (byte)1, "Juan Luis", "Guerrero Minero" },
                    { 2, (byte)1, "Francisco", "Ruiz Vázquez" },
                    { 3, (byte)1, "Eva", "Perez Moreno" },
                    { 4, (byte)1, "Maria", "Serrano Sanchez" }
                });

            migrationBuilder.InsertData(
                table: "CustomersAccounts",
                columns: new[] { "CustomerId", "BankAccount", "MarkAsDefault" },
                values: new object[] { 1, "ES12-1234-1234-1234567890", (byte)1 });

            migrationBuilder.InsertData(
                table: "CustomersAccounts",
                columns: new[] { "CustomerId", "BankAccount", "MarkAsDefault" },
                values: new object[] { 1, "ES13-4321-4321-0987654321", (byte)0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomersAccounts");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
