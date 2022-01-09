using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Isu.Entities;

namespace IsuExtra.Entities
{
    public class Ognp
    {
        public Ognp(string name, string megaFaculty)
        {
            Name = name;
            FlowsList = new List<Flow>();
            MegaFaculty = megaFaculty;
            Students = new List<Student>();
        }

        public string Name { get; }
        public List<Flow> FlowsList { get; }
        public List<Student> Students { get; }
        public string MegaFaculty { get; }
    }
}