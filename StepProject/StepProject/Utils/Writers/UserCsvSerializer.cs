using StepProjectModels;
using StepsAnylyzerSerialize.Serializers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepProject.Utils.Writers
{
    internal class UserCsvSerializer<T> : ITemplateSerializer<User>
    {
        public void Write(IEnumerable<User> users, string filename)
        {
            using (TextWriter writer = new StreamWriter(filename, false, Encoding.Default))
            {
                foreach (User user in users)
                {
                    writer.WriteLine(user.ToString());
                    foreach(Workout workout in user.Workouts)
                    {
                        writer.WriteLine(
                          $"{workout.Steps}," +
                          $"{workout.Status}," +
                          $"{workout.Rank}," +
                          $"{workout.Day}");
                    }

                    writer.WriteLine();
                }

            }
           
        }
    }
}
