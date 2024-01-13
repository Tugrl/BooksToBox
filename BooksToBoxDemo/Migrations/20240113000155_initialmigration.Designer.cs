﻿// <auto-generated />
using System;
using BooksToBoxDemo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BooksToBoxDemo.Migrations
{
    [DbContext(typeof(BooksToBoxDbContext))]
    [Migration("20240113000155_initialmigration")]
    partial class initialmigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookModelCategoryModel", b =>
                {
                    b.Property<Guid>("BooksBookID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoriesCategoryID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BooksBookID", "CategoriesCategoryID");

                    b.HasIndex("CategoriesCategoryID");

                    b.ToTable("BookModelCategoryModel");
                });

            modelBuilder.Entity("BooksToBoxDemo.Models.BookLikeModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BookModelBookID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BookModelBookID");

                    b.ToTable("BookLike");
                });

            modelBuilder.Entity("BooksToBoxDemo.Models.BookModel", b =>
                {
                    b.Property<Guid>("BookID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BookImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BookName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookID");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BooksToBoxDemo.Models.CategoryModel", b =>
                {
                    b.Property<Guid>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProfileBookViewReadBookId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CategoryID");

                    b.HasIndex("ProfileBookViewReadBookId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BooksToBoxDemo.Models.CommentModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BookModelBookID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BookModelBookID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("BooksToBoxDemo.Models.ProfileModel", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId");

                    b.ToTable("ProfileModels");
                });

            modelBuilder.Entity("BooksToBoxDemo.Models.ViewModels.ProfileBookView", b =>
                {
                    b.Property<Guid>("ReadBookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("BookID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BookImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BookName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProfileModelUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProfileModelUserId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ReadBookId");

                    b.HasIndex("ProfileModelUserId");

                    b.HasIndex("ProfileModelUserId1");

                    b.ToTable("ProfileBooks");
                });

            modelBuilder.Entity("BookModelCategoryModel", b =>
                {
                    b.HasOne("BooksToBoxDemo.Models.BookModel", null)
                        .WithMany()
                        .HasForeignKey("BooksBookID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BooksToBoxDemo.Models.CategoryModel", null)
                        .WithMany()
                        .HasForeignKey("CategoriesCategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BooksToBoxDemo.Models.BookLikeModel", b =>
                {
                    b.HasOne("BooksToBoxDemo.Models.BookModel", null)
                        .WithMany("Likes")
                        .HasForeignKey("BookModelBookID");
                });

            modelBuilder.Entity("BooksToBoxDemo.Models.CategoryModel", b =>
                {
                    b.HasOne("BooksToBoxDemo.Models.ViewModels.ProfileBookView", null)
                        .WithMany("Categories")
                        .HasForeignKey("ProfileBookViewReadBookId");
                });

            modelBuilder.Entity("BooksToBoxDemo.Models.CommentModel", b =>
                {
                    b.HasOne("BooksToBoxDemo.Models.BookModel", null)
                        .WithMany("Comments")
                        .HasForeignKey("BookModelBookID");
                });

            modelBuilder.Entity("BooksToBoxDemo.Models.ViewModels.ProfileBookView", b =>
                {
                    b.HasOne("BooksToBoxDemo.Models.ProfileModel", null)
                        .WithMany("ReadBooks")
                        .HasForeignKey("ProfileModelUserId");

                    b.HasOne("BooksToBoxDemo.Models.ProfileModel", null)
                        .WithMany("WantReadBooks")
                        .HasForeignKey("ProfileModelUserId1");
                });

            modelBuilder.Entity("BooksToBoxDemo.Models.BookModel", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Likes");
                });

            modelBuilder.Entity("BooksToBoxDemo.Models.ProfileModel", b =>
                {
                    b.Navigation("ReadBooks");

                    b.Navigation("WantReadBooks");
                });

            modelBuilder.Entity("BooksToBoxDemo.Models.ViewModels.ProfileBookView", b =>
                {
                    b.Navigation("Categories");
                });
#pragma warning restore 612, 618
        }
    }
}
