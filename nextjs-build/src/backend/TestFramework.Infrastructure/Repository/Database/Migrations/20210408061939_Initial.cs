using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace TestFramework.Infrastructure.Repository.Database.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HealthChecks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Healthy = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthChecks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Page",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UITestCases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    StartUrl = table.Column<string>(type: "text", nullable: false),
                    DefaultWaitForUIElement = table.Column<TimeSpan>(type: "time", nullable: false),
                    RunsPeriodically = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UITestCases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UITestRuns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Start = table.Column<DateTime>(type: "datetime", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UITestRuns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UITestRunUIElement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    FindByMethod = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    FindBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UITestRunUIElement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    StoredFileName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UIElements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    FindByMethod = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    FindBy = table.Column<string>(type: "text", nullable: false),
                    PageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UIElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UIElements_Page_PageId",
                        column: x => x.PageId,
                        principalTable: "Page",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UIEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Step = table.Column<int>(type: "int", nullable: false),
                    UITestCaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UIEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UIEvent_UITestCases_UITestCaseId",
                        column: x => x.UITestCaseId,
                        principalTable: "UITestCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UITestRunCases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    StartUrl = table.Column<string>(type: "text", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: true),
                    Browser = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    DefaultWaitForUIElement = table.Column<TimeSpan>(type: "time", nullable: false),
                    AutomaticallyStarted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FailureSendedState = table.Column<int>(type: "int", nullable: false),
                    UITestRunId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UITestRunCases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UITestRunCases_UITestRuns_UITestRunId",
                        column: x => x.UITestRunId,
                        principalTable: "UITestRuns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClearContentEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    WaitForUIElement = table.Column<TimeSpan>(type: "time", nullable: false),
                    UseDefaultWaitForUIElement = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UIElementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClearContentEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClearContentEvent_UIElements_UIElementId",
                        column: x => x.UIElementId,
                        principalTable: "UIElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClearContentEvent_UIEvent_Id",
                        column: x => x.Id,
                        principalTable: "UIEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClickAtPositionEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClickAtPositionEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClickAtPositionEvent_UIEvent_Id",
                        column: x => x.Id,
                        principalTable: "UIEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClickEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    WaitForUIElement = table.Column<TimeSpan>(type: "time", nullable: false),
                    UseDefaultWaitForUIElement = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UIElementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClickEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClickEvent_UIElements_UIElementId",
                        column: x => x.UIElementId,
                        principalTable: "UIElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClickEvent_UIEvent_Id",
                        column: x => x.Id,
                        principalTable: "UIEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportFileEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserFileId = table.Column<int>(type: "int", nullable: false),
                    WaitForUIElement = table.Column<TimeSpan>(type: "time", nullable: false),
                    UseDefaultWaitForUIElement = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UIElementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportFileEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportFileEvent_UIElements_UIElementId",
                        column: x => x.UIElementId,
                        principalTable: "UIElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImportFileEvent_UIEvent_Id",
                        column: x => x.Id,
                        principalTable: "UIEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportFileEvent_UserFiles_UserFileId",
                        column: x => x.UserFileId,
                        principalTable: "UserFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MoveByOffsetEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    OffsetX = table.Column<int>(type: "int", nullable: false),
                    OffsetY = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveByOffsetEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoveByOffsetEvent_UIEvent_Id",
                        column: x => x.Id,
                        principalTable: "UIEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoveToUIElementEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    WaitForUIElement = table.Column<TimeSpan>(type: "time", nullable: false),
                    UseDefaultWaitForUIElement = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UIElementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveToUIElementEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoveToUIElementEvent_UIElements_UIElementId",
                        column: x => x.UIElementId,
                        principalTable: "UIElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MoveToUIElementEvent_UIEvent_Id",
                        column: x => x.Id,
                        principalTable: "UIEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WaitEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Ticks = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaitEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WaitEvent_UIEvent_Id",
                        column: x => x.Id,
                        principalTable: "UIEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WriteEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Input = table.Column<string>(type: "VARCHAR(16000)", nullable: false, defaultValue: ""),
                    GenerateUnique = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    WaitForUIElement = table.Column<TimeSpan>(type: "time", nullable: false),
                    UseDefaultWaitForUIElement = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UIElementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WriteEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WriteEvent_UIElements_UIElementId",
                        column: x => x.UIElementId,
                        principalTable: "UIElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WriteEvent_UIEvent_Id",
                        column: x => x.Id,
                        principalTable: "UIEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UITestRunEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Start = table.Column<DateTime>(type: "datetime", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<int>(type: "int", nullable: false),
                    Step = table.Column<int>(type: "int", nullable: false),
                    UIRunCase = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UITestRunEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UITestRunEvent_UITestRunCases_UIRunCase",
                        column: x => x.UIRunCase,
                        principalTable: "UITestRunCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UITestRunClearContentEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    WaitForUIElement = table.Column<TimeSpan>(type: "time", nullable: false),
                    UseDefaultWaitForUIElement = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UITestRunUIElementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UITestRunClearContentEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UITestRunClearContentEvent_UITestRunEvent_Id",
                        column: x => x.Id,
                        principalTable: "UITestRunEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UITestRunClearContentEvent_UITestRunUIElement_UITestRunUIEle~",
                        column: x => x.UITestRunUIElementId,
                        principalTable: "UITestRunUIElement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UITestRunClickAtPositionEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UITestRunClickAtPositionEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UITestRunClickAtPositionEvent_UITestRunEvent_Id",
                        column: x => x.Id,
                        principalTable: "UITestRunEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UITestRunClickEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    WaitForUIElement = table.Column<TimeSpan>(type: "time", nullable: false),
                    UseDefaultWaitForUIElement = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UITestRunUIElementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UITestRunClickEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UITestRunClickEvent_UITestRunEvent_Id",
                        column: x => x.Id,
                        principalTable: "UITestRunEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UITestRunClickEvent_UITestRunUIElement_UITestRunUIElementId",
                        column: x => x.UITestRunUIElementId,
                        principalTable: "UITestRunUIElement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UITestRunImportFileEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    StoredFileName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    WaitForUIElement = table.Column<TimeSpan>(type: "time", nullable: false),
                    UseDefaultWaitForUIElement = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UITestRunUIElementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UITestRunImportFileEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UITestRunImportFileEvent_UITestRunEvent_Id",
                        column: x => x.Id,
                        principalTable: "UITestRunEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UITestRunImportFileEvent_UITestRunUIElement_UITestRunUIEleme~",
                        column: x => x.UITestRunUIElementId,
                        principalTable: "UITestRunUIElement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UITestRunMoveByOffsetEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    OffsetX = table.Column<int>(type: "int", nullable: false),
                    OffsetY = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UITestRunMoveByOffsetEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UITestRunMoveByOffsetEvent_UITestRunEvent_Id",
                        column: x => x.Id,
                        principalTable: "UITestRunEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UITestRunMoveToUIElementEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    WaitForUIElement = table.Column<TimeSpan>(type: "time", nullable: false),
                    UseDefaultWaitForUIElement = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UITestRunUIElementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UITestRunMoveToUIElementEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UITestRunMoveToUIElementEvent_UITestRunEvent_Id",
                        column: x => x.Id,
                        principalTable: "UITestRunEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UITestRunMoveToUIElementEvent_UITestRunUIElement_UITestRunUI~",
                        column: x => x.UITestRunUIElementId,
                        principalTable: "UITestRunUIElement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UITestRunWaitEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Ticks = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UITestRunWaitEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UITestRunWaitEvent_UITestRunEvent_Id",
                        column: x => x.Id,
                        principalTable: "UITestRunEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UITestRunWriteEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Input = table.Column<string>(type: "VARCHAR(16000)", nullable: false, defaultValue: ""),
                    GenerateUnique = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    WaitForUIElement = table.Column<TimeSpan>(type: "time", nullable: false),
                    UseDefaultWaitForUIElement = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UITestRunUIElementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UITestRunWriteEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UITestRunWriteEvent_UITestRunEvent_Id",
                        column: x => x.Id,
                        principalTable: "UITestRunEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UITestRunWriteEvent_UITestRunUIElement_UITestRunUIElementId",
                        column: x => x.UITestRunUIElementId,
                        principalTable: "UITestRunUIElement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClearContentEvent_UIElementId",
                table: "ClearContentEvent",
                column: "UIElementId");

            migrationBuilder.CreateIndex(
                name: "IX_ClickEvent_UIElementId",
                table: "ClickEvent",
                column: "UIElementId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportFileEvent_UIElementId",
                table: "ImportFileEvent",
                column: "UIElementId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportFileEvent_UserFileId",
                table: "ImportFileEvent",
                column: "UserFileId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveToUIElementEvent_UIElementId",
                table: "MoveToUIElementEvent",
                column: "UIElementId");

            migrationBuilder.CreateIndex(
                name: "IX_UIElements_PageId",
                table: "UIElements",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_UIEvent_UITestCaseId",
                table: "UIEvent",
                column: "UITestCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_UITestRunCases_UITestRunId",
                table: "UITestRunCases",
                column: "UITestRunId");

            migrationBuilder.CreateIndex(
                name: "IX_UITestRunClearContentEvent_UITestRunUIElementId",
                table: "UITestRunClearContentEvent",
                column: "UITestRunUIElementId");

            migrationBuilder.CreateIndex(
                name: "IX_UITestRunClickEvent_UITestRunUIElementId",
                table: "UITestRunClickEvent",
                column: "UITestRunUIElementId");

            migrationBuilder.CreateIndex(
                name: "IX_UITestRunEvent_UIRunCase",
                table: "UITestRunEvent",
                column: "UIRunCase");

            migrationBuilder.CreateIndex(
                name: "IX_UITestRunImportFileEvent_UITestRunUIElementId",
                table: "UITestRunImportFileEvent",
                column: "UITestRunUIElementId");

            migrationBuilder.CreateIndex(
                name: "IX_UITestRunMoveToUIElementEvent_UITestRunUIElementId",
                table: "UITestRunMoveToUIElementEvent",
                column: "UITestRunUIElementId");

            migrationBuilder.CreateIndex(
                name: "IX_UITestRunWriteEvent_UITestRunUIElementId",
                table: "UITestRunWriteEvent",
                column: "UITestRunUIElementId");

            migrationBuilder.CreateIndex(
                name: "IX_WriteEvent_UIElementId",
                table: "WriteEvent",
                column: "UIElementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClearContentEvent");

            migrationBuilder.DropTable(
                name: "ClickAtPositionEvent");

            migrationBuilder.DropTable(
                name: "ClickEvent");

            migrationBuilder.DropTable(
                name: "HealthChecks");

            migrationBuilder.DropTable(
                name: "ImportFileEvent");

            migrationBuilder.DropTable(
                name: "MoveByOffsetEvent");

            migrationBuilder.DropTable(
                name: "MoveToUIElementEvent");

            migrationBuilder.DropTable(
                name: "UITestRunClearContentEvent");

            migrationBuilder.DropTable(
                name: "UITestRunClickAtPositionEvent");

            migrationBuilder.DropTable(
                name: "UITestRunClickEvent");

            migrationBuilder.DropTable(
                name: "UITestRunImportFileEvent");

            migrationBuilder.DropTable(
                name: "UITestRunMoveByOffsetEvent");

            migrationBuilder.DropTable(
                name: "UITestRunMoveToUIElementEvent");

            migrationBuilder.DropTable(
                name: "UITestRunWaitEvent");

            migrationBuilder.DropTable(
                name: "UITestRunWriteEvent");

            migrationBuilder.DropTable(
                name: "WaitEvent");

            migrationBuilder.DropTable(
                name: "WriteEvent");

            migrationBuilder.DropTable(
                name: "UserFiles");

            migrationBuilder.DropTable(
                name: "UITestRunEvent");

            migrationBuilder.DropTable(
                name: "UITestRunUIElement");

            migrationBuilder.DropTable(
                name: "UIElements");

            migrationBuilder.DropTable(
                name: "UIEvent");

            migrationBuilder.DropTable(
                name: "UITestRunCases");

            migrationBuilder.DropTable(
                name: "Page");

            migrationBuilder.DropTable(
                name: "UITestCases");

            migrationBuilder.DropTable(
                name: "UITestRuns");
        }
    }
}
