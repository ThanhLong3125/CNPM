using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class TenMigrationMoi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5bd036c6-f4d4-4d2f-ae50-628ed9a6d5a6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("898fdb73-a13b-4376-989e-14601fb5420f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a24cd1f8-8d05-4adb-88ca-605ab141c4bc"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ce95fc6b-0275-4ba4-98a3-c3b7a0d35171"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ead73ef9-17dc-4249-a2fd-1d4a75700e2c"));

            migrationBuilder.AddColumn<string>(
                name: "Symptoms",
                table: "Patients",
                type: "text",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Symptoms",
                table: "Patients");

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
        }
    }
}
