using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerAppCDSHelper.Utils
{
    public class SharedConstants
    {
        public const string API_BASE_URL = "https://integ-atm-prod-eu.rsu.powerapps.com";
        public const string PROJECT_SUMMERIES = "/IntegratorApp/ProjectManagementService/api/ProjectSummary";
        public const string PROJECT_DETAILS = "/IntegratorApp/ProjectManagementService/api/Project/";

        public const int MAX_RETRY_COUNT = 3;
    }
}
