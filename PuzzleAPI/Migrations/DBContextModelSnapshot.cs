﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PuzzleAPI.Models;

namespace PuzzleAPI.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PuzzleAPI.Models.Answers", b =>
                {
                    b.Property<int>("answerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("answer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isCorrect")
                        .HasColumnType("bit");

                    b.Property<int>("questionId")
                        .HasColumnType("int");

                    b.HasKey("answerId");

                    b.HasIndex("questionId");

                    b.ToTable("answers");
                });

            modelBuilder.Entity("PuzzleAPI.Models.QuestionCategory", b =>
                {
                    b.Property<int>("questionCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("categoryName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("questionCategoryId");

                    b.ToTable("questionCategories");
                });

            modelBuilder.Entity("PuzzleAPI.Models.Questions", b =>
                {
                    b.Property<int>("questionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("hint")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("point")
                        .HasColumnType("int");

                    b.Property<int>("questionCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("questionText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("questionId");

                    b.HasIndex("questionCategoryId");

                    b.ToTable("questions");
                });

            modelBuilder.Entity("PuzzleAPI.Models.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("appId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("points")
                        .HasColumnType("int");

                    b.HasKey("userId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("PuzzleAPI.Models.UserQuestions", b =>
                {
                    b.Property<int>("userQuestionsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("isCorrectOrPassed")
                        .HasColumnType("bit");

                    b.Property<int>("questionId")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("userQuestionsId");

                    b.ToTable("userQuestions");
                });

            modelBuilder.Entity("PuzzleAPI.Models.Answers", b =>
                {
                    b.HasOne("PuzzleAPI.Models.Questions", "questions")
                        .WithMany()
                        .HasForeignKey("questionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("questions");
                });

            modelBuilder.Entity("PuzzleAPI.Models.Questions", b =>
                {
                    b.HasOne("PuzzleAPI.Models.QuestionCategory", "questionCategory")
                        .WithMany()
                        .HasForeignKey("questionCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("questionCategory");
                });
#pragma warning restore 612, 618
        }
    }
}