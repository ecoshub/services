using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductService.Migrations
{
    public partial class InitMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    productId = table.Column<Guid>(nullable: false),
                    productName = table.Column<string>(nullable: false),
                    productStock = table.Column<long>(nullable: false),
                    productPrice = table.Column<double>(nullable: false),
                    productDescription = table.Column<string>(nullable: true),
                    productRegisterDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.productId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product");
        }
    }
}
