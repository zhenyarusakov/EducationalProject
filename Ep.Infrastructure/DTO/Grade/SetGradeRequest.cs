using System.Collections.Generic;

namespace EC.Infrastructure.DTO.Subjects
{
    public record SetGradeRequest
    {
        public Dictionary<string, byte> ValuesBySubjects { get; init; }
        public int StudentId { get; init; }
    }
}