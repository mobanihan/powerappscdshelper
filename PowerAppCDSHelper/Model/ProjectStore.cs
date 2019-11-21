using Newtonsoft.Json;
using PowerAppCDSHelper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PowerAppCDSHelper.Model
{
    public class ProjectStore
    {
        Authentication _authentication;

        string _rawSummary;

        private ProjectStore(Authentication auth)
        {
            _authentication = auth;
            Projects = new List<Project>();
            Load();
        }

        public static ProjectStore GetInstance(Authentication auth)
        {
            return new ProjectStore(auth);
        }

        public List<Project> Projects { get; private set; }

        public Project this[string name]
        {
            get
            {
                return Projects.Where(f => f.ProjectName.Equals(name))
                    .FirstOrDefault();
            }
        }


        /// <summary>
        ///  Load project summeries from web api.
        /// </summary>
        private void Load()
        {
            _rawSummary = WebCallDelegate.CallApi(_authentication, new Func<HttpClient, HttpResponseMessage>((client) =>
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(SharedConstants.API_BASE_URL + SharedConstants.PROJECT_SUMMERIES),
                    Method = HttpMethod.Get,
                };
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authentication.Token);

                var response = AsyncUtil.RunSync<HttpResponseMessage>(() => client.SendAsync(request));
                return response;
            }));
            Init();
        }

        /// <summary>
        ///  Manipulate 
        /// </summary>
        private void Init()
        {
            if (_rawSummary == null)
                return;

            var projects = JsonConvert
                .DeserializeObject<List<Dictionary<string, dynamic>>>(_rawSummary);

            //TODO: remove limitation 
            var projNum = 0;
            foreach (var project in projects)
            {
                if (++projNum > 4)
                    break;
                Projects.Add(new Project(project["projectName"]
                    , _authentication));
            }
        }
    }
}
