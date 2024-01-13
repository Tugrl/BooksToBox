using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksToBoxDemo.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookID);
                });

            migrationBuilder.CreateTable(
                name: "ProfileModels",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileModels", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "BookLike",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookModelBookID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookLike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookLike_Books_BookModelBookID",
                        column: x => x.BookModelBookID,
                        principalTable: "Books",
                        principalColumn: "BookID");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookModelBookID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Books_BookModelBookID",
                        column: x => x.BookModelBookID,
                        principalTable: "Books",
                        principalColumn: "BookID");
                });

            migrationBuilder.CreateTable(
                name: "ProfileBooks",
                columns: table => new
                {
                    ReadBookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileModelUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProfileModelUserId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileBooks", x => x.ReadBookId);
                    table.ForeignKey(
                        name: "FK_ProfileBooks_ProfileModels_ProfileModelUserId",
                        column: x => x.ProfileModelUserId,
                        principalTable: "ProfileModels",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_ProfileBooks_ProfileModels_ProfileModelUserId1",
                        column: x => x.ProfileModelUserId1,
                        principalTable: "ProfileModels",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileBookViewReadBookId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                    table.ForeignKey(
                        name: "FK_Categories_ProfileBooks_ProfileBookViewReadBookId",
                        column: x => x.ProfileBookViewReadBookId,
                        principalTable: "ProfileBooks",
                        principalColumn: "ReadBookId");
                });

            migrationBuilder.CreateTable(
                name: "BookModelCategoryModel",
                columns: table => new
                {
                    BooksBookID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriesCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookModelCategoryModel", x => new { x.BooksBookID, x.CategoriesCategoryID });
                    table.ForeignKey(
                        name: "FK_BookModelCategoryModel_Books_BooksBookID",
                        column: x => x.BooksBookID,
                        principalTable: "Books",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookModelCategoryModel_Categories_CategoriesCategoryID",
                        column: x => x.CategoriesCategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookLike_BookModelBookID",
                table: "BookLike",
                column: "BookModelBookID");

            migrationBuilder.CreateIndex(
                name: "IX_BookModelCategoryModel_CategoriesCategoryID",
                table: "BookModelCategoryModel",
                column: "CategoriesCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProfileBookViewReadBookId",
                table: "Categories",
                column: "ProfileBookViewReadBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BookModelBookID",
                table: "Comments",
                column: "BookModelBookID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileBooks_ProfileModelUserId",
                table: "ProfileBooks",
                column: "ProfileModelUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileBooks_ProfileModelUserId1",
                table: "ProfileBooks",
                column: "ProfileModelUserId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookLike");

            migrationBuilder.DropTable(
                name: "BookModelCategoryModel");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "ProfileBooks");

            migrationBuilder.DropTable(
                name: "ProfileModels");
        }
    }
}
