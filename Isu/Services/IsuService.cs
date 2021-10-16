using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private const int GroupNameLength = 5;
        private List<string> _possibleGroupNames = new List<string> { "M3" };
        private List<Group> _groups = new List<Group>();
        private List<Student> _globalStudentList = new List<Student>();
        private uint _maxStudentGroup;
        public IsuService(uint maxStudentGroup)
        {
            _maxStudentGroup = maxStudentGroup;
        }

        public Group AddGroup(string name)
        {
            bool check = false;
            foreach (var megaFaculty in _possibleGroupNames)
            {
                if (name.Substring(0, 2) == megaFaculty && name.Length == GroupNameLength && int.TryParse(name.Substring(2, 3), out int groupNumber))
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
            if (group.Students.Count >= _maxStudentGroup)
                throw new IsuException("Not enough space for student");
            var student = new Student(name);
            _globalStudentList.Add(student);
            group.Students.Add(student);
            return student;
        }

        public Student GetStudent(int id)
        {
            return _globalStudentList.Find(student => student.Id == id) ?? throw new IsuException("wrong student id");
        }

        public Student FindStudent(string name)
        {
            return _globalStudentList.Find(iStudent => name == iStudent.Name);
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
            return _groups.Where(iGroup => iGroup.Name.Course == courseNumber).SelectMany(iGroup => iGroup.Students).ToList();
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
            foreach (Group iGroup in from iGroup in _groups from iStudent in iGroup.Students where iStudent.Id == student.Id select iGroup)
            {
                if (newGroup.Students.Count < _maxStudentGroup)
                {
                    iGroup.RemoveStudent(student);
                    newGroup.AddStudent(student);
                }
                else
                {
                    throw new IsuException("Not enough space in new group for a student");
                }
            }
        }
    }
}