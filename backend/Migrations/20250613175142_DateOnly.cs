using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class DateOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("122ca3ef-4e58-46b7-bc12-22fdd33c7f62"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6321a22a-e35f-485e-863e-7cac7b6d42cb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8b5e7437-70af-4a3e-b851-f2f46bb178ed"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("98090d27-9579-4fc8-9734-f255d8d70a6a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("be3c6f96-5681-4237-b6a2-350b0b7dfe9b"));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Patients",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Full_name", "IsDeleted", "PasswordHash", "PhoneNumber", "Role", "Specialty" },
                values: new object[,]
                {
                    { new Guid("0bdd8731-0fbb-4c22-ac1a-32cd730f8c66"), "admin@aidims.com", "Admin", false, "$2a$11$wOzUTr.NfwPZZ.qP21alsecjF9UjxggHcic04RL8fzFzsnJujRAJW", "", 1, null },
                    { new Guid("92d981a6-2428-43f1-b795-df9491f91802"), "doctor3@aidims.com", "Khang To", false, "$2a$11$1/tcweZRT1lsnohi.Yh0huqq.8x44QllsTQeh8qj.IsOMDSWWJ90u", "", 2, "Neurology" },
                    { new Guid("95943549-d45e-4cb1-851c-aced94dea0e7"), "doctor1@aidims.com", "Thanh Long", false, "$2a$11$QUV1M4IlxmVqCn6IYtgS1OX161sNZQtFvPoys/uAhpTd8AatzPiA6", "", 2, "Radiology" },
                    { new Guid("c129ca4a-3d82-4896-8c07-789076d835d1"), "staff@aidims.com", "Staff", false, "$2a$11$4M5ScDgjlH/amS.cAXjjO.5SjGOclfR4VIs7YZ.PDsWa5.Q64nGWi", "", 3, null },
                    { new Guid("d72f5bbc-0784-4d33-b884-6be322b7ecf8"), "doctor2@aidims.com", "Hoang Thien", false, "$2a$11$kXbXi3BiqmdZvuElQ28L9OQI9kWsIkg7PU.MhC6nYWm3JFVc1Z3F2", "", 2, "Cardiology" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0bdd8731-0fbb-4c22-ac1a-32cd730f8c66"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("92d981a6-2428-43f1-b795-df9491f91802"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("95943549-d45e-4cb1-851c-aced94dea0e7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c129ca4a-3d82-4896-8c07-789076d835d1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d72f5bbc-0784-4d33-b884-6be322b7ecf8"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Patients",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Full_name", "IsDeleted", "PasswordHash", "PhoneNumber", "Role", "Specialty" },
                values: new object[,]
                {
                    { new Guid("122ca3ef-4e58-46b7-bc12-22fdd33c7f62"), "doctor2@aidims.com", "Hoang Thien", false, "$2a$11$RYwmGx5BiLJqFOg5vugGdOzE1UcgHYCYq5aglit3z0m2WghYDfyha", "", 2, "Cardiology" },
                    { new Guid("6321a22a-e35f-485e-863e-7cac7b6d42cb"), "admin@aidims.com", "Admin", false, "$2a$11$Vp3FgtGNy0UDmhQ/WfBcDuZgaPn88abEQUocXNAwAsg7MaynTaMai", "", 1, null },
                    { new Guid("8b5e7437-70af-4a3e-b851-f2f46bb178ed"), "doctor3@aidims.com", "Khang To", false, "$2a$11$GNHgW15KLCHEasKWkI.yxeodhJt3WLUL/V/ZPWVEtA3fpHXrqWJae", "", 2, "Neurology" },
                    { new Guid("98090d27-9579-4fc8-9734-f255d8d70a6a"), "doctor1@aidims.com", "Thanh Long", false, "$2a$11$K0Tf99dVIsHpiBjuvpR2lOQJlOG.bTAdmlvItY45PCYX2VSHjlnti", "", 2, "Radiology" },
                    { new Guid("be3c6f96-5681-4237-b6a2-350b0b7dfe9b"), "staff@aidims.com", "Staff", false, "$2a$11$3pKu8mcvHc2pHJi1y77GyegBCpESZnB.0imiFyGsz2eLFVOt46yiq", "", 3, null }
                });
        }
    }
}
