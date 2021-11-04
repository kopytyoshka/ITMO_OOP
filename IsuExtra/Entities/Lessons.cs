namespace IsuExtra.Entities
{
    public class Lessons
    {
        public Lessons(string name, string time)
        {
            Name = name;
            Time = time;
        }

        public string Name { get; }
        public string Time { get; }
    }
}