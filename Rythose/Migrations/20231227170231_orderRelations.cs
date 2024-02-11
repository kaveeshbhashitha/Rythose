using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Raythose.Migrations
{
    /// <inheritdoc />
    public partial class orderRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StaffName",
                table: "tbl_user",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_order_AircraftId",
                table: "tbl_order",
                column: "AircraftId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_order_Connectivity",
                table: "tbl_order",
                column: "Connectivity");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_order_CustomerId",
                table: "tbl_order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_order_Entertainment",
                table: "tbl_order",
                column: "Entertainment");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_order_Interior",
                table: "tbl_order",
                column: "Interior");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_order_Seating",
                table: "tbl_order",
                column: "Seating");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_order_tbl_aircraft_AircraftId",
                table: "tbl_order",
                column: "AircraftId",
                principalTable: "tbl_aircraft",
                principalColumn: "AircraftId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_order_tbl_customer_CustomerId",
                table: "tbl_order",
                column: "CustomerId",
                principalTable: "tbl_customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_order_tbl_sub_category_Connectivity",
                table: "tbl_order",
                column: "Connectivity",
                principalTable: "tbl_sub_category",
                principalColumn: "SubId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_order_tbl_sub_category_Entertainment",
                table: "tbl_order",
                column: "Entertainment",
                principalTable: "tbl_sub_category",
                principalColumn: "SubId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_order_tbl_sub_category_Interior",
                table: "tbl_order",
                column: "Interior",
                principalTable: "tbl_sub_category",
                principalColumn: "SubId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_order_tbl_sub_category_Seating",
                table: "tbl_order",
                column: "Seating",
                principalTable: "tbl_sub_category",
                principalColumn: "SubId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_order_tbl_aircraft_AircraftId",
                table: "tbl_order");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_order_tbl_customer_CustomerId",
                table: "tbl_order");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_order_tbl_sub_category_Connectivity",
                table: "tbl_order");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_order_tbl_sub_category_Entertainment",
                table: "tbl_order");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_order_tbl_sub_category_Interior",
                table: "tbl_order");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_order_tbl_sub_category_Seating",
                table: "tbl_order");

            migrationBuilder.DropIndex(
                name: "IX_tbl_order_AircraftId",
                table: "tbl_order");

            migrationBuilder.DropIndex(
                name: "IX_tbl_order_Connectivity",
                table: "tbl_order");

            migrationBuilder.DropIndex(
                name: "IX_tbl_order_CustomerId",
                table: "tbl_order");

            migrationBuilder.DropIndex(
                name: "IX_tbl_order_Entertainment",
                table: "tbl_order");

            migrationBuilder.DropIndex(
                name: "IX_tbl_order_Interior",
                table: "tbl_order");

            migrationBuilder.DropIndex(
                name: "IX_tbl_order_Seating",
                table: "tbl_order");

            migrationBuilder.DropColumn(
                name: "StaffName",
                table: "tbl_user");
        }
    }
}
