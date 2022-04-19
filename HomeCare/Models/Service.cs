using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HomeCare.Models
{
    public class Service
    {
        [JsonProperty("serviceId")]
        public Guid ServiceId { get; set; }

        [JsonProperty("customerId")]
        public Guid CustomerId { get; set; }

        [JsonProperty("serviceStatus")]
        public string ServiceStatus { get; set; }

        [JsonProperty("socialService")]
        public SocialService SocialService { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("isServiceLongDuration")]
        public bool IsServiceLongDuration { get; set; }

        [JsonProperty("serviceDescriptionText")]
        public string ServiceDescriptionText { get; set; }

        [JsonProperty("responsibleTeam")]
        public string ResponsibleTeam { get; set; }

        [JsonProperty("template")]
        public string Template { get; set; }

        [JsonProperty("parameters")]
        public List<Parameter> Parameters { get; set; }
    }
}
