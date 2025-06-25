using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todolistProject.dataAccess.Migrations
{
    /// <inheritdoc />
    public partial class removenoteStorage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_NoteStorage_notestorageid",
                table: "Note");

            migrationBuilder.DropTable(
                name: "NoteStorage");

            migrationBuilder.DropIndex(
                name: "IX_Note_notestorageid",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "notestorageid",
                table: "Note");

            migrationBuilder.AddColumn<string>(
                name: "ContentNote",
                table: "Note",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentNote",
                table: "Note");

            migrationBuilder.AddColumn<Guid>(
                name: "notestorageid",
                table: "Note",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "NoteStorage",
                columns: table => new
                {
                    idnotestorage = table.Column<Guid>(type: "uuid", nullable: false),
                    dirpathnote = table.Column<string>(type: "text", nullable: false),
                    filenamenote = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteStorage", x => x.idnotestorage);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Note_notestorageid",
                table: "Note",
                column: "notestorageid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Note_NoteStorage_notestorageid",
                table: "Note",
                column: "notestorageid",
                principalTable: "NoteStorage",
                principalColumn: "idnotestorage",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
