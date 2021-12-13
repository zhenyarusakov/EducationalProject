namespace EP.Core.Entities
{
    public class Grade : IEntity
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public byte Value { get; set; }
    }
}