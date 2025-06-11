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
                    Patient_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Full_name = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Patient_ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    Patient_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AssignedPhysicianId = table.Column<Guid>(type: "uuid", nullable: false),
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
                columns: new[] { "Id", "Email", "Full_name", "PasswordHash", "PhoneNumber", "Role", "Specialty" },
                values: new object[,]
                {
                    { new Guid("5bd036c6-f4d4-4d2f-ae50-628ed9a6d5a6"), "staff@aidims.com", "Staff", "$2a$11$ewJjFOZtjT8ASv/CMdqFa.G6DTqj.qv7FDqIZLpvptlVdpMn4QbJ6", "", 3, null },
                    { new Guid("898fdb73-a13b-4376-989e-14601fb5420f"), "doctor1@aidims.com", "Thanh Long", "$2a$11$A1hR7VzihaNKkqukxBFgluPpaakEyTXLgJPWIq9qBvDyfCm6IdBaq", "", 2, "Radiology" },
                    { new Guid("a24cd1f8-8d05-4adb-88ca-605ab141c4bc"), "admin@aidims.com", "Admin", "$2a$11$VP.53iTGH3L7/p9mBE1cq.LitM3lcWaU9v/xbhf1S7ILa/hkpfzwu", "", 1, null },
                    { new Guid("ce95fc6b-0275-4ba4-98a3-c3b7a0d35171"), "doctor2@aidims.com", "Hoang Thien", "$2a$11$BcdAhalq1ZlhpPQmadZXRu5vcIzyTsQKK4nXYsfqiVpHOioV.uhUO", "", 2, "Cardiology" },
                    { new Guid("ead73ef9-17dc-4249-a2fd-1d4a75700e2c"), "doctor3@aidims.com", "Khang To", "$2a$11$czbPCEif7w3R6VlH1cWM9u4hexCNfl45N8EGFTvG.Sir/oWni02tC", "", 2, "Neurology" }
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
