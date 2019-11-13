using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PowerAppCDSHelper.Model
{
    public class Field
    {
        [JsonProperty(PropertyName = Constants.ID_REPLACE_VALUE)]
        public string  Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "isRequired")]
        public bool IsRequired { get; set; }

        [JsonProperty(PropertyName = "isRetrievable")]
        public bool IsRetrievable { get; set; }

        [JsonProperty(PropertyName = "isReadonly")]
        public bool IsReadOnly { get; set; }

        [JsonProperty(PropertyName = "typeDetails")]
        public TypeDetails TypeDetails { get; set; }
    }

    public class TypeDetails
    {
        [JsonProperty(PropertyName = Constants.TYPE_REPLACE_STRING, TypeNameHandling = TypeNameHandling.All)]
        public string sType { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "length")]
        public int Length { get; set; }

        [JsonProperty(PropertyName = "isMultiLine")]
        public bool IsMultiLine { get; set; }
    }
}
