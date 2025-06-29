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
                keyValue: new Guid("821c6152-fdce-4293-886c-f9e6dac9b11e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("84522cc8-a321-420c-b277-8ae30ec1c51a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("932dd0ad-7f28-4b37-b57e-0e8bc696184c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("990a8d91-c438-40ae-bb8c-793370657ed9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c020ba1a-b3b3-4359-8314-3b20b89e71bd"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Full_name", "PasswordHash", "PhoneNumber", "PhysicianId", "Role", "Specialty" },
                values: new object[,]
                {
                    { new Guid("243a7acd-5649-4f98-9b26-917277c23876"), "admin@aidims.com", "Admin", "$2a$11$wkSh7ibgi3.LzP0XGGwwlOaAv5QX8a7nLpgPMWDZ6WePA8GE005vq", "", "BS243A7A", 1, null },
                    { new Guid("34d34480-dde3-491a-a617-41ca7230830a"), "staff@aidims.com", "Staff", "$2a$11$Xkreu1L10wTybC1Frms1/ejzoGJj8tge.fsXEtrqVxC1RpOGQWyPW", "0999765432", "BS34D344", 3, null },
                    { new Guid("3991f658-55fb-4160-90ac-0515a9b19e0d"), "doctor2@aidims.com", "Hoang Thien", "$2a$11$lSguklWi5ZclM6GNC7wi/.9zmAGtUB6l6sBUIb1/62q6hxaA8LKyW", "", "BS3991F6", 2, "Cardiology" },
                    { new Guid("5a08215a-794e-41d7-bc11-68fa5be5072e"), "doctor1@aidims.com", "Thanh Long", "$2a$11$FzAgXz6uW.oNwm2rS.gIjur2BpBupK6jbICvsH9alC0KQ79O7Ked.", "", "BS5A0821", 2, "Radiology" },
                    { new Guid("a3af2a2a-9a42-42d0-9894-072ef332f185"), "doctor3@aidims.com", "Khang To", "$2a$11$tx5pMn3W5OII9YNW0PqSOuvLb1KGzzsLHQmMDegESRwsG7KS5AUFW", "", "BSA3AF2A", 2, "Neurology" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("243a7acd-5649-4f98-9b26-917277c23876"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("34d34480-dde3-491a-a617-41ca7230830a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3991f658-55fb-4160-90ac-0515a9b19e0d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5a08215a-794e-41d7-bc11-68fa5be5072e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a3af2a2a-9a42-42d0-9894-072ef332f185"));

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
        }
    }
}
