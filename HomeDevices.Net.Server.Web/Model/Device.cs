using System;

namespace HomeDevices.Net.Server.Web.Model
{
    public class Device
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Description { get; set; }
        public string MacAddress { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string LocationId { get; set; }
        public virtual Location Location { get; set; }
    }
}
