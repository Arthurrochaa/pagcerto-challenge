using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class Transaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    NSU = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DisapprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Anticipated = table.Column<bool>(type: "bit", nullable: false),
                    AcquirerConfirmation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrossAmount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    FixedRateCharged = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    InstallmentsNumber = table.Column<int>(type: "int", nullable: false),
                    LastCardDigits = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.NSU);
                });

            migrationBuilder.CreateTable(
                name: "TransactionInstallments",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParcelNumber = table.Column<long>(type: "bigint", nullable: false),
                    GrossAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AnticipatedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExpectedReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransferDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransactionNSU = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionInstallments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransactionInstallments_Transaction_TransactionNSU",
                        column: x => x.TransactionNSU,
                        principalTable: "Transaction",
                        principalColumn: "NSU",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionInstallments_TransactionNSU",
                table: "TransactionInstallments",
                column: "TransactionNSU");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionInstallments");

            migrationBuilder.DropTable(
                name: "Transaction");
        }
    }
}
