using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gara.Management.Application.Migrations
{
    /// <inheritdoc />
    public partial class createautomotivepartwarehouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsDeliveryNoteDetails_AutomotiveParts_AutomotivePartId",
                table: "GoodsDeliveryNoteDetails");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "AutomotiveParts");

            migrationBuilder.RenameColumn(
                name: "AutomotivePartId",
                table: "GoodsDeliveryNoteDetails",
                newName: "AutomotivePartInWarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_GoodsDeliveryNoteDetails_AutomotivePartId",
                table: "GoodsDeliveryNoteDetails",
                newName: "IX_GoodsDeliveryNoteDetails_AutomotivePartInWarehouseId");

            migrationBuilder.AddColumn<string>(
                name: "GoodsDeliveryCode",
                table: "GoodsDeliveryNotes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceiveDate",
                table: "GoodsDeliveryNotes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "AutomotivePartInWarehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ReceivePrice = table.Column<double>(type: "float", nullable: false),
                    AutomotivePartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_AutomotivePartInWarehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AutomotivePartInWarehouses_AutomotiveParts_AutomotivePartId",
                        column: x => x.AutomotivePartId,
                        principalTable: "AutomotiveParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutomotivePartInWarehouses_AutomotivePartId",
                table: "AutomotivePartInWarehouses",
                column: "AutomotivePartId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsDeliveryNoteDetails_AutomotivePartInWarehouses_AutomotivePartInWarehouseId",
                table: "GoodsDeliveryNoteDetails",
                column: "AutomotivePartInWarehouseId",
                principalTable: "AutomotivePartInWarehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsDeliveryNoteDetails_AutomotivePartInWarehouses_AutomotivePartInWarehouseId",
                table: "GoodsDeliveryNoteDetails");

            migrationBuilder.DropTable(
                name: "AutomotivePartInWarehouses");

            migrationBuilder.DropColumn(
                name: "GoodsDeliveryCode",
                table: "GoodsDeliveryNotes");

            migrationBuilder.DropColumn(
                name: "ReceiveDate",
                table: "GoodsDeliveryNotes");

            migrationBuilder.RenameColumn(
                name: "AutomotivePartInWarehouseId",
                table: "GoodsDeliveryNoteDetails",
                newName: "AutomotivePartId");

            migrationBuilder.RenameIndex(
                name: "IX_GoodsDeliveryNoteDetails_AutomotivePartInWarehouseId",
                table: "GoodsDeliveryNoteDetails",
                newName: "IX_GoodsDeliveryNoteDetails_AutomotivePartId");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "AutomotiveParts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsDeliveryNoteDetails_AutomotiveParts_AutomotivePartId",
                table: "GoodsDeliveryNoteDetails",
                column: "AutomotivePartId",
                principalTable: "AutomotiveParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
