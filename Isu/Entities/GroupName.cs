namespace Isu.Entities
{
    public class GroupName
    {
            public GroupName(string groupname)
            {
                Name = groupname;
                Faculty = groupname.Substring(0, 2);
                Course = new CourseNumber(int.Parse(groupname.Substring(2, 1)));
                Group = int.Parse(groupname.Substring(3, 2));
            }

            public string Name { get; }
            public string Faculty { get; }
            public CourseNumber Course { get; }
            public int Group { get; }
    }
}