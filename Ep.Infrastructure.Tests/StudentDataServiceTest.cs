using Xunit;
using System.Linq;
using EP.Core.Entities;
using FluentAssertions;
using System.Threading.Tasks;
using EC.Infrastructure.Data;
using System.Collections.Generic;
using EC.Infrastructure.DTO.Student;
using Microsoft.EntityFrameworkCore;
using EC.Infrastructure.DTO.Subjects;
using EC.Infrastructure.Data.Services;
using Ep.Infrastructure.Tests.Helpers;

namespace Ep.Infrastructure.Tests
{
    public class StudentDataServiceTest
    {
        [Fact]
        public async Task Creation_student_success()
        {
            // Arrange
            await using EPContext context = await DbContextFactory.CreateContext();
            StudentDataService studentDataService = new(context);
            CreateStudentRequest request = new()
            {
                Name = "Иван",
                Surname = "Иванов",
                GroupNumber = "ЭИБ11"
            };

            // Act
            int studentId = await studentDataService.CreateStudentAsync(request);

            // Assert
            Student? student = await context.Students.FirstOrDefaultAsync(x => x.Id == studentId);
            request.Should().NotBeNull();
            request.Name.Should().Be(student!.Name);
            request.Surname.Should().Be(student.Surname);
            request.GroupNumber.Should().Be(student.GroupNumber);
        }

        [Fact]
        public async Task Set_grade_success()
        {
            // Arrange
            Grade grade = new ()
            {
                Value = 1,
                SubjectName = "Math",
                StudentId = 1
            };
            await using EPContext context = await DbContextFactory.CreateContext();
            context.Grades.Add(grade);
            await context.SaveChangesAsync();
            StudentDataService studentDataService = new(context);
            SetGradeRequest gradeRequest = new ()
            {
                StudentId = 1,
                ValuesBySubjects = new Dictionary<string, byte>()
                {
                    {grade.SubjectName, 2}
                }
            };
            
            // Act
            await studentDataService.SetGradeAsync(gradeRequest);
        
            // Assert
            Grade? actualGrade = await context.Grades.FirstOrDefaultAsync(x => x.Id == grade.Id);
            actualGrade.Should().NotBeNull();
            actualGrade!.Value
                .Should()
                .Be(gradeRequest.ValuesBySubjects[grade.SubjectName]);
        }

        [Fact]
        public async Task Get_students_by_avg_grade_ascending_success()
        {
            // Arrange
            await using EPContext context = await DbContextFactory.CreateContext();
            Student studentAlex = new ()
            {
                Name = "Alex",
                Surname = "Alexov",
                GroupNumber = "ЭИБ11",
                Grades = new List<Grade>()
                {
                    new ()
                    {
                        StudentId = 1,
                        Value = 4,
                        SubjectName = "Math"
                    },
                    new ()
                    {
                        StudentId = 1,
                        Value = 5,
                        SubjectName = "Inform"
                    }
                }
            };
            Student studentIvan = new ()
            {
                Name = "Ivan",
                Surname = "Ivanov",
                GroupNumber = "ЭИБ21",
                Grades = new List<Grade>()
                {
                    new ()
                    {
                        StudentId = 2,
                        Value = 2,
                        SubjectName = "Math"
                    },
                    new ()
                    {
                        StudentId = 2,
                        Value = 1,
                        SubjectName = "Inform"
                    }
                }
            };
            context.Students.Add(studentIvan);
            context.Students.Add(studentAlex);
            await context.SaveChangesAsync();
            StudentDataService studentDataService = new (context);
            
            // Act
            StudentDto[] actualStudents = await studentDataService.GetStudentsByAvgGradeAscendingAsync();

            // Assert
            actualStudents.Should().NotBeNull();
            actualStudents
                .Select(x => x.Grades.Average(g => g.Value))
                .Should().BeInAscendingOrder();
        }

        [Fact]
        public async Task Get_excellent_students_success()
        {
            // Arrange
            await using EPContext context = await DbContextFactory.CreateContext();
            Student studentAlex = new ()
            {
                Name = "Alex",
                Surname = "Alexov",
                GroupNumber = "ЭИБ11",
                Grades = new List<Grade>()
                {
                    new ()
                    {
                        StudentId = 1,
                        Value = 4,
                        SubjectName = "Math"
                    },
                    new ()
                    {
                        StudentId = 1,
                        Value = 5,
                        SubjectName = "Inform"
                    }
                }
            };
            Student studentIvan = new ()
            {
                Name = "Ivan",
                Surname = "Ivanov",
                GroupNumber = "ЭИБ21",
                Grades = new List<Grade>()
                {
                    new ()
                    {
                        StudentId = 2,
                        Value = 2,
                        SubjectName = "Math"
                    },
                    new ()
                    {
                        StudentId = 2,
                        Value = 1,
                        SubjectName = "Inform"
                    }
                }
            };
            context.Students.Add(studentIvan);
            context.Students.Add(studentAlex);
            await context.SaveChangesAsync();
            StudentDataService studentDataService = new (context);
            
            // Act
            StudentShortDto[] sortStudents = await studentDataService.GetExcellentStudentsAsync();

            // Assert
            Student[] students = await context.Students.ToArrayAsync();
            sortStudents.Should().NotBeNull();
            sortStudents.Should()
                .BeEquivalentTo(students.Where(x => x.Grades.All(g => g.Value > 3)), 
                opt=>opt.Excluding(x=>x.Id)
                    .Excluding(x=>x.Name)
                    .Excluding(x=>x.FullName)
                    .Excluding(x=>x.Grades));
        }
    }
}