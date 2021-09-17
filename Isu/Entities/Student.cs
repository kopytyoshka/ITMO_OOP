using System.Dynamic;

namespace Isu.Entities
{
    public class Student
    {
        private static int _idCounter = 100000;

        internal Student(string name)
        {
            Id = ++_idCounter;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }
    }
}