using System.Collections.Generic;
using System.Linq;

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

        public void RemoveStudent(Student studentRemove)
        {
            foreach (var student in Students.Where(student => studentRemove == student))
            {
                Students.Remove(studentRemove);
            }
        }

        public void AddStudent(Student studentAdd)
        {
            Students.Add(studentAdd);
        }
    }
}