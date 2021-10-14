using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Isu.Entities;
using Isu.Tools;
using Microsoft.VisualBasic;
namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        public const int MaxStudentsInAGroup = 23;
        private List<string> _possibleGroupNames = new List<string> { "M3" };
        private List<Group> _groups = new List<Group>();
        private List<Student> GlobalStudentList { get; } = new List<Student>();
        public Group AddGroup(string name)
        {
            bool check = false;
            foreach (var megaFaculty in _possibleGroupNames)
            {
                if (name.Substring(0, 2) == megaFaculty)
                {
                    check = true;
                }
            }

            if (!check)
            {
                throw new IsuException("Wrong Group Name");
            }

            var group = new Group(new GroupName(name));
            _groups.Add(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            if (group.Students.Count >= IsuService.MaxStudentsInAGroup)
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
            foreach (Group group in _groups)
            {
                if (group.Name.Name == groupName)
                {
                    return group.Students;
                }
            }

            return Enumerable.Empty<Student>() as List<Student>;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var courseStudentList = new List<Student>();
            foreach (Group iGroup in _groups)
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
            return _groups.Find(iGroup => iGroup.Name.Name == groupName);
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _groups.Where(iGroup => iGroup.Name.Course == courseNumber).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            foreach (Group iGroup in _groups)
            {
                foreach (Student iStudent in iGroup.Students)
                {
                    if (iStudent.Id == student.Id)
                    {
                        if (newGroup.Students.Count < IsuService.MaxStudentsInAGroup)
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