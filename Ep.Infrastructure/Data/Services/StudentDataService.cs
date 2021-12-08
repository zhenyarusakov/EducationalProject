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
            var grade = await _context.Grades.FirstOrDefaultAsync(x => 
                x.StudentId == setGradeRequest.StudentId &&
                x.SubjectName == setGradeRequest.SubjectName);

            if (grade != null)
            {
                grade.Value = setGradeRequest.Value;
                
                return;
            }

            grade = setGradeRequest.Adapt<Grade>();

            _context.Grades.Add(grade);
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