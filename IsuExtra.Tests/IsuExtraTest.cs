using System;
using NUnit.Framework;
using Isu.Entities;
using Isu.Services;
using IsuExtra.Services;
using IsuExtra.Entities;

namespace IsuExtra.Tests
{
    public class Tests
    {
        private IIsuExtraService _isuExtra;
        private IIsuService _isuService;
        private uint _maxStudentGroup;

        [SetUp]
        public void Setup()
        {
            _maxStudentGroup = 23;
            _isuExtra = new IsuExtraService();
            _isuService = new IsuService(_maxStudentGroup);
        }

        [Test]
        public void OgnpFlowLessonStudentExists()
        {
            Lesson lesson = _isuExtra.AddLesson("Науки о Жизни", "8:20");
            Flow flow = _isuExtra.AddFlow("БиоМио", 20);
            _isuExtra.AddLessonFlow(lesson, flow);
            Ognp ognp = _isuExtra.AddOgnp("Наука бука", "M3");
            _isuExtra.AddFlowOgnp(flow, ognp);
            Group group = _isuService.AddGroup("P3209");
            Student student = _isuService.AddStudent(group, "Misha");
            _isuExtra.AddGroupLessons("Физика", "10:00", group.Name);
            _isuExtra.AddStudentOgnp(student, ognp, flow, group);
            Assert.AreEqual(ognp.Students, new[] {student});
            Assert.AreEqual(flow.Students, new[] {student});
            Assert.NotNull(lesson);
        }

        [Test]
        public void StudentRemovedOgnp()
        {
            Lesson lesson = _isuExtra.AddLesson("2", "222");
            Flow flow = _isuExtra.AddFlow("jj", 20);
            _isuExtra.AddLessonFlow(lesson, flow);
            Ognp ognp = _isuExtra.AddOgnp("Наука бука", "M3");
            _isuExtra.AddFlowOgnp(flow, ognp);
            Group group = _isuService.AddGroup("P3209");
            Student student = _isuService.AddStudent(group, "Misha");
            _isuExtra.AddGroupLessons("Физика", "10:00", group.Name);
            _isuExtra.AddStudentOgnp(student, ognp, flow, group);
            _isuExtra.RemoveStudentOgnp(ognp, flow, student);
            Assert.False(ognp.Students.Contains(student));
        }

        [Test]
        public void EnoughSpaceForStudentInAFlow_ThrowException()
        {
            Lesson lesson = _isuExtra.AddLesson("2", "222");
            Flow flow = _isuExtra.AddFlow("jj", 20);
            _isuExtra.AddLessonFlow(lesson, flow);
            Ognp ognp = _isuExtra.AddOgnp("Наука бука", "M3");
            _isuExtra.AddFlowOgnp(flow, ognp);
            Group group = _isuService.AddGroup("P3209");
            _isuExtra.AddGroupLessons("Физика", "10:00", group.Name);
            for (int i = 0; i < flow.Capacity; i++)
            {
                _isuExtra.AddStudentOgnp(_isuService.AddStudent(group, "Misha"), ognp, flow, group);
            }

            Assert.Catch<Exception>(() =>
            {
                _isuExtra.AddStudentOgnp(_isuService.AddStudent(group, "Misha"), ognp, flow, group);
            });
        }

        [Test]
        public void GetStudentsByFlow()
        {
            Lesson lesson = _isuExtra.AddLesson("2", "222");
            Flow flow = _isuExtra.AddFlow("jj", 20);
            _isuExtra.AddLessonFlow(lesson, flow);
            Ognp ognp = _isuExtra.AddOgnp("Наука бука", "M3");
            _isuExtra.AddFlowOgnp(flow, ognp);
            Group group = _isuService.AddGroup("P3209");
            _isuExtra.AddGroupLessons("Физика", "10:00", group.Name);
            for (int i = 0; i < flow.Capacity; i++)
            {
                _isuExtra.AddStudentOgnp(_isuService.AddStudent(group, "Misha"), ognp, flow, group);
            }

            Assert.AreEqual(_isuExtra.GetStudentsFlow(flow), flow.Students);
        }

