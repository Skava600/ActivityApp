using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StepProject.Entities
{
    [XmlRoot("User",IsNullable = false)]
    public  class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public double AverageSteps { get; set; }
        public int MinSteps { get; set; }
        public int MaxSteps { get; set; }
        [XmlArray("Workouts")]
        public List<Workout> Workouts { get; set; } = new List<Workout>();

        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }
        
        public User()
        {

        }

        [XmlIgnoreAttribute]
        [JsonIgnore]
        public bool HasUnstableWorkouts
        {
            get
            {
                if (AverageSteps - MinSteps > AverageSteps * 0.2 ||
                    MaxSteps - AverageSteps > AverageSteps * 0.2)
                    return true;
                return false;
            }
        }
    }
}
