using Isu.Entities;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;
namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;
        private uint _maxStudentsGroup;

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _maxStudentsGroup = 23;
            _isuService = new IsuService(_maxStudentsGroup);
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
            Group group = _isuService.AddGroup("M3209");
            Assert.Catch<IsuException>(() =>
            {
                for (int i = 0; i < _maxStudentsGroup + 1; i++)
                {
                    _isuService.AddStudent(group, "Kopytyoshka");
                }

            });
        }
        
        [TestCase("P3211")]
        [TestCase("M31111")]
        [TestCase("M31M1")]
        public void CreateGroupWithInvalidName_ThrowException(string group)
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddGroup(group);
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group oldGroup = _isuService.AddGroup("M3210");
            Student studentToMove = _isuService.AddStudent(oldGroup, "Kirill");
            Group newGroup = _isuService.AddGroup("M3209");
            for (int i = 0; i < _maxStudentsGroup; i++)
            {
                _isuService.AddStudent(newGroup, "Kopytyoshka");
            }

            Assert.Catch<IsuException>(() =>
            {
                _isuService.ChangeStudentGroup(studentToMove, newGroup);
            });
        }
    }
}