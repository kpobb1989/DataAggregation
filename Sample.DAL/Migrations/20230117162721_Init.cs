using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sample.DAL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Licenses",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licenses", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Source = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Version = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => new { x.Source, x.Name, x.Version });
                });

            migrationBuilder.CreateTable(
                name: "PackageToLicense",
                columns: table => new
                {
                    PackageName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PackageSource = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PackageVersion = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LicenseName = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageToLicense", x => new { x.LicenseName, x.PackageSource, x.PackageName, x.PackageVersion });
                    table.ForeignKey(
                        name: "FK_PackageToLicense_Licenses_LicenseName",
                        column: x => x.LicenseName,
                        principalTable: "Licenses",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageToLicense_Packages_PackageSource_PackageName_PackageVersion",
                        columns: x => new { x.PackageSource, x.PackageName, x.PackageVersion },
                        principalTable: "Packages",
                        principalColumns: new[] { "Source", "Name", "Version" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PackageToLicense_PackageSource_PackageName_PackageVersion",
                table: "PackageToLicense",
                columns: new[] { "PackageSource", "PackageName", "PackageVersion" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackageToLicense");

            migrationBuilder.DropTable(
                name: "Licenses");

            migrationBuilder.DropTable(
                name: "Packages");
        }
    }
}
