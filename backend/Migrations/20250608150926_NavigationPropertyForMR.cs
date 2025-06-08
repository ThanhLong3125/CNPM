using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class NavigationPropertyForMR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2bb3186e-c9be-4289-a0ea-925b80625033"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3ba675d4-3d54-4bd9-bfb7-6d5ee8c2971b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("87249296-fa86-4ab2-807a-60ea32ecb2c8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f096938c-2989-452d-9b47-6d59aa11e817"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f8fea9c7-6b03-4604-9d6c-c0f8f4a615a0"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Full_name", "PasswordHash", "PhoneNumber", "Role", "Specialty" },
                values: new object[,]
                {
                    { new Guid("02af350d-9dcf-4dfa-9100-d4e30773e255"), "staff@aidims.com", "Staff", "$2a$11$qvTsigogqqi4.xlQC6VQmuaameIMcmPJcb3jDP.38RnzrXvHwEvsa", "", 3, null },
                    { new Guid("0ae122e5-fa81-460b-9ac9-b863c95ffbc2"), "doctor3@aidims.com", "Khang To", "$2a$11$0J.c98Omw01SEkK/gcL.BuAT9EmUltHJCY5rF4kFfcf61wtXYe2X6", "", 2, "Neurology" },
                    { new Guid("4a016dc0-394a-4991-b2b1-28adf362c24c"), "doctor1@aidims.com", "Thanh Long", "$2a$11$2aT8OtV28i8XEbGKcGpm/ut2Z2D4hmm1PkuJkylC1khYz.ZcpIq9m", "", 2, "Radiology" },
                    { new Guid("f16f94f0-b94d-49ff-a2ec-906e66ac7155"), "admin@aidims.com", "Admin", "$2a$11$ufpej0042GCAWFcuD9Us7.ZpK/08TZgjruuT.x.lGW9NFwhsA4rmm", "", 1, null },
                    { new Guid("fed19b86-db91-4a69-b18b-0d4fe2967273"), "doctor2@aidims.com", "Hoang Thien", "$2a$11$IwrTLyNoQV9jfWOxeaxeHeZocVtxSxFRwzVqz9hPn.08H7dVlIXdS", "", 2, "Cardiology" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("02af350d-9dcf-4dfa-9100-d4e30773e255"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0ae122e5-fa81-460b-9ac9-b863c95ffbc2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4a016dc0-394a-4991-b2b1-28adf362c24c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f16f94f0-b94d-49ff-a2ec-906e66ac7155"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fed19b86-db91-4a69-b18b-0d4fe2967273"));

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
        }
    }
}
