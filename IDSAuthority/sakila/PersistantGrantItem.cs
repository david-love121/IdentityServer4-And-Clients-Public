using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDSEmpty.sakila
{
    public class PersistantGrantItem
    {
        public string ID { get; set; }
        public string TYPEI { get; set; }
        public string SUBJECT_ID { get; set; }
        public string CLIENT_ID { get; set; }
        public DateTime CREATION_TIME { get; set; }
        public DateTime? EXPIRATION { get; set; }
        public string DATAI { get; set; }
    }
}
