using System;
using System.Collections.Generic;

namespace SampleWebApiAspNetCore.Entities
{
    public class WebAppDataEntity
    {
        public string guid { get; set; }
        public DateTime lastModified { get; set; }
        public IDictionary<string, object> userData { get; set; }
               
    }
}
