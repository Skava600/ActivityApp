using StepProject.Models;
using StepProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace StepProject.Utils.Writers
{
    internal class UserXmlWriter : IUserSerializer
    {

        public void Write(IEnumerable<User> users, string filename)
        {
            XmlSerializer x = new XmlSerializer(typeof(List<User>));
            
            TextWriter writer = new StreamWriter(filename);
            x.Serialize(writer, new List<User>(users));
        }
    }
}
