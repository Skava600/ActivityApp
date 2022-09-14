using StepProject.Entities;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace StepProject.Utils
{
    internal class WorkoutJsonReader
    {
        public WorkoutJsonReader()
        {
        }

        public IList<Workout> ReadAll(string fileName)
        {
            string jsonString = File.ReadAllText(fileName);
            IList<Workout> workouts = JsonSerializer.Deserialize<List<Workout>>(jsonString) ?? new List<Workout>();

            return workouts;
        }
    }
}
