using Newtonsoft.Json;

namespace PowerAppCDSHelper.Model
{
    public class TemplateIdentifier
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "version")]
        public Version Version { get; set; }

        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }
    public class Version
    {
        [JsonProperty(PropertyName = "major")]
        public int Major { get; set; }

        [JsonProperty(PropertyName = "minor")]
        public int Minor { get; set; }

        [JsonProperty(PropertyName = "build")]
        public int Build { get; set; }

        [JsonProperty(PropertyName = "revision")]
        public int Revision { get; set; }

        [JsonProperty(PropertyName = "majorRevision")]
        public int MajorRevision { get; set; }

        [JsonProperty(PropertyName = "minorRevision")]
        public int MinorRevision { get; set; }
    }
}
