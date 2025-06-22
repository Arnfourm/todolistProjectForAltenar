using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todolistProject.dataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NoteStorage",
                columns: table => new
                {
                    idnotestorage = table.Column<Guid>(type: "uuid", nullable: false),
                    filenamenote = table.Column<string>(type: "text", nullable: false),
                    dirpathnote = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteStorage", x => x.idnotestorage);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    iduser = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    useremail = table.Column<string>(type: "text", nullable: false),
                    userpassword = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.iduser);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    idgroup = table.Column<Guid>(type: "uuid", nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    titlegroup = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.idgroup);
                    table.ForeignKey(
                        name: "FK_Group_User_userid",
                        column: x => x.userid,
                        principalTable: "User",
                        principalColumn: "iduser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    idnote = table.Column<Guid>(type: "uuid", nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    titlenote = table.Column<string>(type: "text", nullable: false),
                    notestorageid = table.Column<Guid>(type: "uuid", nullable: false),
                    groupid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.idnote);
                    table.ForeignKey(
                        name: "FK_Note_Group_groupid",
                        column: x => x.groupid,
                        principalTable: "Group",
                        principalColumn: "idgroup",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Note_NoteStorage_notestorageid",
                        column: x => x.notestorageid,
                        principalTable: "NoteStorage",
                        principalColumn: "idnotestorage",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Note_User_userid",
                        column: x => x.userid,
                        principalTable: "User",
                        principalColumn: "iduser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Group_userid",
                table: "Group",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_Note_groupid",
                table: "Note",
                column: "groupid");

            migrationBuilder.CreateIndex(
                name: "IX_Note_notestorageid",
                table: "Note",
                column: "notestorageid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Note_userid",
                table: "Note",
                column: "userid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "NoteStorage");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
