using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dihweb.Models
{
    public class Attribute
    {
        public string object_id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string expression { get; set; }
    }

    public class Command
    {
        public string object_id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Device
    {
        public string device_id { get; set; }
        public string service { get; set; }
        public string service_path { get; set; }
        public string entity_name { get; set; }
        public string entity_type { get; set; }
        public string transport { get; set; }
        public List<Attribute> attributes { get; set; }
        public List<object> lazy { get; set; }
        public List<Command> commands { get; set; }
        public List<StaticAttribute> static_attributes { get; set; }
        public string expressionLanguage { get; set; }
        public bool explicitAttrs { get; set; }
        public string endpoint { get; set; }
        public string protocol { get; set; }
    }

    public class IOTDevicesModel
    {
        public int count { get; set; }
        public List<Device> devices { get; set; }
    }

    public class StaticAttribute
    {
        public string name { get; set; }
        public string type { get; set; }
        public string value { get; set; }
    }
}
