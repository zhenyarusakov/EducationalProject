using System.Collections.Generic;
using EC.Infrastructure.DTO.Subjects;

namespace EC.Infrastructure.DTO.Student
{
    public record StudentDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Surname { get; init; }
        public string FullName { get; init; }
        public string GroupNumber { get; init; }
        public ICollection<GradeDto> Grades { get; init; } = new List<GradeDto>();
    }
}