using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintOnRoomNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Supprimer les doublons en gardant seulement la première chambre pour chaque numéro
            migrationBuilder.Sql(
                @"
                DELETE FROM ""Rooms"" 
                WHERE ""Id"" IN (
                    SELECT ""Id""
                    FROM (
                        SELECT ""Id"", 
                               ROW_NUMBER() OVER (PARTITION BY ""Number"" ORDER BY ""Id"") as rn
                        FROM ""Rooms""
                    ) t
                    WHERE t.rn > 1
                );
            "
            );

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_Number",
                table: "Rooms",
                column: "Number",
                unique: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(name: "IX_Rooms_Number", table: "Rooms");
        }
    }
}
