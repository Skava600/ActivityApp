using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepProjectModels
{
    public class Workout : IEquatable<Workout>
    {
        public uint Rank { get; set; }
        public string Status { get; set; } = "";
        public uint Steps { get; set; }
        public uint Day { get; set; }

        public Workout()
        {

        }

        public bool Equals(Workout? other)
        {
            if (other == null)
                return false;
            return this.Rank == other.Rank &&
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
            return (Rank, Status, Steps,Day).GetHashCode();
        }
    }
}
