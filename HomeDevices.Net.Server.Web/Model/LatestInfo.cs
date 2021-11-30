using System.Collections.Generic;

namespace HomeDevices.Net.Server.Web.Model
{
    public class LatestInfo
    {
        public RuuviTag Value { get; set; }
        public List<Link> Links { get; set; }
    }
}
