using StepProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StepsDataDeserializer
{
    public class UserDeserializer
    {
        private readonly string[] files;

        public UserDeserializer(string[] files)
        {
            this.files = files;
        }

        public IList<User> ReadAllData()
        {
            var users = new List<User>();
            IList<List<WorkoutObject>> days = new List<List<WorkoutObject>>();

            string pattern = @".*\\day(?<day>[0-9]{1,2}).json$";
            Regex regex = new Regex(pattern);

            foreach (var file in files)
            {
                Match match = regex.Match(file);
                if (match.Success)
                {
                    int dayNumber = Convert.ToInt32(match.Groups["day"].Value);
                    string jsonString = File.ReadAllText(file);
                    List<WorkoutObject> workouts = JsonSerializer.Deserialize<List<WorkoutObject>>(jsonString) ?? new List<WorkoutObject>();


                    foreach (var workout in workouts)
                    {
                        var user = users.Find(u => u.Name.Equals(workout.User));
                        if (user == null)
                        {
                            user = new User();
                            user.Name = workout.User;
                            users.Add(user);
                        }

                        user.Workouts.Add(new Workout
                        {
                            Day = (uint)dayNumber,
                            Status = workout.Status,
                            Steps = (uint)workout.Steps,
                            Rank = (uint)workout.Rank,
                        });
                    }
                }
            }

            return users;
        }
    }
}
