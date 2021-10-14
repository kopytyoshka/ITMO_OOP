namespace Isu.Entities
{
    public class Student
    {
        internal Student(string name)
        {
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }
    }
}