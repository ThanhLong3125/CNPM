using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePatientWithIdPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("469b1680-57f0-42e2-ba04-91f30486d178"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5029d4cf-fe56-4b59-8404-155efa3de1c3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7d6e7366-6698-452d-80ce-e01ac06a9240"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("94adafdc-e442-4326-8e36-41f2fb17f5b7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c6db6f62-40ca-43f3-a40a-eef91dedb953"));

            migrationBuilder.AddColumn<string>(
                name: "IdPatient",
                table: "Patients",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PatientID",
                table: "Patients",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Full_name", "PasswordHash", "PhoneNumber", "Role", "Specialty" },
                values: new object[,]
                {
                    { new Guid("00da2033-00aa-49b9-961b-0ef3f80f01bd"), "doctor1@aidims.com", "Thanh Long", "$2a$11$HxWcWUq5pGFKwlJKWD34AeGbERylRKT/bapb4M0InEAzGBKqRtys.", "", 2, "Radiology" },
                    { new Guid("10d99979-800f-454b-8c6d-ccabf21ac1bc"), "staff@aidims.com", "Staff", "$2a$11$g8Hq2yBGYSFb7.Nr.TM41unBadsy4RKxpo93299p8vmiK6mRoVwSK", "", 3, null },
                    { new Guid("288a867f-cab4-4dfd-95c7-6258886044a6"), "doctor2@aidims.com", "Hoang Thien", "$2a$11$bS80MQL7Emz12WBYmFuRqOHbY0gBVp0A6aRd/N813vSCIiN0jWTHS", "", 2, "Cardiology" },
                    { new Guid("a85860eb-657c-4333-9a4b-933e27ad5c17"), "admin@aidims.com", "Admin", "$2a$11$d9QooNMaPGIvn/FNXw3yqOxOQmYZBQndZdLKEsfM7NPLzMq8Q0aK2", "", 1, null },
                    { new Guid("c2364906-6f6f-4635-8eea-f423ec4693bc"), "doctor3@aidims.com", "Khang To", "$2a$11$UWxukqj7pAGbBN6/12e.CObYzBv/tE2iTK2F.NxLBIecQYCe5.Ndy", "", 2, "Neurology" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00da2033-00aa-49b9-961b-0ef3f80f01bd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10d99979-800f-454b-8c6d-ccabf21ac1bc"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("288a867f-cab4-4dfd-95c7-6258886044a6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a85860eb-657c-4333-9a4b-933e27ad5c17"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c2364906-6f6f-4635-8eea-f423ec4693bc"));

            migrationBuilder.DropColumn(
                name: "IdPatient",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PatientID",
                table: "Patients");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Full_name", "PasswordHash", "PhoneNumber", "Role", "Specialty" },
                values: new object[,]
                {
                    { new Guid("469b1680-57f0-42e2-ba04-91f30486d178"), "doctor1@aidims.com", "Thanh Long", "$2a$11$Cx.FL5C1/g9FxxGjpn20r.MAOfO1O0rdwgmkcQXHhzryOJ1ji2aeu", "", 2, "Radiology" },
                    { new Guid("5029d4cf-fe56-4b59-8404-155efa3de1c3"), "staff@aidims.com", "Staff", "$2a$11$i14UDRFSaG3GJoE3OrGAdu8dFgnkIu/ZL/FNrf8ere6ztfRjqEZeu", "", 3, null },
                    { new Guid("7d6e7366-6698-452d-80ce-e01ac06a9240"), "admin@aidims.com", "Admin", "$2a$11$eQvxOOY7D/OvPVJUUNOnEux4/hz91clNi2CdVB9NxhO1mmBKpOFYy", "", 1, null },
                    { new Guid("94adafdc-e442-4326-8e36-41f2fb17f5b7"), "doctor2@aidims.com", "Hoang Thien", "$2a$11$tzB7zAGnlYPm1D.bPkvueO4UGWLLlW25494csm6VByCRq9y6wxiE.", "", 2, "Cardiology" },
                    { new Guid("c6db6f62-40ca-43f3-a40a-eef91dedb953"), "doctor3@aidims.com", "Khang To", "$2a$11$HpftM4LcazY1s4e8VOrS3u386xwJ7bYv/ddI2ew25xUeQ/P9l4vJG", "", 2, "Neurology" }
                });
        }
    }
}
