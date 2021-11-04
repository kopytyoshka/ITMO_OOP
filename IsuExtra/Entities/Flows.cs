using System;
using System.Collections.Generic;
using System.Dynamic;
using Isu.Entities;

namespace IsuExtra.Entities
{
    public class Flows
    {
        public Flows(string flowName, int capacity)
        {
            Name = flowName;
            LessonsList = new List<Lessons>();
            Capacity = capacity;
            Students = new List<Student>();
        }

        public string Name { get; }
        public int Capacity { get; }
        public List<Student> Students { get; }
        public List<Lessons> LessonsList { get; }
    }
}