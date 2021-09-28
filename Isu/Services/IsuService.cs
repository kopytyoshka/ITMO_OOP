using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        public const int MaxStudentsInAGroup = 23;
        private List<Group> Groups { get; } = new List<Group>();
        private List<Student> GlobalStudentList { get; } = new List<Student>();

        public Group AddGroup(string name)
        {
            if (!name.StartsWith("M3"))
                throw new IsuException("Wrong Group Name");

            var group = new Group(name);
            Groups.Add(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            if (group.Students.Count >= MaxStudentsInAGroup)
                throw new IsuException("Not enough space for student");

            var student = new Student(name);
            GlobalStudentList.Add(student);
            group.Students.Add(student);
            return student;
        }

        public Student GetStudent(int id)
        {
            return GlobalStudentList.Find(student => student.Id == id) ?? throw new IsuException("wrong student id");
        }

        public Student FindStudent(string name)
        {
            return GlobalStudentList.Find(iStudent => name == iStudent.Name);
        }

        public List<Student> FindStudents(string groupName)
        {
            foreach (Group group in Groups)
            {
                if (group.Name.Name == groupName)
                {
                    return group.Students;
                }
            }

            return null;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var courseStudentList = new List<Student>();
            foreach (Group iGroup in Groups)
            {
                if (iGroup.Name.Course == courseNumber)
                {
                    foreach (Student iStudent in iGroup.Students)
                    {
                        courseStudentList.Add(iStudent);
                    }
                }
            }

            return courseStudentList;
        }

        public Group FindGroup(string groupName)
        {
            return Groups.Find(iGroup => iGroup.Name.Name == groupName);
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return Groups.Where(iGroup => iGroup.Name.Course == courseNumber).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            foreach (Group iGroup in Groups)
            {
                foreach (Student iStudent in iGroup.Students)
                {
                    if (iStudent.Id == student.Id)
                    {
                        if (newGroup.Students.Count < MaxStudentsInAGroup - 1)
                        {
                            newGroup.Students.Add(student);
                            iGroup.Students.Remove(student);
                        }
                        else
                        {
                            throw new IsuException("Not enough space in new group for a student");
                        }
                    }
                }
            }
        }
    }
}