using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Raythose.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_aircraft",
                columns: table => new
                {
                    AircraftId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AircraftType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Passengers = table.Column<int>(type: "int", nullable: false),
                    Baggage = table.Column<int>(type: "int", nullable: false),
                    CabinWidth = table.Column<float>(type: "real", nullable: false),
                    CabinHeight = table.Column<float>(type: "real", nullable: false),
                    CabinLength = table.Column<float>(type: "real", nullable: false),
                    Range = table.Column<float>(type: "real", nullable: false),
                    Speed = table.Column<float>(type: "real", nullable: false),
                    Fuel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrontImage1 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    FrontImage2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    FrontImage3 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    InnerImage1 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    InnerImage2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    InnerImage3 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    SeatingImage = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_aircraft", x => x.AircraftId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ZipCode = table.Column<int>(type: "int", nullable: false),
                    LicenseCard = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VerificationCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_essential_items",
                columns: table => new
                {
                    EssentialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EssentialType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EssentialName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EssentialQuantity = table.Column<int>(type: "int", nullable: false),
                    EssentialDate = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EssentialStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_essential_items", x => x.EssentialId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_items",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MainId = table.Column<int>(type: "int", nullable: false),
                    SubId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Vendor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Date = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Stock = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_items", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_main_category",
                columns: table => new
                {
                    MainId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainCategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_main_category", x => x.MainId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_manufacture",
                columns: table => new
                {
                    ManufactureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Seating = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Interior = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Connectivity = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Entertainment = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Airframe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Powerplant = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Avionics = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Miscellaneous = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_manufacture", x => x.ManufactureId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AircraftId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Seating = table.Column<int>(type: "int", nullable: false),
                    Interior = table.Column<int>(type: "int", nullable: false),
                    Connectivity = table.Column<int>(type: "int", nullable: false),
                    Entertainment = table.Column<int>(type: "int", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AircraftPrice = table.Column<float>(type: "real", nullable: false),
                    SeatingPrice = table.Column<float>(type: "real", nullable: false),
                    InteriorPrice = table.Column<float>(type: "real", nullable: false),
                    ConnectivityPrice = table.Column<float>(type: "real", nullable: false),
                    EntertainmentPrice = table.Column<float>(type: "real", nullable: false),
                    VAT = table.Column<float>(type: "real", nullable: false),
                    FinalAmount = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_order", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_sub_category",
                columns: table => new
                {
                    SubId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_sub_category", x => x.SubId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_user",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_user", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_aircraft");

            migrationBuilder.DropTable(
                name: "tbl_customer");

            migrationBuilder.DropTable(
                name: "tbl_essential_items");

            migrationBuilder.DropTable(
                name: "tbl_items");

            migrationBuilder.DropTable(
                name: "tbl_main_category");

            migrationBuilder.DropTable(
                name: "tbl_manufacture");

            migrationBuilder.DropTable(
                name: "tbl_order");

            migrationBuilder.DropTable(
                name: "tbl_sub_category");

            migrationBuilder.DropTable(
                name: "tbl_user");
        }
    }
}
