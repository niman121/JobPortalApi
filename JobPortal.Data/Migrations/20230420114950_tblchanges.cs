using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobPortal.Data.Migrations
{
    public partial class tblchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateJob_Candidates_CandidatesId",
                table: "CandidateJob");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateJob_Jobs_JobsId",
                table: "CandidateJob");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Jobs",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "JobsId",
                table: "CandidateJob",
                newName: "JobId");

            migrationBuilder.RenameColumn(
                name: "CandidatesId",
                table: "CandidateJob",
                newName: "CandidateId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateJob_JobsId",
                table: "CandidateJob",
                newName: "IX_CandidateJob_JobId");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Jobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Jobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "NumberOfOpenings",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "Jobs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "AppliedDate",
                table: "CandidateJob",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "JobPortalOtps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Otp = table.Column<int>(type: "int", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPortalOtps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobType_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobType_JobId",
                table: "JobType",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateJob_Candidates_CandidateId",
                table: "CandidateJob",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateJob_Jobs_JobId",
                table: "CandidateJob",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateJob_Candidates_CandidateId",
                table: "CandidateJob");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateJob_Jobs_JobId",
                table: "CandidateJob");

            migrationBuilder.DropTable(
                name: "JobPortalOtps");

            migrationBuilder.DropTable(
                name: "JobType");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "NumberOfOpenings",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "AppliedDate",
                table: "CandidateJob");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Jobs",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "JobId",
                table: "CandidateJob",
                newName: "JobsId");

            migrationBuilder.RenameColumn(
                name: "CandidateId",
                table: "CandidateJob",
                newName: "CandidatesId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateJob_JobId",
                table: "CandidateJob",
                newName: "IX_CandidateJob_JobsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateJob_Candidates_CandidatesId",
                table: "CandidateJob",
                column: "CandidatesId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateJob_Jobs_JobsId",
                table: "CandidateJob",
                column: "JobsId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
