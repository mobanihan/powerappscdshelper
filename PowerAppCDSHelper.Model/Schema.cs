using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerAppCDSHelper.Model
{
    public class Schema
    {
        [JsonProperty(PropertyName = "$id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "targetType")]
        public string TargetType { get; set; }

        [JsonProperty(PropertyName = "refreshState")]
        public string RefreshState { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "fields")]
        public List<Field> Fields { get; set; }
    }

    public class EntitySchema : Schema
    {
        //TODO: check key type 
        [JsonProperty(PropertyName = "keys")]
        public List<string> Keys { get; set; }

        [JsonProperty(PropertyName = "tenant")]
        public string Tenant { get; set; }
    }
}
