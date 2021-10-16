namespace Isu.Entities
{
    public class Student
    {
        private int _idCounter;
        internal Student(string name)
        {
            Name = name;
            Id = GenerateId();
        }

        public int Id { get; }
        public string Name { get; }

        private int GenerateId()
        {
            return ++_idCounter;
        }
    }
}