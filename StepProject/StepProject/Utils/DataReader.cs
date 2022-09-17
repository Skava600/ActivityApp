using StepProject.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StepProject.Utils
{
    internal class DataReader
    {
        string[] files;

        public DataReader(string[] files)
        {
            this.files = files;
        }

        public IList<Day> ReadAllDays()
        {
            IList<Day> days = new List<Day>();

            WorkoutJsonReader reader = new WorkoutJsonReader();

            string pattern = @".*\\day(?<day>[0-9]{1,2}).json$";
            Regex regex = new Regex(pattern);

            foreach (var file in files)
            {
                Match match = regex.Match(file);
                if (match.Success)
                {
                    int dayNumber = Convert.ToInt32(match.Groups["day"].Value);
                    days.Add(new Day(reader.ReadAll(file, dayNumber)));
                }
            }

            return days;
        }
    }
}
