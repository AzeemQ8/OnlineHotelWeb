using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineHotel.BLL.Migrations
{
    public partial class AddFacilityToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "RoomFacilities");

            migrationBuilder.AddColumn<int>(
                name: "FacilitiesId",
                table: "RoomFacilities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomFacilities_FacilitiesId",
                table: "RoomFacilities",
                column: "FacilitiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFacilities_Facilities_FacilitiesId",
                table: "RoomFacilities",
                column: "FacilitiesId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomFacilities_Facilities_FacilitiesId",
                table: "RoomFacilities");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropIndex(
                name: "IX_RoomFacilities_FacilitiesId",
                table: "RoomFacilities");

            migrationBuilder.DropColumn(
                name: "FacilitiesId",
                table: "RoomFacilities");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "RoomFacilities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
