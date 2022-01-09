namespace IsuExtra.Entities
{
    public class Lesson
    {
        public Lesson(string name, string time)
        {
            Name = name;
            Time = time;
        }

        public string Name { get; }
        public string Time { get; }
    }
}