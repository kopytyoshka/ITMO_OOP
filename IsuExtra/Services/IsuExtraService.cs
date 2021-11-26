using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Services;
using IsuExtra.Entities;

namespace IsuExtra.Services
{
    public class IsuExtraService : IIsuExtraService
    {
        private List<Ognp> _ognps = new ();
        private List<Lesson> _lessons = new ();
        private List<Flow> _flows = new ();
        private List<Student> _studentsWithOgnp = new ();
        private List<GroupLesson> _groupLessons = new ();

        public Ognp AddOgnp(string name, string megaFaculty)
        {
            var ognp = new Ognp(name, megaFaculty);
            _ognps.Add(ognp);
            return ognp;
        }

        public void AddGroupLessons(string name, string time, GroupName groupName)
        {
            var groupLesson = new GroupLesson(name, time, groupName);
            _groupLessons.Add(groupLesson);
        }

        public Lesson AddLesson(string name, string time)
        {
            var lesson = new Lesson(name, time);
            _lessons.Add(lesson);
            return lesson;
        }

        public Flow AddFlow(string name, int size)
        {
            var flow = new Flow(name, size);
            _flows.Add(flow);
            return flow;
        }

        public Flow AddLessonFlow(Lesson lesson, Flow flow)
        {
            if (_flows.Contains(flow) && _lessons.Contains(lesson))
            {
                flow.LessonsList.Add(lesson);
            }

            return flow;
        }

        public Ognp AddFlowOgnp(Flow flow, Ognp ognp)
        {
            if (_ognps.Contains(ognp) && ognp.FlowsList.Contains(flow))
            {
                ognp.FlowsList.Add(flow);
            }

            return ognp;
        }

        public void AddStudentOgnp(Student student, Ognp ognp, Flow flow, Group group)
        {
            if (!_ognps.Contains(ognp)) return;
            if (_studentsWithOgnp.Contains(student))
            {
                throw new Exception("Student already choosed Ognp");
            }

            if (group.Name.Faculty == ognp.MegaFaculty)
            {
                throw new Exception("One Megafaculty");
            }

            if (flow.Capacity <= flow.Students.Count)
            {
                throw new Exception("Not enough space");
            }

            foreach (Lesson variaLesson in _lessons.Where(variaLesson => flow.LessonsList.Contains(variaLesson)))
            {
                if (_groupLessons.Any(groupLessons => groupLessons.Time == variaLesson.Time))
                {
                    throw new Exception("Issue with schedule");
                }

                flow.Students.Add(student);
                ognp.Students.Add(student);
                _studentsWithOgnp.Add(student);
            }
        }

        public void RemoveStudentOgnp(Ognp ognp, Flow flow, Student student)
        {
            if (!_ognps.Contains(ognp)) return;
            ognp.Students.Remove(student);
            flow.Students.Remove(student);
        }

        public List<Student> GetStudentsOgnp(Ognp ognp)
        {
            return ognp.Students;
        }

        public List<Student> GetStudentsFlow(Flow flow)
        {
            return flow.Students;
        }

        public List<Student> GetStudentsWithoutOgnp(Group group)
        {
            return group.Students.Where(groupStudent => !_studentsWithOgnp.Contains(groupStudent)).ToList();
        }
    }
}