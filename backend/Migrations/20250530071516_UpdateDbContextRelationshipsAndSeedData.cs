using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbContextRelationshipsAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Patients_Patient_ID",
                table: "MedicalRecords");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("378c9c57-ba1a-44c0-a0b5-fa7eac2b1060"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("898b7b4c-b505-4a02-ae2c-1ec86d93e3b1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9ef843f0-73b5-4ddc-8794-2fd9850e0a8c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9f648053-4ba9-48c0-be73-8633bea2f1ac"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ba956c08-0c0c-4c5c-a2f4-34b1b31d5d5e"));

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Symptoms",
                table: "MedicalRecords",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "AssignedPhysicianId",
                table: "MedicalRecords",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByStaffId",
                table: "MedicalRecords",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisSummary",
                table: "MedicalRecords",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "MedicalRecords",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastUpdatedByUserId",
                table: "MedicalRecords",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhysicianNotes",
                table: "MedicalRecords",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicalRecordId = table.Column<Guid>(type: "uuid", nullable: true),
                    FilePath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    UploadDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UploadedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DoctorImageNotes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Record_ID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Images_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Patient_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Images_Users_UploadedByUserId",
                        column: x => x.UploadedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Full_name", "PasswordHash", "Role", "Specialty" },
                values: new object[,]
                {
                    { new Guid("18bb4e84-8655-4075-bf6f-89f2372f1911"), "doctor2@aidims.com", "Hoang Thien", "$2a$11$W6Rp5NhinUSfBfBI7QQ6seroT8LD2MiVScjcMkpfIkKMRVMVIKkaK", 2, "Cardiology" },
                    { new Guid("2e913de5-0a19-4e2e-b33f-12d77a893b12"), "staff@aidims.com", "Staff", "$2a$11$a4yV4foN7Ksnwe0SZ679ROlYyxHHmkm8gs/kzkaSmNIgRhpQIk9X2", 3, null },
                    { new Guid("65d21bd2-b9db-4992-8c88-10b62ff16c00"), "doctor1@aidims.com", "Thanh Long", "$2a$11$04BAGrn4VPOSDLTEOinbZeZ17hpDS7lhLVIahxY1FPQ6EwhRKKqEW", 2, "Radiology" },
                    { new Guid("eb1a71be-dccf-4719-aaeb-8026769a1258"), "admin@aidims.com", "Admin", "$2a$11$uj1ZL4AFJapwaRkmQ10hf.g7On8tr4SY1f2gd961ojOGxpFVWABF2", 1, null },
                    { new Guid("ee483019-5f5f-4b76-ae34-b504a1602b31"), "doctor3@aidims.com", "Khang To", "$2a$11$EF1jMlmcLNMwSm3ISRhYnujPFKfOarZBmf67hXMUNKJQsK4EQIrGy", 2, "Neurology" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_AssignedPhysicianId",
                table: "MedicalRecords",
                column: "AssignedPhysicianId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_CreatedByStaffId",
                table: "MedicalRecords",
                column: "CreatedByStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_MedicalRecordId",
                table: "Images",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_PatientId",
                table: "Images",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_UploadedByUserId",
                table: "Images",
                column: "UploadedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Patients_Patient_ID",
                table: "MedicalRecords",
                column: "Patient_ID",
                principalTable: "Patients",
                principalColumn: "Patient_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Users_AssignedPhysicianId",
                table: "MedicalRecords",
                column: "AssignedPhysicianId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Users_CreatedByStaffId",
                table: "MedicalRecords",
                column: "CreatedByStaffId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Patients_Patient_ID",
                table: "MedicalRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Users_AssignedPhysicianId",
                table: "MedicalRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Users_CreatedByStaffId",
                table: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_AssignedPhysicianId",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_CreatedByStaffId",
                table: "MedicalRecords");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("18bb4e84-8655-4075-bf6f-89f2372f1911"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2e913de5-0a19-4e2e-b33f-12d77a893b12"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("65d21bd2-b9db-4992-8c88-10b62ff16c00"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("eb1a71be-dccf-4719-aaeb-8026769a1258"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ee483019-5f5f-4b76-ae34-b504a1602b31"));

            migrationBuilder.DropColumn(
                name: "AssignedPhysicianId",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "CreatedByStaffId",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "DiagnosisSummary",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "LastUpdatedByUserId",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "PhysicianNotes",
                table: "MedicalRecords");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Symptoms",
                table: "MedicalRecords",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Full_name", "PasswordHash", "PhoneNumber", "Role", "Specialty" },
                values: new object[,]
                {
                    { new Guid("378c9c57-ba1a-44c0-a0b5-fa7eac2b1060"), "admin@aidims.com", "Admin", "$2a$11$2.pAd0rkvzkbgGj0.ofSveplmdqu6w5Y4Y8enYPopEtUEpWdUNNUS", "", 1, null },
                    { new Guid("898b7b4c-b505-4a02-ae2c-1ec86d93e3b1"), "staff@aidims.com", "Staff", "$2a$11$MyqkNc9VqnJ8yFS2fFfHDuz8QLyIMBQpEguaFScTqx1/0P9XhiVC6", "", 3, null },
                    { new Guid("9ef843f0-73b5-4ddc-8794-2fd9850e0a8c"), "doctor1@aidims.com", "Thanh Long", "$2a$11$TwS3r49AOhrT2Ykie.PWnudt/nyiHgCsbzhHmhZ65enqFQlRm4NES", "", 2, "Radiology" },
                    { new Guid("9f648053-4ba9-48c0-be73-8633bea2f1ac"), "doctor2@aidims.com", "Hoang Thien", "$2a$11$WPWnlvkcsmzcYdIMZ7Lin.dG8Pnpllj9grHPH7U/sVjVqMOAne9Wu", "", 2, "Cardiology" },
                    { new Guid("ba956c08-0c0c-4c5c-a2f4-34b1b31d5d5e"), "doctor3@aidims.com", "Khang To", "$2a$11$vEyQG5y2rgctkem5CAULeeCICQmrkVqxyjPme6cmfn1X5Z2ed03qO", "", 2, "Neurology" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Patients_Patient_ID",
                table: "MedicalRecords",
                column: "Patient_ID",
                principalTable: "Patients",
                principalColumn: "Patient_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
