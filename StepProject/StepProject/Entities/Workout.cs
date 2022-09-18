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

    public class Workout : IEquatable<Workout>
    {
        public int Rank { get; set; }
        public string User { get; set; } = "";
        public string Status { get; set; } = "";
        public int Steps { get; set; }
        public int Day { get; set; }

        public Workout()
        {

        }

        public bool Equals(Workout? other)
        {
            if (other == null)
                return false;
            return this.Rank == other.Rank &&
                this.User == other.User &&
                this.Status == other.Status &&
                this.Steps == other.Steps &&
                this.Day == other.Day;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Workout);
        }

        public override int GetHashCode()
        {
            return (Rank, User, Status, Steps, Day).GetHashCode();
        }
    }
}
