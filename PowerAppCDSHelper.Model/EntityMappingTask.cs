using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerAppCDSHelper.Model
{
    public class EntityMappingTask
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "order")]
        public int Order { get; set; }

        [JsonProperty(PropertyName = "connectionSetName")]
        public string ConnectionSetName { get; set; }

        [JsonProperty(PropertyName = "leftEnvironmentType")]
        public string LeftEnvironmentType { get; set; }

        [JsonProperty(PropertyName = "centerEnvironmentType")]
        public string CenterEnvironmentType { get; set; }

        [JsonProperty(PropertyName = "rightEnvironmentType")]
        public string RightEnvironmentType { get; set; }

        [JsonProperty(PropertyName = "leftPartitionName")]
        public string LeftPartitionName { get; set; }

        [JsonProperty(PropertyName = "centerPartitionName")]
        public string CenterPartitionName { get; set; }

        [JsonProperty(PropertyName = "rightPartitionName")]
        public string RightPartitionName { get; set; }

        [JsonProperty(PropertyName = "legs")]
        public List<Leg> Legs { get; set; }
    }

    public class Leg
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "sourceEnvironment")]
        public string SourceEnvironment { get; set; }

        [JsonProperty(PropertyName = "sourceSchema")]
        public string SourceSchema { get; set; }

        [JsonProperty(PropertyName = "sourceEnvironmentType")]
        public string SourceEnvironmentType { get; set; }

        [JsonProperty(PropertyName = "sourceFilter")]
        public string SourceFilter { get; set; }

        [JsonProperty(PropertyName = "isSourceFilterEditable")]
        public bool IsSourceFilterEditable { get; set; }

        [JsonProperty(PropertyName = "destinationEnvironment")]
        public string DestinationEnvironment { get; set; }

        [JsonProperty(PropertyName = "destinationSchema")]
        public string DestinationSchema { get; set; }

        [JsonProperty(PropertyName = "destinationEnvironmentType")]
        public string DestinationEnvironmentType { get; set; }

        [JsonProperty(PropertyName = "reversedSourceFilter")]
        public string ReversedSourceFilter { get; set; }

        [JsonProperty(PropertyName = "fieldMappings")]
        public List<FieldMapping> FieldMappings { get; set; }

        [JsonProperty(PropertyName = "sourceMQuery")]
        public string SourceMQuery { get; set; }

        [JsonProperty(PropertyName = "mashup")]
        public Mashup Mashup { get; set; }

        [JsonProperty(PropertyName = "sourceEntitySchema")]
        public EntitySchema SourceEntitySchema { get; set; }

        [JsonProperty(PropertyName = "sourceProjectName")]
        public string SourceProjectName { get; set; }

        [JsonProperty(PropertyName = "entityFileFormat")]
        public string EntityFileFormat { get; set; }

        [JsonProperty(PropertyName = "deleteNonMatchingData")]
        public bool DeleteNonMatchingData { get; set; }
    }

    public class Mashup
    {
        public string Document { get; set; }

        public string Query { get; set; }
    }

    public class FieldMapping
    {
        [JsonProperty(PropertyName = "syncDirection")]
        public string SyncDirection { get; set; }

        [JsonProperty(PropertyName = "sourceField")]
        public string SourceField { get; set; }

        [JsonProperty(PropertyName = "destinationField")]
        public string DestinationField { get; set; }

        [JsonProperty(PropertyName = "isSystemGenerated")]
        public bool IsSystemGenerated { get; set; }
    }
}
