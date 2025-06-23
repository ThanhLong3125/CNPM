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
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    MedicalHistory = table.Column<string>(type: "text", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Patients_Patient_ID",
                        column: x => x.Patient_ID,
                        principalTable: "Patients",
                        principalColumn: "Patient_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Full_name", "PasswordHash", "PhoneNumber", "PhysicianId", "Role", "Specialty" },
                values: new object[,]
                {
                    { new Guid("aa6e9b18-caee-4e3e-b8bf-4bf321f9e670"), "staff@aidims.com", "Staff", "$2a$11$aYrb575h.aYKaFhJpTIGMumTRAV2PcQxkp55WpAqAWovtukbpKJ1W", "", "BSAA6E9B", 3, null },
                    { new Guid("ce06d159-2784-4304-ad2c-397162014514"), "doctor2@aidims.com", "Hoang Thien", "$2a$11$5fsaR5um6.XCm3eJHFEjbOWqkeohkeUChE13aBzNkoVPE/BelWqd6", "", "BSCE06D1", 2, "Cardiology" },
                    { new Guid("eb860258-2620-450a-b193-cabc198a017f"), "admin@aidims.com", "Admin", "$2a$11$bq3CCVKcbMmYKJ018/uD5.ImHhabTZ7bqgX/bahA0gWO0.ScrsMoa", "", "BSEB8602", 1, null },
                    { new Guid("eec93066-89d3-4d17-b135-43742ee8090f"), "doctor3@aidims.com", "Khang To", "$2a$11$MJZ/KaNRPUANdQZ.B66jWOhcHAjsqhfM1V1qGtJRX0LYwEi9oDQzy", "", "BSEEC930", 2, "Neurology" },
                    { new Guid("f1545025-5fb4-40eb-8e4c-13b08d7ed2bd"), "doctor1@aidims.com", "Thanh Long", "$2a$11$LzLtj7RDkmaHErd72rK8f.0YM3pWHCObMdQ981pF4Q2pZIKnfzXpu", "", "BSF15450", 2, "Radiology" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_Patient_ID",
                table: "MedicalRecords",
                column: "Patient_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
