using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gara.Management.Application.Migrations
{
    /// <inheritdoc />
    public partial class updateappointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminWorkDetail",
                table: "AppointmentSchedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StaffId",
                table: "AppointmentSchedules",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentSchedules_StaffId",
                table: "AppointmentSchedules",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentSchedules_AspNetUsers_StaffId",
                table: "AppointmentSchedules",
                column: "StaffId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentSchedules_AspNetUsers_StaffId",
                table: "AppointmentSchedules");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentSchedules_StaffId",
                table: "AppointmentSchedules");

            migrationBuilder.DropColumn(
                name: "AdminWorkDetail",
                table: "AppointmentSchedules");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "AppointmentSchedules");
        }
    }
}
