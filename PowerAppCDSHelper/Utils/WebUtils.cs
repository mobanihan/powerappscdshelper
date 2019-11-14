using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PowerAppCDSHelper.Utils
{
    public class WebUtils
    {
        public const string BASE_URL = "https://integ-atm-prod-eu.rsu.powerapps.com";
        public const string PROJECT_SUMMERIES = "/IntegratorApp/ProjectManagementService/api/ProjectSummary";
        public const string PROJECT_DETAILS = "/IntegratorApp/ProjectManagementService/api/Project/";
        public static string ListProjects(string token)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(BASE_URL + PROJECT_SUMMERIES),
                Method = HttpMethod.Get,
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //client.DefaultRequestHeaders.Authorization = 
            //  new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


            var response = AsyncUtil.RunSync<HttpResponseMessage>(() => client.SendAsync(request));


            if (response.IsSuccessStatusCode)
                return AsyncUtil.RunSync<string>(() => response.Content.ReadAsStringAsync());
            throw new Exception("error code is " + response.StatusCode + " reason phrase " + response.ReasonPhrase);
        }


        public static string ListProjectTasks(string token, string projectName)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(BASE_URL + PROJECT_DETAILS + projectName),
                Method = HttpMethod.Get,
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = AsyncUtil.RunSync<HttpResponseMessage>(() => client.SendAsync(request));
            if (response.IsSuccessStatusCode)
                return AsyncUtil.RunSync<string>(() => response.Content.ReadAsStringAsync());
            throw new Exception("error code is " + response.StatusCode + " reason phrase " + response.ReasonPhrase);
        }
        

        public static string PutProject(string token, string projectName, string project)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);

            var content = new StringContent(project, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(BASE_URL + PROJECT_DETAILS + projectName),
                Method = HttpMethod.Put,
                Content = content
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = AsyncUtil.RunSync<HttpResponseMessage>(() => client.SendAsync(request));
            if (response.IsSuccessStatusCode)
                return AsyncUtil.RunSync<string>(() => response.Content.ReadAsStringAsync());
            throw new Exception("error code is " + response.StatusCode + " reason phrase " + response.ReasonPhrase);
        }
    }
}
