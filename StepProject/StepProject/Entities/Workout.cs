using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepProject.Entities
{

    enum Status
    { 
        Finished,
        Rejected,
    }

    public class Workout
    {
        public int Rank { get; set; }
        public string User { get; set; } = "";
        public string Status { get; set; } = "";
        public int Steps { get; set; }
        public int Day { get; set; }

        public Workout()
        {

        }
    }
}
