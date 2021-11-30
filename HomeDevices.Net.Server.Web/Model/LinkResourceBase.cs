using System.Collections.Generic;

namespace HomeDevices.Net.Server.Web.Model
{
    public class LinkResourceBase
    {
        public LinkResourceBase()
        {
        }
        public List<Link> Links { get; set; } = new List<Link>();
    }
}
