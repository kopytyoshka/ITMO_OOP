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
        Lessons lessons = _isuExtra.AddLesson("Науки о Жизни", "8:20");
        Flows flows = _isuExtra.AddFlow("БиоМио", 20);
        _isuExtra.AddLessonFlow(lessons, flows);
        Ognp ognp = _isuExtra.AddOgnp("Наука бука", "M3");
        _isuExtra.AddFlowOgnp(flows, ognp);
        Group group = _isuService.AddGroup("P3209");
        Student student = _isuService.AddStudent(group, "Misha");
        GroupLessons groupLessons = _isuExtra.AddGroupLessons("Физика", "10:00", group.Name);
        _isuExtra.AddStudentOgnp(student, ognp, flows, groupLessons);
        Assert.AreEqual(ognp.Students, new[] {student});
        Assert.AreEqual(flows.Students, new[] {student});
        Assert.NotNull(lessons);
        }

        [Test]
        public void StudentRemovedOgnp()
        {
            Lessons lessons = _isuExtra.AddLesson("2", "222");
            Flows flows = _isuExtra.AddFlow("jj", 20);
            _isuExtra.AddLessonFlow(lessons, flows);
            Ognp ognp = _isuExtra.AddOgnp("Наука бука", "M3");
            _isuExtra.AddFlowOgnp(flows, ognp);
            Group group = _isuService.AddGroup("P3209");
            Student student = _isuService.AddStudent(group, "Misha");
            GroupLessons groupLessons = _isuExtra.AddGroupLessons("Физика", "10:00", group.Name);
            _isuExtra.AddStudentOgnp(student, ognp, flows, groupLessons);
            _isuExtra.RemoveStudentOgnp(ognp, flows, student);
            Assert.False(ognp.Students.Contains(student));
        }

        [Test]
        public void EnoughSpaceForStudentInAFlow_ThrowException()
        {
            Lessons lessons = _isuExtra.AddLesson("2", "222");
            Flows flows = _isuExtra.AddFlow( "jj", 20);
            _isuExtra.AddLessonFlow(lessons, flows);
            Ognp ognp = _isuExtra.AddOgnp("Наука бука", "M3");
            _isuExtra.AddFlowOgnp(flows, ognp);
            Group group = _isuService.AddGroup("P3209");
            GroupLessons groupLessons = _isuExtra.AddGroupLessons("Физика", "10:00", group.Name);
            for (int i = 0; i < flows.Capacity; i++)
            {
                _isuExtra.AddStudentOgnp(_isuService.AddStudent(group, "Misha"), ognp, flows, groupLessons);
            }
            Assert.Catch<Exception>(() =>
            {
                _isuExtra.AddStudentOgnp(_isuService.AddStudent(group, "Misha"), ognp, flows, groupLessons);
            });
        }

        [Test]
        public void GetStudentsByFlow()
        {
            Lessons lessons = _isuExtra.AddLesson("2", "222");
            Flows flows = _isuExtra.AddFlow( "jj", 20);
            _isuExtra.AddLessonFlow(lessons, flows);
            Ognp ognp = _isuExtra.AddOgnp("Наука бука", "M3");
            _isuExtra.AddFlowOgnp(flows, ognp);
            Group group = _isuService.AddGroup("P3209");
            GroupLessons groupLessons = _isuExtra.AddGroupLessons("Физика", "10:00", group.Name);
            for (int i = 0; i < flows.Capacity; i++)
            {
                _isuExtra.AddStudentOgnp(_isuService.AddStudent(group, "Misha"), ognp, flows, groupLessons);
            }
            Assert.AreEqual(_isuExtra.GetStudentsFlow(flows), flows.Students);
        }

        [Test]
        public void GetStudentsByOgnp()
        {
            Lessons lessons = _isuExtra.AddLesson("2", "222");
            Flows flows = _isuExtra.AddFlow("jj", 20);
            Flows flows2 = _isuExtra.AddFlow("jj", 20);
            _isuExtra.AddLessonFlow(lessons, flows);
            _isuExtra.AddLessonFlow(lessons, flows2);
            Ognp ognp = _isuExtra.AddOgnp("Наука бука", "M3");
            _isuExtra.AddFlowOgnp(flows, ognp);
            _isuExtra.AddFlowOgnp(flows2, ognp);
            Group group = _isuService.AddGroup("P3209");
            Group group2 = _isuService.AddGroup("P3210");
            GroupLessons groupLessons = _isuExtra.AddGroupLessons("Физика", "10:00", group.Name);
            for (int i = 0; i < flows.Capacity; i++)
            {
                _isuExtra.AddStudentOgnp(_isuService.AddStudent(group, "Misha"), ognp, flows, groupLessons);
                _isuExtra.AddStudentOgnp(_isuService.AddStudent(group2, "Kolya"), ognp, flows2, groupLessons);
            }
            Assert.AreEqual(_isuExtra.GetStudentsOgnp(ognp), ognp.Students);
        }
        
        [Test]
        public void GetStudentsOgnpFreeByGroup()
        {
            Lessons lessons = _isuExtra.AddLesson("2", "222");
            Flows flows = _isuExtra.AddFlow("jj", 20);
            _isuExtra.AddLessonFlow(lessons, flows);
            Ognp ognp = _isuExtra.AddOgnp("Наука бука", "M3");
            _isuExtra.AddFlowOgnp(flows, ognp);
            Group group = _isuService.AddGroup("P3209");
            Student student = _isuService.AddStudent(group, "kolya");
            GroupLessons groupLessons = _isuExtra.AddGroupLessons("Физика", "10:00", group.Name);
            _isuExtra.AddStudentOgnp(_isuService.AddStudent(group, "Misha"), ognp, flows, groupLessons);
            Assert.IsNotEmpty(_isuExtra.GetStudentsWithoutOgnp(group));
        }

        [Test]
        public void StudentGroupLessonsTimeOgnpTimeSame_ThrowException()
        {
            Lessons lessons = _isuExtra.AddLesson("НОЖ", "8:20");
            Flows flows = _isuExtra.AddFlow("jj", 20);
            _isuExtra.AddLessonFlow(lessons, flows);
            Ognp ognp = _isuExtra.AddOgnp("Наука бука", "M3");
            _isuExtra.AddFlowOgnp(flows, ognp);
            Group group = _isuService.AddGroup("P3209");
            Student student = _isuService.AddStudent(group, "Misha");
            GroupLessons groupLessons = _isuExtra.AddGroupLessons("Физика", "8:20", group.Name);
            Assert.Catch<Exception>(() =>
            {
                _isuExtra.AddStudentOgnp(student, ognp, flows, groupLessons);
            });
        }

        [Test]
        public void StudentMegafacultyAndOgnpMegafacultySame_ThrowException()
        {
            Lessons lessons = _isuExtra.AddLesson("Науки о Жизни", "8:20");
            Flows flows = _isuExtra.AddFlow("БиоМио", 20);
            _isuExtra.AddLessonFlow(lessons, flows);
            Ognp ognp = _isuExtra.AddOgnp("Наука бука", "M3");
            _isuExtra.AddFlowOgnp(flows, ognp);
            Group group = _isuService.AddGroup("M3209");
            Student student = _isuService.AddStudent(group, "Misha");
            GroupLessons groupLessons = _isuExtra.AddGroupLessons("Физика", "10:00", group.Name);
            Assert.Catch<Exception>(() =>
            {
                _isuExtra.AddStudentOgnp(student, ognp, flows, groupLessons);
            });
        }
    }
    
}