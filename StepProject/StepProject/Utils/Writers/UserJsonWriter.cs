using StepProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using StepProject.Interfaces;

namespace StepProject.Utils.Writers
{
    internal class UserJsonWriter : IUserSerializer
    {

        public void Write(IEnumerable<User> users, string filename)
        {
            using (TextWriter fs = new StreamWriter(filename))
            {
                JsonSerializerOptions options = new() 
                { 
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                };
                string jsonString = JsonSerializer.Serialize(users, options);
                fs.Write(jsonString);
            }

        }
    }
}
