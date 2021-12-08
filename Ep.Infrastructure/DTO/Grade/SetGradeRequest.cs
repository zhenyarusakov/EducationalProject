namespace EC.Infrastructure.DTO.Subjects
{
    public record SetGradeRequest
    {
        public string SubjectName { get; init; }
        public int StudentId { get; init; }
        public int Value { get; init; }
    }
}