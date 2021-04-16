﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestFramework.Infrastructure.Repository.Database;

namespace TestFramework.Infrastructure.Repository.Database.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20210408061939_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("TestFramework.Domain.HealthCheck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Healthy")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("HealthChecks");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Run.Events.UITestRunEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<TimeSpan?>("Duration")
                        .HasColumnType("time");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Result")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Start")
                        .HasColumnType("datetime");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int>("Step")
                        .HasColumnType("int");

                    b.Property<int>("UIRunCase")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UIRunCase");

                    b.ToTable("UITestRunEvent");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Run.UITestRun", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UITestRuns");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Run.UITestRunCase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("AutomaticallyStarted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Browser")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("DefaultWaitForUIElement")
                        .HasColumnType("time");

                    b.Property<TimeSpan?>("Duration")
                        .HasColumnType("time");

                    b.Property<int>("FailureSendedState")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("Start")
                        .HasColumnType("datetime");

                    b.Property<string>("StartUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int>("UITestRunId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UITestRunId");

                    b.ToTable("UITestRunCases");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Run.UITestRunUIElement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FindBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("FindByMethod")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("UITestRunUIElement");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Template.Events.UIEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Step")
                        .HasColumnType("int");

                    b.Property<int>("UITestCaseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UITestCaseId");

                    b.ToTable("UIEvent");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Template.Page", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Page");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Template.UIElement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FindBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("FindByMethod")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("PageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PageId");

                    b.ToTable("UIElements");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Template.UITestCase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<TimeSpan>("DefaultWaitForUIElement")
                        .HasColumnType("time");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("RunsPeriodically")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("StartUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UITestCases");
                });

            modelBuilder.Entity("TestFramework.Domain.UserFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<string>("StoredFileName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("UserFiles");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Run.Events.UITestRunClearContentEvent", b =>
                {
                    b.HasBaseType("TestFramework.Domain.UITesting.Run.Events.UITestRunEvent");

                    b.Property<int>("UITestRunUIElementId")
                        .HasColumnType("int");

                    b.Property<bool>("UseDefaultWaitForUIElement")
                        .HasColumnType("tinyint(1)");

                    b.Property<TimeSpan>("WaitForUIElement")
                        .HasColumnType("time");

                    b.HasIndex("UITestRunUIElementId");

                    b.ToTable("UITestRunClearContentEvent");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Run.Events.UITestRunClickAtPositionEvent", b =>
                {
                    b.HasBaseType("TestFramework.Domain.UITesting.Run.Events.UITestRunEvent");

                    b.ToTable("UITestRunClickAtPositionEvent");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Run.Events.UITestRunClickEvent", b =>
                {
                    b.HasBaseType("TestFramework.Domain.UITesting.Run.Events.UITestRunEvent");

                    b.Property<int>("UITestRunUIElementId")
                        .HasColumnType("int");

                    b.Property<bool>("UseDefaultWaitForUIElement")
                        .HasColumnType("tinyint(1)");

                    b.Property<TimeSpan>("WaitForUIElement")
                        .HasColumnType("time");

                    b.HasIndex("UITestRunUIElementId");

                    b.ToTable("UITestRunClickEvent");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Run.Events.UITestRunImportFileEvent", b =>
                {
                    b.HasBaseType("TestFramework.Domain.UITesting.Run.Events.UITestRunEvent");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("StoredFileName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("UITestRunUIElementId")
                        .HasColumnType("int");

                    b.Property<bool>("UseDefaultWaitForUIElement")
                        .HasColumnType("tinyint(1)");

                    b.Property<TimeSpan>("WaitForUIElement")
                        .HasColumnType("time");

                    b.HasIndex("UITestRunUIElementId");

                    b.ToTable("UITestRunImportFileEvent");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Run.Events.UITestRunMoveByOffsetEvent", b =>
                {
                    b.HasBaseType("TestFramework.Domain.UITesting.Run.Events.UITestRunEvent");

                    b.Property<int>("OffsetX")
                        .HasColumnType("int");

                    b.Property<int>("OffsetY")
                        .HasColumnType("int");

                    b.ToTable("UITestRunMoveByOffsetEvent");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Run.Events.UITestRunMoveToUIElementEvent", b =>
                {
                    b.HasBaseType("TestFramework.Domain.UITesting.Run.Events.UITestRunEvent");

                    b.Property<int>("UITestRunUIElementId")
                        .HasColumnType("int");

                    b.Property<bool>("UseDefaultWaitForUIElement")
                        .HasColumnType("tinyint(1)");

                    b.Property<TimeSpan>("WaitForUIElement")
                        .HasColumnType("time");

                    b.HasIndex("UITestRunUIElementId");

                    b.ToTable("UITestRunMoveToUIElementEvent");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Run.Events.UITestRunWaitEvent", b =>
                {
                    b.HasBaseType("TestFramework.Domain.UITesting.Run.Events.UITestRunEvent");

                    b.Property<TimeSpan>("Ticks")
                        .HasColumnType("time");

                    b.ToTable("UITestRunWaitEvent");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Run.Events.UITestRunWriteEvent", b =>
                {
                    b.HasBaseType("TestFramework.Domain.UITesting.Run.Events.UITestRunEvent");

                    b.Property<bool>("GenerateUnique")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Input")
                        .IsRequired()
                        .HasColumnType("VARCHAR(16000)")
                        .HasDefaultValue("");

                    b.Property<int>("UITestRunUIElementId")
                        .HasColumnType("int");

                    b.Property<bool>("UseDefaultWaitForUIElement")
                        .HasColumnType("tinyint(1)");

                    b.Property<TimeSpan>("WaitForUIElement")
                        .HasColumnType("time");

                    b.HasIndex("UITestRunUIElementId");

                    b.ToTable("UITestRunWriteEvent");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Template.Events.ClearContentEvent", b =>
                {
                    b.HasBaseType("TestFramework.Domain.UITesting.Template.Events.UIEvent");

                    b.Property<int>("UIElementId")
                        .HasColumnType("int");

                    b.Property<bool>("UseDefaultWaitForUIElement")
                        .HasColumnType("tinyint(1)");

                    b.Property<TimeSpan>("WaitForUIElement")
                        .HasColumnType("time");

                    b.HasIndex("UIElementId");

                    b.ToTable("ClearContentEvent");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Template.Events.ClickAtPositionEvent", b =>
                {
                    b.HasBaseType("TestFramework.Domain.UITesting.Template.Events.UIEvent");

                    b.ToTable("ClickAtPositionEvent");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Template.Events.ClickEvent", b =>
                {
                    b.HasBaseType("TestFramework.Domain.UITesting.Template.Events.UIEvent");

                    b.Property<int>("UIElementId")
                        .HasColumnType("int");

                    b.Property<bool>("UseDefaultWaitForUIElement")
                        .HasColumnType("tinyint(1)");

                    b.Property<TimeSpan>("WaitForUIElement")
                        .HasColumnType("time");

                    b.HasIndex("UIElementId");

                    b.ToTable("ClickEvent");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Template.Events.ImportFileEvent", b =>
                {
                    b.HasBaseType("TestFramework.Domain.UITesting.Template.Events.UIEvent");

                    b.Property<int>("UIElementId")
                        .HasColumnType("int");

                    b.Property<bool>("UseDefaultWaitForUIElement")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("UserFileId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("WaitForUIElement")
                        .HasColumnType("time");

                    b.HasIndex("UIElementId");

                    b.HasIndex("UserFileId");

                    b.ToTable("ImportFileEvent");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Template.Events.MoveByOffsetEvent", b =>
                {
                    b.HasBaseType("TestFramework.Domain.UITesting.Template.Events.UIEvent");

                    b.Property<int>("OffsetX")
                        .HasColumnType("int");

                    b.Property<int>("OffsetY")
                        .HasColumnType("int");

                    b.ToTable("MoveByOffsetEvent");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Template.Events.MoveToUIElementEvent", b =>
                {
                    b.HasBaseType("TestFramework.Domain.UITesting.Template.Events.UIEvent");

                    b.Property<int>("UIElementId")
                        .HasColumnType("int");

                    b.Property<bool>("UseDefaultWaitForUIElement")
                        .HasColumnType("tinyint(1)");

                    b.Property<TimeSpan>("WaitForUIElement")
                        .HasColumnType("time");

                    b.HasIndex("UIElementId");

                    b.ToTable("MoveToUIElementEvent");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Template.Events.WaitEvent", b =>
                {
                    b.HasBaseType("TestFramework.Domain.UITesting.Template.Events.UIEvent");

                    b.Property<TimeSpan>("Ticks")
                        .HasColumnType("time");

                    b.ToTable("WaitEvent");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Template.Events.WriteEvent", b =>
                {
                    b.HasBaseType("TestFramework.Domain.UITesting.Template.Events.UIEvent");

                    b.Property<bool>("GenerateUnique")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Input")
                        .IsRequired()
                        .HasColumnType("VARCHAR(16000)")
                        .HasDefaultValue("");

                    b.Property<int>("UIElementId")
                        .HasColumnType("int");

                    b.Property<bool>("UseDefaultWaitForUIElement")
                        .HasColumnType("tinyint(1)");

                    b.Property<TimeSpan>("WaitForUIElement")
                        .HasColumnType("time");

                    b.HasIndex("UIElementId");

                    b.ToTable("WriteEvent");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Run.Events.UITestRunEvent", b =>
                {
                    b.HasOne("TestFramework.Domain.UITesting.Run.UITestRunCase", "RunCase")
                        .WithMany("Events")
                        .HasForeignKey("UIRunCase")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RunCase");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Run.UITestRunCase", b =>
                {
                    b.HasOne("TestFramework.Domain.UITesting.Run.UITestRun", "TestRun")
                        .WithMany("TestCases")
                        .HasForeignKey("UITestRunId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TestRun");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Template.Events.UIEvent", b =>
                {
                    b.HasOne("TestFramework.Domain.UITesting.Template.UITestCase", "UITestCase")
                        .WithMany("Events")
                        .HasForeignKey("UITestCaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UITestCase");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Template.UIElement", b =>
                {
                    b.HasOne("TestFramework.Domain.UITesting.Template.Page", "Page")
                        .WithMany("UIElements")
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Page");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Run.Events.UITestRunClearContentEvent", b =>
                {
                    b.HasOne("TestFramework.Domain.UITesting.Run.Events.UITestRunEvent", null)
                        .WithOne()
                        .HasForeignKey("TestFramework.Domain.UITesting.Run.Events.UITestRunClearContentEvent", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestFramework.Domain.UITesting.Run.UITestRunUIElement", "UIElement")
                        .WithMany("ClearContentEvents")
                        .HasForeignKey("UITestRunUIElementId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("UIElement");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Run.Events.UITestRunClickAtPositionEvent", b =>
                {
                    b.HasOne("TestFramework.Domain.UITesting.Run.Events.UITestRunEvent", null)
                        .WithOne()
                        .HasForeignKey("TestFramework.Domain.UITesting.Run.Events.UITestRunClickAtPositionEvent", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Run.Events.UITestRunClickEvent", b =>
                {
                    b.HasOne("TestFramework.Domain.UITesting.Run.Events.UITestRunEvent", null)
                        .WithOne()
                        .HasForeignKey("TestFramework.Domain.UITesting.Run.Events.UITestRunClickEvent", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestFramework.Domain.UITesting.Run.UITestRunUIElement", "UIElement")
                        .WithMany("ClickEvents")
                        .HasForeignKey("UITestRunUIElementId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("UIElement");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Run.Events.UITestRunImportFileEvent", b =>
                {
                    b.HasOne("TestFramework.Domain.UITesting.Run.Events.UITestRunEvent", null)
                        .WithOne()
                        .HasForeignKey("TestFramework.Domain.UITesting.Run.Events.UITestRunImportFileEvent", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestFramework.Domain.UITesting.Run.UITestRunUIElement", "UIElement")
                        .WithMany("ImportFileEvents")
                        .HasForeignKey("UITestRunUIElementId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("UIElement");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Run.Events.UITestRunMoveByOffsetEvent", b =>
                {
                    b.HasOne("TestFramework.Domain.UITesting.Run.Events.UITestRunEvent", null)
                        .WithOne()
                        .HasForeignKey("TestFramework.Domain.UITesting.Run.Events.UITestRunMoveByOffsetEvent", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Run.Events.UITestRunMoveToUIElementEvent", b =>
                {
                    b.HasOne("TestFramework.Domain.UITesting.Run.Events.UITestRunEvent", null)
                        .WithOne()
                        .HasForeignKey("TestFramework.Domain.UITesting.Run.Events.UITestRunMoveToUIElementEvent", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestFramework.Domain.UITesting.Run.UITestRunUIElement", "UIElement")
                        .WithMany("MoveToUIElements")
                        .HasForeignKey("UITestRunUIElementId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("UIElement");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Run.Events.UITestRunWaitEvent", b =>
                {
                    b.HasOne("TestFramework.Domain.UITesting.Run.Events.UITestRunEvent", null)
                        .WithOne()
                        .HasForeignKey("TestFramework.Domain.UITesting.Run.Events.UITestRunWaitEvent", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Run.Events.UITestRunWriteEvent", b =>
                {
                    b.HasOne("TestFramework.Domain.UITesting.Run.Events.UITestRunEvent", null)
                        .WithOne()
                        .HasForeignKey("TestFramework.Domain.UITesting.Run.Events.UITestRunWriteEvent", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestFramework.Domain.UITesting.Run.UITestRunUIElement", "UIElement")
                        .WithMany("WriteEvents")
                        .HasForeignKey("UITestRunUIElementId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("UIElement");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Template.Events.ClearContentEvent", b =>
                {
                    b.HasOne("TestFramework.Domain.UITesting.Template.Events.UIEvent", null)
                        .WithOne()
                        .HasForeignKey("TestFramework.Domain.UITesting.Template.Events.ClearContentEvent", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestFramework.Domain.UITesting.Template.UIElement", "UIElement")
                        .WithMany("ClearContentEvents")
                        .HasForeignKey("UIElementId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("UIElement");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Template.Events.ClickAtPositionEvent", b =>
                {
                    b.HasOne("TestFramework.Domain.UITesting.Template.Events.UIEvent", null)
                        .WithOne()
                        .HasForeignKey("TestFramework.Domain.UITesting.Template.Events.ClickAtPositionEvent", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Template.Events.ClickEvent", b =>
                {
                    b.HasOne("TestFramework.Domain.UITesting.Template.Events.UIEvent", null)
                        .WithOne()
                        .HasForeignKey("TestFramework.Domain.UITesting.Template.Events.ClickEvent", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestFramework.Domain.UITesting.Template.UIElement", "UIElement")
                        .WithMany("ClickEvents")
                        .HasForeignKey("UIElementId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("UIElement");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Template.Events.ImportFileEvent", b =>
                {
                    b.HasOne("TestFramework.Domain.UITesting.Template.Events.UIEvent", null)
                        .WithOne()
                        .HasForeignKey("TestFramework.Domain.UITesting.Template.Events.ImportFileEvent", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestFramework.Domain.UITesting.Template.UIElement", "UIElement")
                        .WithMany("ImportFileEvents")
                        .HasForeignKey("UIElementId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TestFramework.Domain.UserFile", "UserFile")
                        .WithMany("ImportFileEvents")
                        .HasForeignKey("UserFileId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("UIElement");

                    b.Navigation("UserFile");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Template.Events.MoveByOffsetEvent", b =>
                {
                    b.HasOne("TestFramework.Domain.UITesting.Template.Events.UIEvent", null)
                        .WithOne()
                        .HasForeignKey("TestFramework.Domain.UITesting.Template.Events.MoveByOffsetEvent", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Template.Events.MoveToUIElementEvent", b =>
                {
                    b.HasOne("TestFramework.Domain.UITesting.Template.Events.UIEvent", null)
                        .WithOne()
                        .HasForeignKey("TestFramework.Domain.UITesting.Template.Events.MoveToUIElementEvent", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestFramework.Domain.UITesting.Template.UIElement", "UIElement")
                        .WithMany("MoveToUIElementEvents")
                        .HasForeignKey("UIElementId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("UIElement");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Template.Events.WaitEvent", b =>
                {
                    b.HasOne("TestFramework.Domain.UITesting.Template.Events.UIEvent", null)
                        .WithOne()
                        .HasForeignKey("TestFramework.Domain.UITesting.Template.Events.WaitEvent", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Template.Events.WriteEvent", b =>
                {
                    b.HasOne("TestFramework.Domain.UITesting.Template.Events.UIEvent", null)
                        .WithOne()
                        .HasForeignKey("TestFramework.Domain.UITesting.Template.Events.WriteEvent", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestFramework.Domain.UITesting.Template.UIElement", "UIElement")
                        .WithMany("WriteEvents")
                        .HasForeignKey("UIElementId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("UIElement");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Run.UITestRun", b =>
                {
                    b.Navigation("TestCases");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Run.UITestRunCase", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Run.UITestRunUIElement", b =>
                {
                    b.Navigation("ClearContentEvents");

                    b.Navigation("ClickEvents");

                    b.Navigation("ImportFileEvents");

                    b.Navigation("MoveToUIElements");

                    b.Navigation("WriteEvents");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Template.Page", b =>
                {
                    b.Navigation("UIElements");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Template.UIElement", b =>
                {
                    b.Navigation("ClearContentEvents");

                    b.Navigation("ClickEvents");

                    b.Navigation("ImportFileEvents");

                    b.Navigation("MoveToUIElementEvents");

                    b.Navigation("WriteEvents");
                });

            modelBuilder.Entity("TestFramework.Domain.UITesting.Template.UITestCase", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("TestFramework.Domain.UserFile", b =>
                {
                    b.Navigation("ImportFileEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
