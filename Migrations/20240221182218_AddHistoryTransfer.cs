using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialApp.Migrations
{
    /// <inheritdoc />
    public partial class AddHistoryTransfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoryActionsWithUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAdministrator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeActionsWithUser = table.Column<int>(type: "int", nullable: false),
                    IdPerson = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryActionsWithUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryActionsWithUsers_Admins_IdAdministrator",
                        column: x => x.IdAdministrator,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoryActionsWithUsers_Persons_IdPerson",
                        column: x => x.IdPerson,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryTransfers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    MoneyTransfer = table.Column<double>(type: "float", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OperationType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryTransfers_Persons_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryTransfers_Persons_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryActionsWithUsers_IdAdministrator",
                table: "HistoryActionsWithUsers",
                column: "IdAdministrator");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryActionsWithUsers_IdPerson",
                table: "HistoryActionsWithUsers",
                column: "IdPerson");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryTransfers_RecipientId",
                table: "HistoryTransfers",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryTransfers_SenderId",
                table: "HistoryTransfers",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryActionsWithUsers");

            migrationBuilder.DropTable(
                name: "HistoryTransfers");
        }
    }
}