        [Test]
        public void GetStudentsByOgnp()
        {
            Lesson lesson = _isuExtra.AddLesson("2", "222");
            Flow flow = _isuExtra.AddFlow("jj", 20);
            Flow flows2 = _isuExtra.AddFlow("jj", 20);
            _isuExtra.AddLessonFlow(lesson, flow);
            _isuExtra.AddLessonFlow(lesson, flows2);
            Ognp ognp = _isuExtra.AddOgnp("Наука бука", "M3");
            _isuExtra.AddFlowOgnp(flow, ognp);
            _isuExtra.AddFlowOgnp(flows2, ognp);
            Group group = _isuService.AddGroup("P3209");
            Group group2 = _isuService.AddGroup("P3210"); 
            _isuExtra.AddGroupLessons("Физика", "10:00", group.Name);
            for (int i = 0; i < flow.Capacity; i++)
            {
                _isuExtra.AddStudentOgnp(_isuService.AddStudent(group, "Misha"), ognp, flow, group);
                _isuExtra.AddStudentOgnp(_isuService.AddStudent(group2, "Kolya"), ognp, flows2, group);
            }

            Assert.AreEqual(_isuExtra.GetStudentsOgnp(ognp), ognp.Students);
        }

        [Test]
        public void GetStudentsOgnpFreeByGroup()
        {
            Lesson lesson = _isuExtra.AddLesson("2", "222");
            Flow flow = _isuExtra.AddFlow("jj", 20);
            _isuExtra.AddLessonFlow(lesson, flow);
            Ognp ognp = _isuExtra.AddOgnp("Наука бука", "M3");
            _isuExtra.AddFlowOgnp(flow, ognp);
            Group group = _isuService.AddGroup("P3209");
            Student student = _isuService.AddStudent(group, "kolya");
            _isuExtra.AddGroupLessons("Физика", "10:00", group.Name);
            _isuExtra.AddStudentOgnp(_isuService.AddStudent(group, "Misha"), ognp, flow, group);
            Assert.IsNotEmpty(_isuExtra.GetStudentsWithoutOgnp(group));
        }

        [Test]
        public void StudentGroupLessonsTimeOgnpTimeSame_ThrowException()
        {
            Lesson lesson = _isuExtra.AddLesson("НОЖ", "8:20");
            Flow flow = _isuExtra.AddFlow("jj", 20);
            _isuExtra.AddLessonFlow(lesson, flow);
            Ognp ognp = _isuExtra.AddOgnp("Наука бука", "M3");
            _isuExtra.AddFlowOgnp(flow, ognp);
            Group group = _isuService.AddGroup("P3209");
            Student student = _isuService.AddStudent(group, "Misha"); 
            _isuExtra.AddGroupLessons("Физика", "8:20", group.Name);
            _isuExtra.AddGroupLessons("Физика", "10:20", group.Name);
            Assert.Catch<Exception>(() =>
            {
                _isuExtra.AddStudentOgnp(student, ognp, flow, group);
            });
        }

        [Test]
        public void StudentMegafacultyAndOgnpMegafacultySame_ThrowException()
        {
            Lesson lesson = _isuExtra.AddLesson("Науки о Жизни", "8:20");
            Flow flow = _isuExtra.AddFlow("БиоМио", 20);
            _isuExtra.AddLessonFlow(lesson, flow);
            Ognp ognp = _isuExtra.AddOgnp("Наука бука", "M3");
            _isuExtra.AddFlowOgnp(flow, ognp);
            Group group = _isuService.AddGroup("M3209");
            Student student = _isuService.AddStudent(group, "Misha");
            _isuExtra.AddGroupLessons("Физика", "10:00", group.Name);
            Assert.Catch<Exception>(() =>
            {
                _isuExtra.AddStudentOgnp(student, ognp, flow, group);
            });
        }

        [Test]
        public void StudentAlreadyHasOgnp_ThrowException()
        {
            Lesson lesson = _isuExtra.AddLesson("Науки о Жизни", "8:20");
            Flow flow = _isuExtra.AddFlow("БиоМио", 20);
            _isuExtra.AddLessonFlow(lesson, flow);
            Ognp ognp = _isuExtra.AddOgnp("Наука бука", "M3");
            _isuExtra.AddFlowOgnp(flow, ognp);
            Lesson lesson2 = _isuExtra.AddLesson("Науки о Жизни", "8:20");
            Flow flow2 = _isuExtra.AddFlow("БиоМио", 20);
            _isuExtra.AddLessonFlow(lesson2, flow2);
            Ognp ognp2 = _isuExtra.AddOgnp("Наука бука", "M3");
            _isuExtra.AddFlowOgnp(flow2, ognp2);
            Group group = _isuService.AddGroup("P3209");
            Student student = _isuService.AddStudent(group, "Misha");
            _isuExtra.AddGroupLessons("Физика", "10:00", group.Name);
            _isuExtra.AddStudentOgnp(student, ognp, flow, group);
            Assert.Catch<Exception>(() =>
            {
                _isuExtra.AddStudentOgnp(student, ognp2, flow2, group);
            });
        }
    }
}