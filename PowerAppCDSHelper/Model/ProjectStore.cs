using Newtonsoft.Json;
using PowerAppCDSHelper.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PowerAppCDSHelper.Model
{
    public class ProjectStore
    {
        Authentication _authentication;

        string _rawSummary;

        public static Action<int, int> ProgressChanged;

        public static Action<string, bool> Log;

        private void _ProjectStore(Authentication auth)
        {
            _authentication = auth;
            Projects = new List<Project>();
            Load();
        }

        private ProjectStore(Authentication auth)
        {
            _ProjectStore(auth);
        }

        public static ProjectStore GetInstance(Authentication auth)
        {
            return new ProjectStore(auth);
        }

        public void OnProgressChanged(int current, int total)
        {
            if (ProgressChanged != null)
                ProgressChanged(current, total);
        }

        public void OnLog(string message, bool red = false)
        {
            if (Log != null)
                Log(message, red);
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
            OnLog("\nStart fetching project summeries.");
            try
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
                OnProgressChanged(1, 1);
                OnLog(string.Format("\nFetch projects succesfully."));
                Init();
            }
            catch (Exception ex)
            {
                OnLog(string.Format("\nError. {0}", ex.Message), true);
            }
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
            var current = 1;
            var total = projects.Count();
            OnLog(string.Format("\nTotal projects are {0}", total));
            foreach (var project in projects)
            {
                OnLog(string.Format("\nFetching project data. {0}", project["projectName"]));

                Projects.Add(new Project(project["projectName"]
                    , _authentication));

                OnLog(string.Format("\nDone {0}.", project["projectName"]));

                OnProgressChanged(current, total);
                ++current;
            }
        }
    }
}
