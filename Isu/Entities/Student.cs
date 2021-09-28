namespace Isu.Entities
{
    public class Student
    {
        internal Student(string name)
        {
            Id = new IdCounter().Id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }
    }
}