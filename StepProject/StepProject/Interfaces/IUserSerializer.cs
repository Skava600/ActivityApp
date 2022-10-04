using StepProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepProject.Interfaces
{
    internal interface IUserSerializer
    {
        void Write(IEnumerable<User> users, string filename);
    }
}
