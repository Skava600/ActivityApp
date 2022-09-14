using StepProject.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepProject.Utils
{
    internal class DataReader
    {
        string dataPath;

        public DataReader(string dataPath)
        {
            this.dataPath = dataPath;
        }

        public IList<Day> ReadAllDays()
        {
            string[] files = Directory.GetFiles(dataPath, "*.json");
            IList<Day> days = new List<Day>();
            WorkoutJsonReader reader = new WorkoutJsonReader();
            foreach(var file in files)
            {
                
                days.Add(new Day(reader.ReadAll(file)));
            }

            return days;
        }
    }
}
