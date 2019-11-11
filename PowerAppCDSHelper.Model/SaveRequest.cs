using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerAppCDSHelper.Model
{
    public class SaveRequest
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "environments")]
        public List<Environment> Environments { get; set; }

        [JsonProperty(PropertyName = "targetTypeList")]
        public List<string> TargetTypeList { get; set; }

        [JsonProperty(PropertyName = "entityMappingTasks")]
        public List<EntityMappingTask> EntityMappingTasks { get; set; }

        [JsonProperty(PropertyName = "validationIssues")]
        public List<ValidationIssue> ValidationIssues { get; set; }

        [JsonProperty(PropertyName = "projectState")]
        public string ProjectState { get; set; }

        [JsonProperty(PropertyName = "isPQOnlineFlow")]
        public bool IsPQOnlineFlow { get; set; }

        [JsonProperty(PropertyName = "maxCrmIo")]
        public int MaxCrmIo { get; set; }

        [JsonProperty(PropertyName = "useCrmODataExport")]
        public bool UseCrmODataExport { get; set; }

        [JsonProperty(PropertyName = "createImportErrorFile")]
        public bool CreateImportErrorFile { get; set; }

        [JsonProperty(PropertyName = "autoRetryFailedImportRecords")]
        public bool AutoRetryFailedImportRecords { get; set; }

        [JsonProperty(PropertyName = "templateIdentifier")]
        public TemplateIdentifier TemplateIdentifier { get; set; }

        [JsonProperty(PropertyName = "isDataManagementProject")]
        public bool IsDataManagementProject { get; set; }

        [JsonProperty(PropertyName = "dataManagementOperation")]
        public string DataManagementOperation { get; set; }

        [JsonProperty(PropertyName = "tenant")]
        public string Tenant { get; set; }

        [JsonProperty(PropertyName = "isDualWriteProject")]
        public bool IsDualWriteProject { get; set; }

        [JsonProperty(PropertyName = "isDualWriteEnabled")]
        public bool IsDualWriteEnabled { get; set; }

        [JsonProperty(PropertyName = "skipInitialSync")]
        public bool SkipInitialSync { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "owner")]
        public string Owner { get; set; }

        [JsonProperty(PropertyName = "createdDateTime")]
        public string CreatedDateTime { get; set; }
    }
}
