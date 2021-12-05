using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class AdvanceTransactionRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionInstallments_Transaction_TransactionNSU",
                table: "TransactionInstallments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionInstallments",
                table: "TransactionInstallments");

            migrationBuilder.RenameTable(
                name: "TransactionInstallments",
                newName: "TransactionInstallment");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionInstallments_TransactionNSU",
                table: "TransactionInstallment",
                newName: "IX_TransactionInstallment_TransactionNSU");

            migrationBuilder.AddColumn<long>(
                name: "AdvanceTransactionRequestID",
                table: "Transaction",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "NetAmount",
                table: "TransactionInstallment",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "GrossAmount",
                table: "TransactionInstallment",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AnticipatedAmount",
                table: "TransactionInstallment",
                type: "decimal(8,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionInstallment",
                table: "TransactionInstallment",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "AdvanceTransactionRequest",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnalysisStartedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnalysisEndedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnalysisStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestedAmount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    AnticipatedAmount = table.Column<decimal>(type: "decimal(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvanceTransactionRequest", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_AdvanceTransactionRequestID",
                table: "Transaction",
                column: "AdvanceTransactionRequestID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_AdvanceTransactionRequest_AdvanceTransactionRequestID",
                table: "Transaction",
                column: "AdvanceTransactionRequestID",
                principalTable: "AdvanceTransactionRequest",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionInstallment_Transaction_TransactionNSU",
                table: "TransactionInstallment",
                column: "TransactionNSU",
                principalTable: "Transaction",
                principalColumn: "NSU",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_AdvanceTransactionRequest_AdvanceTransactionRequestID",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionInstallment_Transaction_TransactionNSU",
                table: "TransactionInstallment");

            migrationBuilder.DropTable(
                name: "AdvanceTransactionRequest");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_AdvanceTransactionRequestID",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionInstallment",
                table: "TransactionInstallment");

            migrationBuilder.DropColumn(
                name: "AdvanceTransactionRequestID",
                table: "Transaction");

            migrationBuilder.RenameTable(
                name: "TransactionInstallment",
                newName: "TransactionInstallments");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionInstallment_TransactionNSU",
                table: "TransactionInstallments",
                newName: "IX_TransactionInstallments_TransactionNSU");

            migrationBuilder.AlterColumn<decimal>(
                name: "NetAmount",
                table: "TransactionInstallments",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "GrossAmount",
                table: "TransactionInstallments",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AnticipatedAmount",
                table: "TransactionInstallments",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionInstallments",
                table: "TransactionInstallments",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionInstallments_Transaction_TransactionNSU",
                table: "TransactionInstallments",
                column: "TransactionNSU",
                principalTable: "Transaction",
                principalColumn: "NSU",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
