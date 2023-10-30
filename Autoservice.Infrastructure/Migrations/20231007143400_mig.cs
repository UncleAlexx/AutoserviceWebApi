#nullable disable

namespace Autoservice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Client_ClientID",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Part_Client_ClientID",
                table: "Part");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientID",
                table: "Part",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientID",
                table: "Car",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Client_ClientID",
                table: "Car",
                column: "ClientID",
                principalTable: "Client",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Part_Client_ClientID",
                table: "Part",
                column: "ClientID",
                principalTable: "Client",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Client_ClientID",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Part_Client_ClientID",
                table: "Part");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientID",
                table: "Part",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientID",
                table: "Car",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Client_ClientID",
                table: "Car",
                column: "ClientID",
                principalTable: "Client",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Part_Client_ClientID",
                table: "Part",
                column: "ClientID",
                principalTable: "Client",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
