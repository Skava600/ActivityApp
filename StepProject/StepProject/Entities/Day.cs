using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepProject.Entities
{
    internal class Day
    {
        IList<Workout> workouts;

        public Day(IList<Workout> workouts)
        {
            this.workouts = workouts;
        }

        public Day()
        {
            this.workouts = new List<Workout>();
        }
    }
}
