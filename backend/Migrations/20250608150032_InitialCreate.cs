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
                    AssignedPhysicianId = table.Column<Guid>(type: "uuid", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Diagnoses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicalRecordId = table.Column<Guid>(type: "uuid", nullable: false),
                    DiagnosedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnoses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diagnoses_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Record_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Full_name", "PasswordHash", "PhoneNumber", "Role", "Specialty" },
                values: new object[,]
                {
                    { new Guid("2bb3186e-c9be-4289-a0ea-925b80625033"), "doctor2@aidims.com", "Hoang Thien", "$2a$11$7HloDhzXEvCYewLhAS5Yd.VloXTlBHnR1rF81ujEo.tTQgNZi18QK", "", 2, "Cardiology" },
                    { new Guid("3ba675d4-3d54-4bd9-bfb7-6d5ee8c2971b"), "admin@aidims.com", "Admin", "$2a$11$mp6bkFZy7UKeLoidfIrIM.RcufdIwzXXeZbV8.U6zUp7Jti5GgZHe", "", 1, null },
                    { new Guid("87249296-fa86-4ab2-807a-60ea32ecb2c8"), "staff@aidims.com", "Staff", "$2a$11$V0dvADwMR8uBFXct24HnBOhfk0F8KFhcJTALVrtDYcq08WYuC3qy.", "", 3, null },
                    { new Guid("f096938c-2989-452d-9b47-6d59aa11e817"), "doctor1@aidims.com", "Thanh Long", "$2a$11$tVb1EZvpKqE.k2ln5IBve.M3oHJNr6B0TDmC7jzIsd6e25FJf2yaC", "", 2, "Radiology" },
                    { new Guid("f8fea9c7-6b03-4604-9d6c-c0f8f4a615a0"), "doctor3@aidims.com", "Khang To", "$2a$11$onOkyVFnPkJt6v.ktRYUg.qzxtrZwFDliyIjg3UsDqgGPHbFGshsq", "", 2, "Neurology" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_MedicalRecordId",
                table: "Diagnoses",
                column: "MedicalRecordId");

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
                name: "Diagnoses");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
