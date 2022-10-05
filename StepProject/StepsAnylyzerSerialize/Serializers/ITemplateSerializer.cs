using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepsAnylyzerSerialize.Serializers
{
    public interface ITemplateSerializer<T> where T : class
    {
        void Write(IEnumerable<T> array, string filename);
    }
}
