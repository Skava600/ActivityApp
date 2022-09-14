using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepProject.Entities
{
    public class Day
    {
        public IList<Workout> workouts;
        int number;

        public Day(IList<Workout> workouts, int number)
        {
            this.workouts = workouts;
            this.number = number;
        }
    }
}
