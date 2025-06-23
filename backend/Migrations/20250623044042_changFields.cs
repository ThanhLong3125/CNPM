using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class changFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Symptoms",
                table: "Patients",
                newName: "MedicaHistory");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Full_name", "PasswordHash", "PhoneNumber", "Role", "Specialty" },
                values: new object[,]
                {
                    { new Guid("1cbfc3a4-7f78-4f90-8d0b-ffdc938d36e7"), "doctor3@aidims.com", "Khang To", "$2a$11$xUrcVp9KL5aXwnfSb8lrtergW5VterEI/Y1LzUxmirvcUhJufMMyy", "", 2, "Neurology" },
                    { new Guid("a4f465d9-7f43-4f20-b110-a5162eab9872"), "doctor2@aidims.com", "Hoang Thien", "$2a$11$aPVYnW9ttemjgyCXPaYXnuCaaM5.wmeg3K.2fTwgy0EcqxvWzpVmW", "", 2, "Cardiology" },
                    { new Guid("c7949f9e-111a-40ca-927c-56d38c43dd63"), "staff@aidims.com", "Staff", "$2a$11$SKsU.NmeZN9fpU/MKLKdq.UufGGUMXa2NWsfiOgL0JF9uyXgypcnm", "", 3, null },
                    { new Guid("d23030bd-f284-44a4-8471-ea17c914bb1c"), "doctor1@aidims.com", "Thanh Long", "$2a$11$BAMGPAiH9mAefMNfZ1WDQeu9kF1b6FLucEXJsnOt8zNKW4KjZTjfG", "", 2, "Radiology" },
                    { new Guid("d3605db7-877f-405f-90c7-399bc82644e0"), "admin@aidims.com", "Admin", "$2a$11$PfNY8KkSmsEqQ/gUKYN3iejzWPOvHrxBQc/6GJ7hXd2afrb37.vPK", "", 1, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1cbfc3a4-7f78-4f90-8d0b-ffdc938d36e7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a4f465d9-7f43-4f20-b110-a5162eab9872"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c7949f9e-111a-40ca-927c-56d38c43dd63"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d23030bd-f284-44a4-8471-ea17c914bb1c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d3605db7-877f-405f-90c7-399bc82644e0"));

            migrationBuilder.RenameColumn(
                name: "MedicaHistory",
                table: "Patients",
                newName: "Symptoms");

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
    }
}
