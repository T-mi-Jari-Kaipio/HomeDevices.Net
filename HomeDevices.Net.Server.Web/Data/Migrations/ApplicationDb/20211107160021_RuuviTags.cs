using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeDevices.Net.Server.Web.Data.Migrations.ApplicationDb
{
    public partial class RuuviTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RuuviTags",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataFormat = table.Column<int>(type: "int", nullable: false),
                    Humidity = table.Column<double>(type: "double", nullable: true),
                    Temperature = table.Column<double>(type: "double", nullable: true),
                    AirPressure = table.Column<double>(type: "double", nullable: true),
                    AccelerationX = table.Column<double>(type: "double", nullable: true),
                    AccelerationY = table.Column<double>(type: "double", nullable: true),
                    AccelerationZ = table.Column<double>(type: "double", nullable: true),
                    BatteryVoltage = table.Column<double>(type: "double", nullable: true),
                    TxPower = table.Column<double>(type: "double", nullable: true),
                    MovementCounter = table.Column<double>(type: "double", nullable: true),
                    MeasurementSequenceNumber = table.Column<double>(type: "double", nullable: true),
                    MacAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuuviTags", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RuuviTags");
        }
    }
}
