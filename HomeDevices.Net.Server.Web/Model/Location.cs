using System;
using System.Collections.Generic;

namespace HomeDevices.Net.Server.Web.Model
{
    public class Location
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual IList<Device> Devices { get; set; }
    }
}
