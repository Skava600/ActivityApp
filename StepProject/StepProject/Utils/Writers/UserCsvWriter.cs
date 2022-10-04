using StepProject.Models;
using StepProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepProject.Utils.Writers
{
    internal class UserCsvWriter : IUserSerializer
    {
        public void Write(IEnumerable<User> users, string filename)
        {
            using (TextWriter writer = new StreamWriter(filename, false, Encoding.Default))
            {
                foreach (User user in users)
                {
                    foreach(Workout workout in user.Workouts)
                    {
                        writer.WriteLine(
                          $"{workout.User.ToString(CultureInfo.InvariantCulture)}," +
                          $"{workout.Steps}," +
                          $"{workout.Status}," +
                          $"{workout.Rank}," +
                          $"{workout.Day}");
                    }
                }

            }
           
        }
    }
}
