namespace EC.Infrastructure.DTO.Subjects
{
    public record GradeDto
    {
        public string SubjectName { get; init; }
        public int Value { get; init; }
    }
}