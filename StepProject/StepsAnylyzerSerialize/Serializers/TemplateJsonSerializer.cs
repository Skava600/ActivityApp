using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace StepsAnylyzerSerialize.Serializers
{
    public class TemplateJsonSerializer<T> : ITemplateSerializer<T> where T : class
    {
        public void Write(IEnumerable<T> array, string filename)
        {
            JsonSerializerOptions options = new()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            };

            File.WriteAllText(filename, JsonSerializer.Serialize(array, options));
        }
    }
}
