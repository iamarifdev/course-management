﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagement.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class FixStudentCourseClassForeignKeyRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_student_course_classes_courses_class_id",
                schema: "public",
                table: "student_course_classes");

            migrationBuilder.DropIndex(
                name: "ix_student_course_classes_student_id_course_id_class_id",
                schema: "public",
                table: "student_course_classes");

            migrationBuilder.CreateIndex(
                name: "ix_student_course_classes_course_id",
                schema: "public",
                table: "student_course_classes",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_student_course_classes_student_id_class_id",
                schema: "public",
                table: "student_course_classes",
                columns: new[] { "student_id", "class_id" },
                unique: true,
                filter: "\"is_deleted\" = false");

            migrationBuilder.AddForeignKey(
                name: "fk_student_course_classes_courses_course_id",
                schema: "public",
                table: "student_course_classes",
                column: "course_id",
                principalSchema: "public",
                principalTable: "courses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_student_course_classes_courses_course_id",
                schema: "public",
                table: "student_course_classes");

            migrationBuilder.DropIndex(
                name: "ix_student_course_classes_course_id",
                schema: "public",
                table: "student_course_classes");

            migrationBuilder.DropIndex(
                name: "ix_student_course_classes_student_id_class_id",
                schema: "public",
                table: "student_course_classes");

            migrationBuilder.CreateIndex(
                name: "ix_student_course_classes_student_id_course_id_class_id",
                schema: "public",
                table: "student_course_classes",
                columns: new[] { "student_id", "course_id", "class_id" },
                unique: true,
                filter: "\"is_deleted\" = false");

            migrationBuilder.AddForeignKey(
                name: "fk_student_course_classes_courses_class_id",
                schema: "public",
                table: "student_course_classes",
                column: "class_id",
                principalSchema: "public",
                principalTable: "courses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
