using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Raythose.Migrations
{
    /// <inheritdoc />
    public partial class ItemsForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_tbl_items_MainId",
                table: "tbl_items",
                column: "MainId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_items_SubId",
                table: "tbl_items",
                column: "SubId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_items_tbl_main_category_MainId",
                table: "tbl_items",
                column: "MainId",
                principalTable: "tbl_main_category",
                principalColumn: "MainId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_items_tbl_sub_category_SubId",
                table: "tbl_items",
                column: "SubId",
                principalTable: "tbl_sub_category",
                principalColumn: "SubId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_items_tbl_main_category_MainId",
                table: "tbl_items");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_items_tbl_sub_category_SubId",
                table: "tbl_items");

            migrationBuilder.DropIndex(
                name: "IX_tbl_items_MainId",
                table: "tbl_items");

            migrationBuilder.DropIndex(
                name: "IX_tbl_items_SubId",
                table: "tbl_items");
        }
    }
}
