using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieHub.API.Migrations
{
    /// <inheritdoc />
    public partial class AddBuferMinutesFieldToShowTimesEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BufferMinutes",
                table: "ShowTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BufferMinutes",
                table: "ShowTimes");
        }
    }
}
