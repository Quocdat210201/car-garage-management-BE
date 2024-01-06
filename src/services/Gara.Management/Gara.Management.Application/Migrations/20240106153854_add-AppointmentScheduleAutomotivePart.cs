using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gara.Management.Application.Migrations
{
    /// <inheritdoc />
    public partial class addAppointmentScheduleAutomotivePart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppointmentScheduleAutomotiveParts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    AutomotivePartInWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppointmentScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    IsActived = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentScheduleAutomotiveParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentScheduleAutomotiveParts_AppointmentSchedules_AppointmentScheduleId",
                        column: x => x.AppointmentScheduleId,
                        principalTable: "AppointmentSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentScheduleAutomotiveParts_AutomotivePartInWarehouses_AutomotivePartInWarehouseId",
                        column: x => x.AutomotivePartInWarehouseId,
                        principalTable: "AutomotivePartInWarehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentScheduleAutomotiveParts_AppointmentScheduleId",
                table: "AppointmentScheduleAutomotiveParts",
                column: "AppointmentScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentScheduleAutomotiveParts_AutomotivePartInWarehouseId",
                table: "AppointmentScheduleAutomotiveParts",
                column: "AutomotivePartInWarehouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentScheduleAutomotiveParts");
        }
    }
}
