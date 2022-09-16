using StepProject.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace StepProject.Utils.Writers
{
    internal class UserJsonWriter
    {
        private string fileName;

        public UserJsonWriter(string fileName)
        {
            this.fileName = fileName;
        }

        public void Write(User user)
        {
            using (TextWriter fs = new StreamWriter(fileName))
            {
                JsonSerializerOptions options = new() 
                { 
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                };
                string jsonString = JsonSerializer.Serialize(user, options);
                fs.Write(jsonString);
            }

        }
    }
}
