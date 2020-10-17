using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Tracer
{
    [XmlRoot("root")]
    public class TraceResult
    {
        [XmlElement(ElementName = "thread")]
        public List<Threads> Threads;
    }

    public class TreeBrunch
    {
        [XmlAttribute("time")]
        public long Time;
        [XmlElement(ElementName = "method")]
        public List<Methods> Methods;   
        [NonSerialized]  
        [XmlIgnore]
        public Stopwatch stopwatch;
    }
    public class Threads : TreeBrunch
    {
        [XmlAttribute("id")]
        public int ID;

        public Threads()
        {            
        }

        public Threads(int _ID)
        {
            ID = _ID;
        }        
    }

    public class Methods : TreeBrunch
    {
        [XmlAttribute("name")]
        public string Name;
        [XmlAttribute("class")]
        public string Class;        
    }
}
