using Isu.Entities;

namespace IsuExtra.Entities
{
    public class GroupLessons
    {
        public GroupLessons(string name, string time, GroupName groupName)
        {
            Name = name;
            Time = time;
            GroupName = groupName;
        }

        public GroupName GroupName { get; }
        public string Name { get; }
        public string Time { get; }
    }
}
