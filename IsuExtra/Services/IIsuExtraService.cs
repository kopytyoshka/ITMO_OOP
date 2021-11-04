using System.Collections.Generic;
using Isu.Entities;
using IsuExtra.Entities;

namespace IsuExtra.Services
{
    public interface IIsuExtraService
    {
        Ognp AddOgnp(string name, string megaFaculty);
        Lessons AddLesson(string name, string time);
        Flows AddFlow(string name, int size);
        Flows AddLessonFlow(Lessons lessons, Flows flow);
        Ognp AddFlowOgnp(Flows flow, Ognp ognp);
        void AddStudentOgnp(Student student, Ognp ognp, Flows flow, GroupLessons groupLessons);
        GroupLessons AddGroupLessons(string name, string time, GroupName groupName);
        void RemoveStudentOgnp(Ognp ognp, Flows flow, Student student);
        List<Student> GetStudentsFlow(Flows flow);
        List<Student> GetStudentsOgnp(Ognp ognp);
        List<Student> GetStudentsWithoutOgnp(Group group);
    }
}