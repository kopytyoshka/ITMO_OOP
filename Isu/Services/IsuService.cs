using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Services;
using Isu.Tools;

namespace Isu.Service
{
    public class IsuService : IIsuService
    {
        private List<Group> Groups { get; } = new List<Group>();
        private List<Student> GlobalStudentList { get; } = new List<Student>();

        public Group AddGroup(string name)
        {
            if (name.StartsWith("M3"))
            {
                Group group = new Group(name);
                Groups.Add(group);
                return group;
            }
            else
            {
                throw new IsuException("Wrong Group Name");
            }
        }

        public Student AddStudent(Group group, string name)
        {
            if (group.Students.Count < 22)
            {
                Student student = new Student(name);
                GlobalStudentList.Add(student);
                group.Students.Add(student);
                return student;
            }
            else
            {
                throw new IsuException("Not enough space for student");
            }
        }

        public Student GetStudent(int id)
        {
            foreach (Student iStudent in GlobalStudentList)
            {
                if (id == iStudent.Id)
                {
                    return iStudent;
                }
            }

            throw new IsuException("Didn't found student");
        }

        public Student FindStudent(string name)
        {
            foreach (Student iStudent in GlobalStudentList)
            {
                if (name == iStudent.Name)
                {
                    return iStudent;
                }
            }

            return null;
        }

        public List<Student> FindStudents(string groupName)
        {
            foreach (Group iGroup in Groups)
            {
                if (iGroup.Name.Name == groupName)
                {
                    return iGroup.Students;
                }
            }

            return null;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            List<Student> courseStudentList = new List<Student>();
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
            foreach (Group iGroup in Groups)
            {
                if (iGroup.Name.Name == groupName)
                {
                    return iGroup;
                }
            }

            return null;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            List<Group> courseGroupList = new List<Group>();
            foreach (Group iGroup in Groups)
            {
                if (iGroup.Name.Course == courseNumber)
                {
                    courseGroupList.Add(iGroup);
                }
            }

            return courseGroupList;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            foreach (Group iGroup in Groups)
            {
                foreach (Student iStudent in iGroup.Students)
                {
                    if (iStudent.Id == student.Id)
                    {
                        if (newGroup.Students.Count < 22)
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