using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Ten : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("87fea2c7-d511-4a72-8b9b-41306c5ac646"), "doctor2@aidims.com", "Hoang Thien", "$2a$11$9tcBlAbYAqShVoxDAecdSuPayqbtyMWhZ7wDOmU0UvQQL.Z1jOl.u", "", "BS87FEA2", 2, "Cardiology" },
                    { new Guid("91e486d5-bef6-473b-9b50-d7c4bff8a007"), "doctor1@aidims.com", "Thanh Long", "$2a$11$kRaODMHNad1cuzRwr.qk/.Wrrp/Z4Px8wd03EJteL6BY8T6KfrCTO", "0999765432", "BS91E486", 2, "Radiology" },
                    { new Guid("a042e89c-8504-4bd6-9f92-8ce7dfbc4b8b"), "admin@aidims.com", "Admin", "$2a$11$uw44Qy3poJCcpYZ0zoJwAO6aBQrjeI1jIIbvioGHi26qj6fHvT2va", "", "BSA042E8", 1, null },
                    { new Guid("d868d6a4-e32d-4b67-961e-900d28b32bdd"), "doctor3@aidims.com", "Khang To", "$2a$11$Ox/BC8EmF1xp4LMXr/kPXu6kd29iR4So99nyWPfjEpbBYlbWMpC1y", "", "BSD868D6", 2, "Neurology" },
                    { new Guid("fcd1d252-a408-4e65-b966-338f941324f1"), "staff@aidims.com", "Staff", "$2a$11$eir3eHZxSYyPCxaMneyeHuBCadBmVYBF8T.zo/rNnPpOm78NnQSs.", "0999765432", "BSFCD1D2", 3, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("87fea2c7-d511-4a72-8b9b-41306c5ac646"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("91e486d5-bef6-473b-9b50-d7c4bff8a007"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a042e89c-8504-4bd6-9f92-8ce7dfbc4b8b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d868d6a4-e32d-4b67-961e-900d28b32bdd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fcd1d252-a408-4e65-b966-338f941324f1"));

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
    }
}
