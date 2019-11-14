using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerAppCDSHelper.Model
{
    public class Project
    {
        [JsonProperty("projectName")]
        public string projectName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("projectDisplayName")]
        public string ProjectDisplayName { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
    }
}
