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
            FlowsList = new List<Flows>();
            MegaFaculty = megaFaculty;
            Students = new List<Student>();
        }

        public string Name { get; }
        public List<Flows> FlowsList { get; }
        public List<Student> Students { get; }
        public string MegaFaculty { get; }
    }
}