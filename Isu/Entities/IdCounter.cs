namespace Isu.Entities
{
    public class IdCounter
    {
        private static int _studentId = 100000;

        public IdCounter()
        {
            Id = _studentId++;
        }

        public int Id { get; }
    }
}