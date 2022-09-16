using StepProject.Entities;
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
    internal class UserXmlWriter
    {
        private string fileName;

        public UserXmlWriter(string fileName)
        {
            this.fileName = fileName;
        }

        public void Write(User user)
        {
            XmlSerializer x = new XmlSerializer(typeof(User));
            
            TextWriter writer = new StreamWriter(fileName);
            x.Serialize(writer, user);
        }
    }
}
