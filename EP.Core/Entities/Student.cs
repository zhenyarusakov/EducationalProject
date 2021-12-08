using System.Collections.Generic;

namespace EP.Core.Entities
{
    public class Student : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        
        public string FullName => $"{Name} {Surname}";

        public string GroupNumber { get; set; }
        
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    }
}