using System.Threading.Tasks;
using EC.Infrastructure.DTO.Student;
using EC.Infrastructure.DTO.Subjects;

namespace EC.Infrastructure.Abstractions
{
    public interface IStudentDataService
    {
        Task<int> CreateStudentAsync(CreateStudentRequest createStudentRequest);
        Task AddGradeAsync(SetGradeRequest setGradeRequest);
        Task<StudentDto[]> GetStudentsByAvgGradeAscendingAsync();
        Task<StudentShortDto[]> GetExcellentStudentsAsync();
    }
}