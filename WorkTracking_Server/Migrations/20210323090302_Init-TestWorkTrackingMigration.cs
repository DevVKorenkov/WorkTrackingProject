using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkTracking_Server.Migrations
{
    public partial class InitTestWorkTrackingMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Access = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Complited_Work",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Who = table.Column<string>(nullable: true),
                    OspWork = table.Column<string>(nullable: true),
                    OspOrder = table.Column<string>(nullable: true),
                    OrderType = table.Column<string>(nullable: true),
                    OrderNum = table.Column<string>(nullable: true),
                    OldInv = table.Column<string>(nullable: true),
                    NewInv = table.Column<string>(nullable: true),
                    OldPCName = table.Column<string>(nullable: true),
                    OsType = table.Column<string>(nullable: true),
                    Why = table.Column<string>(nullable: true),
                    Results = table.Column<string>(nullable: true),
                    Brocken = table.Column<string>(nullable: true),
                    Pass = table.Column<string>(nullable: true),
                    NoActive = table.Column<string>(nullable: true),
                    WriteOffNum = table.Column<string>(nullable: true),
                    ModernNum = table.Column<string>(nullable: true),
                    ModernNewPc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complited_Work", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceName = table.Column<string>(nullable: true),
                    InvNumber = table.Column<string>(nullable: true),
                    OsName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Osp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Osp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OsType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Repairs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    OsName = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    SNumber = table.Column<string>(nullable: true),
                    InvNumber = table.Column<string>(nullable: true),
                    DiagNumber = table.Column<string>(nullable: true),
                    ScOks = table.Column<string>(nullable: true),
                    KaProvider = table.Column<string>(nullable: true),
                    KaRepair = table.Column<string>(nullable: true),
                    HandedOver = table.Column<string>(nullable: true),
                    Defect = table.Column<string>(nullable: true),
                    ShipmentDate = table.Column<DateTime>(nullable: true),
                    DaysOfRepair = table.Column<int>(nullable: false),
                    ReturnFromRepair = table.Column<DateTime>(nullable: true),
                    ProviderOrder = table.Column<string>(nullable: true),
                    RepairBill = table.Column<string>(nullable: true),
                    WarrantyBasis = table.Column<string>(nullable: true),
                    StartWarranty = table.Column<DateTime>(nullable: true),
                    Warranty = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repairs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RepairsStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairsStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScOks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScOks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Why",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Why", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Complited_Work");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Osp");

            migrationBuilder.DropTable(
                name: "OsType");

            migrationBuilder.DropTable(
                name: "Repairs");

            migrationBuilder.DropTable(
                name: "RepairsStatuses");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "ScOks");

            migrationBuilder.DropTable(
                name: "Why");
        }
    }
}
