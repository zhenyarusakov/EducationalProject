namespace EC.Infrastructure.DTO.Student
{
    public record StudentShortDto
    {
        public string Surname { get; init; }
        public string GroupNumber { get; init; }
    }
}