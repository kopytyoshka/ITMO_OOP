using System;
using System.Collections.Generic;
using System.Dynamic;
using Isu.Entities;

namespace IsuExtra.Entities
{
    public class Flow
    {
        public Flow(string flowName, int capacity)
        {
            Name = flowName;
            LessonsList = new List<Lesson>();
            Capacity = capacity;
            Students = new List<Student>();
        }

        public string Name { get; }
        public int Capacity { get; }
        public List<Student> Students { get; }
        public List<Lesson> LessonsList { get; }
    }
}