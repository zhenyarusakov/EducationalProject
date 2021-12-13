using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EC.Infrastructure.Abstractions;
using EC.Infrastructure.DTO.Student;
using EC.Infrastructure.DTO.Subjects;
using EP.Core.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EC.Infrastructure.Data.Services
{
    public class StudentDataService: IStudentDataService
    {
        private readonly EPContext _context;

        public StudentDataService(EPContext context)
        {
            _context = context;
        }
        
        public async Task<int> CreateStudentAsync(CreateStudentRequest createStudentRequest)
        {
            Student student = createStudentRequest.Adapt<Student>();
            
            _context.Students.Add(student);

            await _context.SaveChangesAsync();

            return student.Id;
        }

        public async Task SetGradeAsync(SetGradeRequest setGradeRequest)
        {
            var subjectNames = setGradeRequest.ValuesBySubjects.Select(x => x.Key);

            var grades = await _context.Grades
                .Where(x =>
                    x.StudentId == setGradeRequest.StudentId &&
                    subjectNames.Contains(x.SubjectName))
                .ToArrayAsync();

            foreach (var (subjectName, value) in setGradeRequest.ValuesBySubjects)
            {
                var grade = grades.FirstOrDefault(x => x.SubjectName == subjectName);
                
                if (grade == null)
                {
                    grade = new Grade
                    {
                        StudentId = setGradeRequest.StudentId,
                        SubjectName = subjectName,
                        Value = value
                    };

                    _context.Grades.Add(grade);
                }
                else
                {
                    grade.Value = value;
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<StudentDto[]> GetStudentsByAvgGradeAscendingAsync()
        {
            return await _context.Students.Include(x => x.Grades)
                .OrderBy(x => x.Grades.Average(g => g.Value))
                .ProjectToType<StudentDto>()
                .ToArrayAsync();
        }

        public async Task<StudentShortDto[]> GetExcellentStudentsAsync()
        {
            return await _context.Students.Include(x => x.Grades)
                .Where(x => x.Grades.All(g => g.Value > 3))
                .ProjectToType<StudentShortDto>()
                .ToArrayAsync();
        }
    }
}