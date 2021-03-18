﻿// <auto-generated />
using System;
using AnotherBlog.Infra.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AnotherBlog.Infra.Data.Migrations.Blog
{
    [DbContext(typeof(BlogContext))]
    [Migration("20210318083722_initBlogDb")]
    partial class initBlogDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("AnotherBlog.Domain.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<long>("ArticleNo")
                        .HasColumnType("bigint");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("LastEditTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("PostDateTime")
                        .HasColumnType("datetime");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Article", "blog");
                });

            modelBuilder.Entity("AnotherBlog.Domain.Models.ArticleContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId")
                        .IsUnique();

                    b.ToTable("ArticleContent");
                });

            modelBuilder.Entity("AnotherBlog.Domain.Models.ArticleContent", b =>
                {
                    b.HasOne("AnotherBlog.Domain.Models.Article", "Article")
                        .WithOne("Content")
                        .HasForeignKey("AnotherBlog.Domain.Models.ArticleContent", "ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");
                });

            modelBuilder.Entity("AnotherBlog.Domain.Models.Article", b =>
                {
                    b.Navigation("Content");
                });
#pragma warning restore 612, 618
        }
    }
}
