using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvcolGrpsCharity.Migrations
{
    /// <inheritdoc />
    public partial class fixingDonationsFKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Donors_DonorID",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Donors_SignedCharityGrps_SignedCharityGrpId",
                table: "Donors");

            migrationBuilder.RenameColumn(
                name: "SignedCharityGrpId",
                table: "Donors",
                newName: "DonationsDonationID");

            migrationBuilder.RenameIndex(
                name: "IX_Donors_SignedCharityGrpId",
                table: "Donors",
                newName: "IX_Donors_DonationsDonationID");

            migrationBuilder.RenameColumn(
                name: "DonorID",
                table: "Donations",
                newName: "SignedCharityGrpId");

            migrationBuilder.RenameIndex(
                name: "IX_Donations_DonorID",
                table: "Donations",
                newName: "IX_Donations_SignedCharityGrpId");

            migrationBuilder.AddColumn<int>(
                name: "DonationId",
                table: "Donors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_SignedCharityGrps_SignedCharityGrpId",
                table: "Donations",
                column: "SignedCharityGrpId",
                principalTable: "SignedCharityGrps",
                principalColumn: "CharityGrpID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donors_Donations_DonationsDonationID",
                table: "Donors",
                column: "DonationsDonationID",
                principalTable: "Donations",
                principalColumn: "DonationID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_SignedCharityGrps_SignedCharityGrpId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Donors_Donations_DonationsDonationID",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "DonationId",
                table: "Donors");

            migrationBuilder.RenameColumn(
                name: "DonationsDonationID",
                table: "Donors",
                newName: "SignedCharityGrpId");

            migrationBuilder.RenameIndex(
                name: "IX_Donors_DonationsDonationID",
                table: "Donors",
                newName: "IX_Donors_SignedCharityGrpId");

            migrationBuilder.RenameColumn(
                name: "SignedCharityGrpId",
                table: "Donations",
                newName: "DonorID");

            migrationBuilder.RenameIndex(
                name: "IX_Donations_SignedCharityGrpId",
                table: "Donations",
                newName: "IX_Donations_DonorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Donors_DonorID",
                table: "Donations",
                column: "DonorID",
                principalTable: "Donors",
                principalColumn: "DonorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donors_SignedCharityGrps_SignedCharityGrpId",
                table: "Donors",
                column: "SignedCharityGrpId",
                principalTable: "SignedCharityGrps",
                principalColumn: "CharityGrpID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
