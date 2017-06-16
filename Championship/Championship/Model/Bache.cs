using System;
namespace Championship.Model
{
    public class Bache
    {
        public Bache()
        {
        }

		[Newtonsoft.Json.JsonProperty("Id")]
		public string Id { get; set; }

		[Microsoft.WindowsAzure.MobileServices.Version]
		public string AzureVersion { get; set; }

		[Newtonsoft.Json.JsonProperty("latitude")]
        public double Latitude { get; set; }

		[Newtonsoft.Json.JsonProperty("longitude")]
		public double Longitude { get; set; }

        [Newtonsoft.Json.JsonProperty("photoUrl")]
        public string PhotoUrl { get; set; }

        public DateTime DateUtc { get; set; }

		[Newtonsoft.Json.JsonIgnore]
		public string DateDisplay { get { return DateUtc.ToLocalTime().ToString("g"); } }
    }
}
