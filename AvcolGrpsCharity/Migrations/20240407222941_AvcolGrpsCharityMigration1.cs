using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvcolGrpsCharity.Migrations
{
    /// <inheritdoc />
    public partial class AvcolGrpsCharityMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DonationDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DonorID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemberID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Member_email",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Member_name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Member_phonenum",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SignedCharityGrps",
                columns: table => new
                {
                    CharityGrpID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChartyGrp_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CharityGrp_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CharityGrp_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CharityGrp_phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignedCharityGrps", x => x.CharityGrpID);
                });

            migrationBuilder.CreateTable(
                name: "CharityCategory",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SignedCharityGrpId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharityCategory", x => x.CategoryID);
                    table.ForeignKey(
                        name: "FK_CharityCategory_SignedCharityGrps_SignedCharityGrpId",
                        column: x => x.SignedCharityGrpId,
                        principalTable: "SignedCharityGrps",
                        principalColumn: "CharityGrpID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharityGrpStaff",
                columns: table => new
                {
                    StaffmemberID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffMember_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffMember_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffMember_phonenum = table.Column<int>(type: "int", nullable: false),
                    SignedCharityGrpId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharityGrpStaff", x => x.StaffmemberID);
                    table.ForeignKey(
                        name: "FK_CharityGrpStaff_SignedCharityGrps_SignedCharityGrpId",
                        column: x => x.SignedCharityGrpId,
                        principalTable: "SignedCharityGrps",
                        principalColumn: "CharityGrpID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Donors",
                columns: table => new
                {
                    DonorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Donor_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Donor_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SignedCharityGrpId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donors", x => x.DonorID);
                    table.ForeignKey(
                        name: "FK_Donors_SignedCharityGrps_SignedCharityGrpId",
                        column: x => x.SignedCharityGrpId,
                        principalTable: "SignedCharityGrps",
                        principalColumn: "CharityGrpID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    DonationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonationMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DonorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.DonationID);
                    table.ForeignKey(
                        name: "FK_Donations_Donors_DonorID",
                        column: x => x.DonorID,
                        principalTable: "Donors",
                        principalColumn: "DonorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DonorID",
                table: "AspNetUsers",
                column: "DonorID");

            migrationBuilder.CreateIndex(
                name: "IX_CharityCategory_SignedCharityGrpId",
                table: "CharityCategory",
                column: "SignedCharityGrpId");

            migrationBuilder.CreateIndex(
                name: "IX_CharityGrpStaff_SignedCharityGrpId",
                table: "CharityGrpStaff",
                column: "SignedCharityGrpId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_DonorID",
                table: "Donations",
                column: "DonorID");

            migrationBuilder.CreateIndex(
                name: "IX_Donors_SignedCharityGrpId",
                table: "Donors",
                column: "SignedCharityGrpId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Donors_DonorID",
                table: "AspNetUsers",
                column: "DonorID",
                principalTable: "Donors",
                principalColumn: "DonorID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Donors_DonorID",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CharityCategory");

            migrationBuilder.DropTable(
                name: "CharityGrpStaff");

            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "Donors");

            migrationBuilder.DropTable(
                name: "SignedCharityGrps");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DonorID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DonationDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DonorID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MemberID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Member_email",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Member_name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Member_phonenum",
                table: "AspNetUsers");
        }
    }
}
