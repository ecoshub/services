using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BillingService.Migrations
{
    public partial class InitialMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sale",
                columns: table => new
                {
                    saleId = table.Column<Guid>(nullable: false),
                    billId = table.Column<Guid>(nullable: false),
                    productRefId = table.Column<Guid>(nullable: false),
                    saleDate = table.Column<DateTime>(nullable: false),
                    saleAmount = table.Column<long>(nullable: false),
                    saleUnitPrice = table.Column<double>(nullable: false),
                    saleTotalPrice = table.Column<double>(nullable: false),
                    stockLeft = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale", x => x.saleId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sale");
        }
    }
}
