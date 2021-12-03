using System.Collections.Generic;
using Isu.Entities;
using IsuExtra.Entities;

namespace IsuExtra.Services
{
    public interface IIsuExtraService
    {
        Ognp AddOgnp(string name, string megaFaculty);
        Lesson AddLesson(string name, string time);
        Flow AddFlow(string name, int size);
        Flow AddLessonFlow(Lesson lesson, Flow flow);
        Ognp AddFlowOgnp(Flow flow, Ognp ognp);
        void AddStudentOgnp(Student student, Ognp ognp, Flow flow, Group group);
        void AddGroupLessons(string name, string time, GroupName groupName);
        void RemoveStudentOgnp(Ognp ognp, Flow flow, Student student);
        List<Student> GetStudentsFlow(Flow flow);
        List<Student> GetStudentsOgnp(Ognp ognp);
        List<Student> GetStudentsWithoutOgnp(Group group);
    }
}