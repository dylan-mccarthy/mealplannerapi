using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealPlannerApi.Migrations
{
    /// <inheritdoc />
    public partial class ModifyMealTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Macros_MacrosId",
                table: "Meals");

            migrationBuilder.DropTable(
                name: "Macros");

            migrationBuilder.DropIndex(
                name: "IX_Meals_MacrosId",
                table: "Meals");

            migrationBuilder.RenameColumn(
                name: "MacrosId",
                table: "Meals",
                newName: "Protein");

            migrationBuilder.AddColumn<int>(
                name: "Carbs",
                table: "Meals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Fat",
                table: "Meals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Carbs",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "Fat",
                table: "Meals");

            migrationBuilder.RenameColumn(
                name: "Protein",
                table: "Meals",
                newName: "MacrosId");

            migrationBuilder.CreateTable(
                name: "Macros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Carbs = table.Column<int>(type: "INTEGER", nullable: false),
                    Fat = table.Column<int>(type: "INTEGER", nullable: false),
                    Protein = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Macros", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meals_MacrosId",
                table: "Meals",
                column: "MacrosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Macros_MacrosId",
                table: "Meals",
                column: "MacrosId",
                principalTable: "Macros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
