using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepProject.Entities
{
    public  class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double AverageSteps { get; set; }
        public int MinSteps { get; set; }
        public int MaxSteps { get; set; }
        public List<Workout> Workouts { get; set; } = new List<Workout>();

        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
