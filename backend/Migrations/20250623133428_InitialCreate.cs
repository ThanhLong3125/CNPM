using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Patient_GUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Patient_ID = table.Column<string>(type: "text", nullable: false),
                    Full_name = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    MedicalHistory = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Patient_GUID);
                    table.UniqueConstraint("AK_Patients_Patient_ID", x => x.Patient_ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PhysicianId = table.Column<string>(type: "text", nullable: false),
                    Full_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Specialty = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                columns: table => new
                {
                    Record_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicalRecordId = table.Column<string>(type: "text", nullable: false),
                    Patient_ID = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AssignedPhysicianId = table.Column<string>(type: "text", nullable: false),
                    Symptoms = table.Column<string>(type: "text", nullable: false),
                    IsPriority = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.Record_ID);
                    table.UniqueConstraint("AK_MedicalRecords_MedicalRecordId", x => x.MedicalRecordId);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Patients_Patient_ID",
                        column: x => x.Patient_ID,
                        principalTable: "Patients",
                        principalColumn: "Patient_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diagnoses",
                columns: table => new
                {
                    DiagnosisId = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicalRecordId = table.Column<string>(type: "text", nullable: false),
                    DiagnosedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnoses", x => x.DiagnosisId);
                    table.ForeignKey(
                        name: "FK_Diagnoses_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "MedicalRecordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageId = table.Column<string>(type: "text", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    DiagnosisId = table.Column<string>(type: "text", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AIAnalysis = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ImageName = table.Column<string>(type: "text", nullable: true),
                    DiagnosisId1 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.UniqueConstraint("AK_Images_ImageId", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Images_Diagnoses_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnoses",
                        principalColumn: "DiagnosisId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Images_Diagnoses_DiagnosisId1",
                        column: x => x.DiagnosisId1,
                        principalTable: "Diagnoses",
                        principalColumn: "DiagnosisId");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Full_name", "PasswordHash", "PhoneNumber", "PhysicianId", "Role", "Specialty" },
                values: new object[,]
                {
                    { new Guid("821c6152-fdce-4293-886c-f9e6dac9b11e"), "doctor2@aidims.com", "Hoang Thien", "$2a$11$.FNgzPe0eDj5Bw92GYA.fuXZ8.I6uuvi821utNtPbkivGSphBtqbe", "", "BS821C61", 2, "Cardiology" },
                    { new Guid("84522cc8-a321-420c-b277-8ae30ec1c51a"), "staff@aidims.com", "Staff", "$2a$11$vzO7rmQ7jKW4JDp/RRqpbOgjKeEMgwmVsAoauwyyLnVWZuaqDrhom", "", "BS84522C", 3, null },
                    { new Guid("932dd0ad-7f28-4b37-b57e-0e8bc696184c"), "doctor1@aidims.com", "Thanh Long", "$2a$11$VBMHa0eyfADdTJftAg2gouDjrucFwsbJDEC2CkjiKC4xR.tHL6k6m", "", "BS932DD0", 2, "Radiology" },
                    { new Guid("990a8d91-c438-40ae-bb8c-793370657ed9"), "admin@aidims.com", "Admin", "$2a$11$zTEqAlxlMrPEiDmpRzgXBeUMnNncy650ihHnbxWa2.KWT2e6W7mou", "", "BS990A8D", 1, null },
                    { new Guid("c020ba1a-b3b3-4359-8314-3b20b89e71bd"), "doctor3@aidims.com", "Khang To", "$2a$11$l4Y2g8gyQAJ6TrfZzsDFyOGy9DPMniggs0opGN2zjVlf9FZ5nP026", "", "BSC020BA", 2, "Neurology" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_MedicalRecordId",
                table: "Diagnoses",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_DiagnosisId",
                table: "Images",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_DiagnosisId1",
                table: "Images",
                column: "DiagnosisId1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_Patient_ID",
                table: "MedicalRecords",
                column: "Patient_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Diagnoses");

            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
