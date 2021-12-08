namespace EC.Infrastructure.DTO.Student
{
    public record CreateStudentRequest
    {
        public string Name { get; init; }
        public string Surname { get; init; }
        public string GroupNumber { get; init; }
    }
}