using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StepsAnylyzerSerialize.Serializers
{
    public class TemplateXmlSerializer<T> : ITemplateSerializer<T> where T : class
    {
        public void Write(IEnumerable<T> array, string filename)
        {            
            using (Stream fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                serializer.Serialize(fs, new List<T>(array));
            }
        }
    }
}
