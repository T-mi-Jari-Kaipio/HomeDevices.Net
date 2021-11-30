namespace HomeDevices.Net.Server.Web.Model
{
    public class ReportState
    {
        public string agentUserId { get; set; }
        public string eventId { get; set; }
        public string requestId { get; set; }
        public Payload payload { get; set; }
    }

    public class Payload
    {
        public Devices devices { get; set; }
    }

    public class Devices
    {
        public Notifications notifications { get; set; }
    }

    public class Notifications
    {
        public string DeviceId { get; set; }
    }
    public class Objectdetection
    {
        public int priority { get; set; }
        public long detectionTimestamp { get; set; }
        public Objects objects { get; set; }
    }

    public class Objects
    {
        public string[] named { get; set; }
        public int unclassified { get; set; }
    }

}
