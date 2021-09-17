using System.Collections.Generic;

namespace Isu.Entities
{
    public class Group
    {
        public Group(string groupName)
        {
            Name = new GroupName(groupName);
            Students = new List<Student>();
        }

        public List<Student> Students { get; }
        public GroupName Name { get;  }
    }
}