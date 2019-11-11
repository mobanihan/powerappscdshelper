using Newtonsoft.Json;

namespace PowerAppCDSHelper.Model
{
    public class ValidationIssue
    {
        [JsonProperty(PropertyName = "severity")]
        public string Severity { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "mappingTask")]
        public string MappingTask { get; set; }

        [JsonProperty(PropertyName = "leg")]
        public string Leg { get; set; }

        [JsonProperty(PropertyName = "dataset")]
        public string Dataset { get; set; }

        [JsonProperty(PropertyName = "schema")]
        public string Schema { get; set; }

        [JsonProperty(PropertyName = "field")]
        public string Field { get; set; }
    }


}
