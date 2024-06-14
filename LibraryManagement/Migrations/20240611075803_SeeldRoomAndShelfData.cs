using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class SeeldRoomAndShelfData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomId", "Capacity", "CreatedDate", "DeletedDate", "UpdatedDate", "RoomNumber" },
                values: new object[,]
                {
                    { new Guid("4c1d5049-b1cd-4130-999b-ec58b185f59a"), 20, DateTime.UtcNow, null, null, 101 },
                    { new Guid("f95a0286-27df-4dea-b2e1-1e9e6dac07fd"), 30, DateTime.UtcNow, null, null, 102 },
                    { new Guid("45f5855a-8bed-4b4f-b42a-979f25f6d3d5"), 25, DateTime.UtcNow, null, null, 201 }
                });

            migrationBuilder.InsertData(
                table: "Shelf",
                columns: new[] { "RoomId", "ShelfId", "CreatedDate", "DeletedDate", "MaxCapacity", "ShelfNumber", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("45f5855a-8bed-4b4f-b42a-979f25f6d3d5"), new Guid("d3595162-093a-4fc8-b046-ad38fc481872"), DateTime.UtcNow, null, 100, 1, null },
                    { new Guid("4c1d5049-b1cd-4130-999b-ec58b185f59a"), new Guid("8a491b2f-b39f-4957-abc6-58db09042395"), DateTime.UtcNow, null, 100, 1, null },
                    { new Guid("4c1d5049-b1cd-4130-999b-ec58b185f59a"), new Guid("a30ec77f-2a39-466d-a9ce-596d6e081075"), DateTime.UtcNow, null, 100, 2, null },
                    { new Guid("f95a0286-27df-4dea-b2e1-1e9e6dac07fd"), new Guid("94e8259a-fbd7-44c8-ae93-c0772df00f8f"), DateTime.UtcNow, null, 100, 1, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
