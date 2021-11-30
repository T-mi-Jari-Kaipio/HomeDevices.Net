using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HomeDevices.Net.Server.Web.Model
{
    public class LinkCollectionWrapper<T> : LinkResourceBase
    {
        public List<T> Value { get; set; } = new List<T>();
        public LinkCollectionWrapper()
        {
        }
        public LinkCollectionWrapper(List<T> value)
        {
            Value = value;
        }
    }
}
