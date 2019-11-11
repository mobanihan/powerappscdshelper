using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerAppCDSHelper.Model
{
    public class Environment
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "connectionSetName")]
        public string ConnectionSetName { get; set; }

        [JsonProperty(PropertyName = "targetType")]
        public string TargetType { get; set; }

        //TODO: check the type of the shared enums variable 
        //[JsonProperty(PropertyName = "sharedEnums")]
        //public List<string> SharedEnums { get; set; }

        [JsonProperty(PropertyName = "schemas")]
        public List<Schema> Schemas { get; set; }

        [JsonProperty(PropertyName = "connectionDisplayName")]
        public string ConnectionDisplayName { get; set; }

        [JsonProperty(PropertyName = "environmentDisplayName")]
        public string EnvironmentDisplayName { get; set; }

        [JsonProperty(PropertyName = "metadataUrl")]
        public string MetadataUrl { get; set; }

        [JsonProperty(PropertyName = "environmentInfo")]
        public string EnvironmentInfo { get; set; }

        [JsonProperty(PropertyName = "powerAppsEnvironment")]
        public string PowerAppsEnvironment { get; set; }

        [JsonProperty(PropertyName = "needIntegrationKey")]
        public bool NeedIntegrationKey { get; set; }

        [JsonProperty(PropertyName = "excludeHardCodedIntegrationKeys")]
        public bool ExcludeHardCodedIntegrationKeys { get; set; }
    }
}
