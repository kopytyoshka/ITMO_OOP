using System.Collections.Generic;

namespace Isu.Entities
{
    public class Group
    {
        public Group(GroupName groupName)
        {
            Name = groupName;
            Students = new List<Student>();
        }

        public List<Student> Students { get; }
        public GroupName Name { get;  }
    }
}