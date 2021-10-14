using Isu.Entities;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;
namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group group = _isuService.AddGroup("M3209");
            Student student = _isuService.AddStudent(group, "Kopytyoshka");
            Assert.Contains(student, group.Students);
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup("M3209");
                for (int i = 0; i < IsuService.MaxStudentsInAGroup + 1; i++)
                {
                    _isuService.AddStudent(group, "Kopytyoshka");
                }

            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddGroup("M3111");
                _isuService.AddGroup("P3211");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group oldGroup = _isuService.AddGroup("M3210");
                Student studentToMove = _isuService.AddStudent(oldGroup, "Kirill");
                Group newGroup = _isuService.AddGroup("M3209");
                for (int i = 0; i < IsuService.MaxStudentsInAGroup; i++)
                {
                    _isuService.AddStudent(newGroup, "Kopytyoshka");
                }

                _isuService.ChangeStudentGroup(studentToMove, newGroup);
            });
        }
    }
}