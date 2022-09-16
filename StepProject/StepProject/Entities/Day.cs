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

        public Day(IList<Workout> workouts)
        {
            this.workouts = workouts;
        }
    }
}
