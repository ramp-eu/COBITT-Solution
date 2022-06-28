using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dihweb.Models
{
    public class Condition
    {
        public List<object> attrs { get; set; }
    }

    public class Entity
    {
        public string id { get; set; }
        public string type { get; set; }
        public string idPattern { get; set; }
    }

    public class Http
    {
        public string url { get; set; }
    }

    public class Notification
    {
        public List<object> attrs { get; set; }
        public bool onlyChangedAttrs { get; set; }
        public string attrsFormat { get; set; }
        public Http http { get; set; }
        public int? timesSent { get; set; }
        public DateTime? lastNotification { get; set; }
        public DateTime? lastSuccess { get; set; }
        public int? lastSuccessCode { get; set; }
    }

    public class SubscriptionsModel
    {
        public string id { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public Subject subject { get; set; }
        public Notification notification { get; set; }
        public int throttling { get; set; }
    }

    public class Subject
    {
        public List<Entity> entities { get; set; }
        public Condition condition { get; set; }
    }
}
