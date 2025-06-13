using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    DiagnosisId = table.Column<Guid>(type: "uuid", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AIAnalysis = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Diagnoses_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnoses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Full_name", "PasswordHash", "PhoneNumber", "Role", "Specialty" },
                values: new object[,]
                {
                    { new Guid("00da7ec6-b79d-4bae-940c-fafa51bf4b63"), "doctor1@aidims.com", "Thanh Long", "$2a$11$RU3gj7MSEUBBwEf.TKMkl.EO81WjS5Dljg0V2NtHs8xfRHX2AhOjG", "", 2, "Radiology" },
                    { new Guid("0576a36c-8747-41b0-9b63-f644385655f5"), "staff@aidims.com", "Staff", "$2a$11$QycrdLvHwIsBXyJKie65ou9dE.lZbdAODZEPx4zdXnq3YilO.nsdK", "", 3, null },
                    { new Guid("306c837b-8ad2-4dc7-b1f6-6fc5839f4858"), "admin@aidims.com", "Admin", "$2a$11$KcgtKjOzwmD313r.HVPQiuHtGE/hj7E0Hdue6d0ep08ahwjHPjd5y", "", 1, null },
                    { new Guid("6c808c50-2fb7-4a49-91e6-77fdb3dd60ff"), "doctor2@aidims.com", "Hoang Thien", "$2a$11$1JFyhMA96m7Po7uS7w1r0OeMAFiIkK1MsMvSPCzeKVt8urdcj7IrK", "", 2, "Cardiology" },
                    { new Guid("e5efb415-f727-484a-8bb8-9f4ba93fe5f7"), "doctor3@aidims.com", "Khang To", "$2a$11$OBKPA.nlqJ8sWn4JZPIXJeVBjx0u1n2yb2gYWboWiTLv1JLhYEG7W", "", 2, "Neurology" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_DiagnosisId",
                table: "Images",
                column: "DiagnosisId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00da7ec6-b79d-4bae-940c-fafa51bf4b63"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0576a36c-8747-41b0-9b63-f644385655f5"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("306c837b-8ad2-4dc7-b1f6-6fc5839f4858"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6c808c50-2fb7-4a49-91e6-77fdb3dd60ff"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e5efb415-f727-484a-8bb8-9f4ba93fe5f7"));

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
    }
}
