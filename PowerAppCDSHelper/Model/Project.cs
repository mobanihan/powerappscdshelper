using Newtonsoft.Json;
using PowerAppCDSHelper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace PowerAppCDSHelper.Model
{
    public class Project
    {
        private string _rawProjectOrig;

        private dynamic _rawProject;

        private Authentication _authentication;

        public Dictionary<string, dynamic> Tasks { get; private set; }

        public string ProjectName { get; }

        public string DisplayName { get; private set; }

        public bool IsInit { get; private set; }

        public string[] Environments { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="auth"></param>
        public Project(string projectName, Authentication auth)
        {
            if (string.IsNullOrEmpty(projectName) || auth == null)
                throw new ArgumentNullException("projectName or auth can't be null or empty");
            this.ProjectName = projectName;
            this._authentication = auth;
            Load();
        }

        /// <summary>
        ///  Add new task to the existed one.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="task"></param>
        /// <param name="environments"></param>
        public void AppendTasks(string name, dynamic task, string[] environments)
        {
            if (!IsValidEnvironment(environments))
                throw new InvalidOperationException("Combining projects should be occur within the same environments");

            if (Tasks.ContainsKey(name))
                Tasks[name] = task;
            else
                Tasks.Add(name, task);
        }

        public void Update()
        {
            Order();
            var result = WebCallDelegate.CallApi(_authentication,
                new Func<HttpClient, HttpResponseMessage>((client) =>
                {
                    var content = new StringContent(JsonConvert.SerializeObject(_rawProject), Encoding.UTF8, "application/json");
                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(SharedConstants.API_BASE_URL + SharedConstants.PROJECT_DETAILS + ProjectName),
                        Method = HttpMethod.Put,
                        Content = content
                    };
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authentication.Token);

                    var response = AsyncUtil.RunSync<HttpResponseMessage>(() => client.SendAsync(request));
                    return response;
                }));
        }

        public void Reset()
        {
            _rawProject = JsonConvert.DeserializeObject(_rawProjectOrig);
            InitFromRawProject();
        }

        private void Order()
        {
            var tasks = new List<dynamic>();
            var order = 0;
            foreach (var item in Tasks)
            {
                item.Value["order"] = order;
                tasks.Add(item.Value);
                ++order;
            }
            _rawProject["entityMappingTasks"] = Newtonsoft.Json.Linq.JToken.FromObject(tasks);
        }

        /// <summary>
        /// Retreive task by name
        /// </summary>
        /// <param name="name">Task Name</param>
        /// <returns></returns>
        public object this[string name]
        {
            get
            {
                return Tasks[name];
            }
            set
            {
                Tasks[name] = value;
            }
        }

        /// <summary>
        /// Fetch project from web api calls and put the result into rawProject dynamic object.
        /// </summary>
        private void Load()
        {
            string rawProjects = WebCallDelegate.CallApi(_authentication, new Func<HttpClient, HttpResponseMessage>((client) =>
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(SharedConstants.API_BASE_URL + SharedConstants.PROJECT_DETAILS + ProjectName),
                    Method = HttpMethod.Get,
                };
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authentication.Token);
                var response = AsyncUtil.RunSync<HttpResponseMessage>(() => client.SendAsync(request));
                return response;
            }));

            _rawProjectOrig = rawProjects;
            _rawProject = JsonConvert.DeserializeObject(rawProjects);

            InitFromRawProject();
        }

        /// <summary>
        ///  Initialize project values from rawProject dynamic object.
        /// </summary>
        private void InitFromRawProject()
        {
            if (_rawProject == null)
                return;

            DisplayName = _rawProject.displayName;

            if (_rawProject["entityMappingTasks"] == null)
                return;

            Tasks = new Dictionary<string, dynamic>();
            var tasks = JsonConvert.DeserializeObject
                <List<Dictionary<string, dynamic>>>(_rawProject["entityMappingTasks"].ToString());
            foreach (var task in tasks)
            {
                Tasks.Add(task["name"], task);
            }

            var environmentsList = new List<string>();
            var environments = JsonConvert.DeserializeObject
                <List<Dictionary<string, dynamic>>>(_rawProject["environments"].ToString());
            foreach (var env in environments)
                environmentsList.Add(env["name"]);

            Environments = environmentsList.ToArray();
            IsInit = true;
        }

        /// <summary>
        ///  Validate environments when combining projects.
        /// </summary>
        /// <param name="environments"></param>
        /// <returns></returns>
        private bool IsValidEnvironment(string[] environments)
        {
            if (environments.Length != Environments.Length)
                return false;

            environments = environments.OrderBy(i => i).ToArray();
            Environments = Environments.OrderBy(i => i).ToArray();

            var length = environments.Length;
            for (var i = 0; i < length; i++)
            {
                if (environments[i] != Environments[i])
                    return false;
            }
            return true;
        }

    }
}