using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                    ContactInfo = table.Column<string>(type: "text", nullable: true),
                    MedicalHistory = table.Column<string>(type: "text", nullable: true)
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
                    Symptoms = table.Column<string>(type: "text", nullable: false),
                    IsPriority = table.Column<bool>(type: "boolean", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Notification_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    User_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Notification_ID);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_Patient_ID",
                table: "MedicalRecords",
                column: "Patient_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_User_ID",
                table: "Notifications",
                column: "User_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
