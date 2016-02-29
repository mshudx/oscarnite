using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Mshudx.OscarNite.Web.Migrations
{
    public partial class questionorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Answer_Option_OptionId", table: "Answer");
            migrationBuilder.DropForeignKey(name: "FK_Answer_Question_QuestionId", table: "Answer");
            migrationBuilder.DropForeignKey(name: "FK_Answer_Vote_VoteId", table: "Answer");
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Question",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Option_OptionId",
                table: "Answer",
                column: "OptionId",
                principalTable: "Option",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Vote_VoteId",
                table: "Answer",
                column: "VoteId",
                principalTable: "Vote",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Answer_Option_OptionId", table: "Answer");
            migrationBuilder.DropForeignKey(name: "FK_Answer_Question_QuestionId", table: "Answer");
            migrationBuilder.DropForeignKey(name: "FK_Answer_Vote_VoteId", table: "Answer");
            migrationBuilder.DropColumn(name: "Order", table: "Question");
            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Option_OptionId",
                table: "Answer",
                column: "OptionId",
                principalTable: "Option",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Vote_VoteId",
                table: "Answer",
                column: "VoteId",
                principalTable: "Vote",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
