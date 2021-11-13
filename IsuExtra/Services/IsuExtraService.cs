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
        private List<Lessons> _lessons = new ();
        private List<Flows> _flows = new ();
        private List<Student> _studentsWithOgnp = new ();

        public Ognp AddOgnp(string name, string megaFaculty)
        {
            var ognp = new Ognp(name, megaFaculty);
            _ognps.Add(ognp);
            return ognp;
        }

        public GroupLessons AddGroupLessons(string name, string time, GroupName groupName)
        {
            var groupLesson = new GroupLessons(name, time, groupName);
            return groupLesson;
        }

        public Lessons AddLesson(string name, string time)
        {
            var lesson = new Lessons(name, time);
            _lessons.Add(lesson);
            return lesson;
        }

        public Flows AddFlow(string name, int size)
        {
            var flow = new Flows(name, size);
            _flows.Add(flow);
            return flow;
        }

        public Flows AddLessonFlow(Lessons lessons, Flows flow)
        {
            foreach (Lessons vLesson in from vFlow in _flows
                where vFlow == flow
                from vLesson in _lessons
                where vLesson == lessons
                select vLesson)
            {
                flow.LessonsList.Add(lessons);
            }

            return flow;
        }

        public Ognp AddFlowOgnp(Flows flow, Ognp ognp)
        {
            foreach (Flows vflow in from vOgnp in _ognps
                where vOgnp == ognp
                from vflow in _flows
                where vflow == flow
                select vflow)
            {
                ognp.FlowsList.Add(flow);
            }

            return ognp;
        }

        public void AddStudentOgnp(Student student, Ognp ognp, Flows flow, GroupLessons groupLessons)
        {
            if (groupLessons.GroupName.Faculty == ognp.MegaFaculty)
            {
                throw new Exception("One Megafaculty");
            }

            if (flow.Capacity <= flow.Students.Count)
            {
                throw new Exception("Not enough space");
            }

            foreach (Ognp vOgnp in _ognps.Where(vOgnp => vOgnp == ognp))
            {
                {
                    foreach (Lessons variaLesson in _lessons)
                    {
                        if (flow.LessonsList.Contains(variaLesson) && variaLesson.Time != groupLessons.Time)
                        {
                            flow.Students.Add(student);
                            ognp.Students.Add(student);
                            _studentsWithOgnp.Add(student);
                        }
                        else
                        {
                            throw new Exception("Issue with schedule");
                        }
                    }
                }
            }
        }

        public void RemoveStudentOgnp(Ognp ognp, Flows flow, Student student)
        {
            foreach (Ognp vOgnp in _ognps.Where(vOgnp => vOgnp == ognp))
            {
                ognp.Students.Remove(student);
                flow.Students.Remove(student);
            }
        }

        public List<Student> GetStudentsOgnp(Ognp ognp)
        {
            return ognp.Students;
        }

        public List<Student> GetStudentsFlow(Flows flow)
        {
            return flow.Students;
        }

        public List<Student> GetStudentsWithoutOgnp(Group group)
        {
            return group.Students.Where(groupStudent => !_studentsWithOgnp.Contains(groupStudent)).ToList();
        }
    }
}