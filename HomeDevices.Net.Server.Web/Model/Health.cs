namespace HomeDevices.Net.Server.Web.Model
{

    //{
    //  "status": "Healthy",
    //  "results": {
    //    "ruuvi_hc": {
    //      "status": "Healthy",
    //      "description": "A healthy result.",
    //      "data": {
    //        "Status": "Running"
    //      }
    //    }
    //  }
    //}
    public class Health
    {
        public string status { get; set; }
        public Results results { get; set; }
    }

    public class Results
    {
        public Ruuvi_Hc ruuvi_hc { get; set; }
    }

    public class Ruuvi_Hc
    {
        public string status { get; set; }
        public string description { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string Status { get; set; }
    }
}
