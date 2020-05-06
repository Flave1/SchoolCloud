using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolCloud.Data.Migrations
{
    public partial class AddedStudentNDSTAFFOBJ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlatformUserId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "MembershipId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    AttendanceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(nullable: false),
                    LevelId = table.Column<int>(nullable: false),
                    TrimeStar = table.Column<int>(nullable: false),
                    Attended = table.Column<int>(nullable: false),
                    Date = table.Column<int>(nullable: false),
                    FormTeacherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.AttendanceId);
                });

            migrationBuilder.CreateTable(
                name: "ScoreRecords",
                columns: table => new
                {
                    ScoreRecordId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<string>(nullable: true),
                    SubjectId = table.Column<int>(nullable: false),
                    Assignment = table.Column<double>(nullable: false),
                    Tesst = table.Column<double>(nullable: false),
                    Examination = table.Column<double>(nullable: false),
                    FinalGrade = table.Column<double>(nullable: false),
                    TrimeStar = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    FormTeacherId = table.Column<int>(nullable: false),
                    ClassId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreRecords", x => x.ScoreRecordId);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    StaffId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    StaffMembeershipID = table.Column<string>(nullable: true),
                    FristName = table.Column<string>(nullable: true),
                    SurName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    PlaceOfBirth = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    Carnet = table.Column<string>(nullable: true),
                    TelePhone = table.Column<string>(nullable: true),
                    MobilePhone = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    LGA = table.Column<string>(nullable: true),
                    NearestBustop = table.Column<string>(nullable: true),
                    DateHired = table.Column<DateTime>(nullable: false),
                    YearsOfService = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Observations = table.Column<string>(nullable: true),
                    Specialty = table.Column<string>(nullable: true),
                    Salary = table.Column<int>(nullable: false),
                    StaffTypeId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    GradeParaleloId = table.Column<string>(nullable: true),
                    FatherName = table.Column<string>(nullable: true),
                    MotherName = table.Column<string>(nullable: true),
                    FatherPhone = table.Column<string>(nullable: true),
                    MotherPhone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.StaffId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    StudentMembeershipID = table.Column<string>(nullable: true),
                    FristName = table.Column<string>(nullable: true),
                    SurName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    PlaceOfBirth = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Carnet = table.Column<string>(nullable: true),
                    TelePhone = table.Column<string>(nullable: true),
                    MobilePhone = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    LGA = table.Column<string>(nullable: true),
                    NearestBustop = table.Column<string>(nullable: true),
                    Observations = table.Column<string>(nullable: true),
                    GradeParaleloId = table.Column<int>(nullable: false),
                    FatherName = table.Column<string>(nullable: true),
                    MotherName = table.Column<string>(nullable: true),
                    FatherPhone = table.Column<string>(nullable: true),
                    MotherPhone = table.Column<string>(nullable: true),
                    FatherProfession = table.Column<string>(nullable: true),
                    MotherProfession = table.Column<string>(nullable: true),
                    FatherPlaceOfWork = table.Column<string>(nullable: true),
                    MotherPlaceOfWork = table.Column<string>(nullable: true),
                    LastNameFather = table.Column<string>(nullable: true),
                    LastNameMother = table.Column<string>(nullable: true),
                    GuardianHomeAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "ScoreRecords");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropColumn(
                name: "MembershipId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "PlatformUserId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
