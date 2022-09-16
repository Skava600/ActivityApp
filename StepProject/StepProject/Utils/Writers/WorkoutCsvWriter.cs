using StepProject.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepProject.Utils.Writers
{
    internal class WorkoutCsvWriter
    {
        private TextWriter writer;

        public WorkoutCsvWriter(TextWriter writer)
        {
            this.writer = writer;
        }

        public void Write(Workout workout)
        {
            this.writer.WriteLine(
                $"{workout.User.ToString(CultureInfo.InvariantCulture)}," +
                $"{workout.Steps}," +
                $"{workout.Status}," +
                $"{workout.Rank}," +
                $"{workout.Day}");
        }
    }
}
