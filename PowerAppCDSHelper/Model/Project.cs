using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerAppCDSHelper.Model
{
    public class Project
    {
        private string _projectName;
        private string _displayName;
        private bool _isInit;
        private object _rawProject;
        private Dictionary<string, object> _tasks;
        private string[] _environments;

        public string ProjectName
        {
            get
            {
                return _projectName;
            }
        }

        public string DisplayName
        {
            get
            {
                return _displayName;
            }
        }

        public bool IsInit
        {
            get
            {
                return _isInit;
            }
        }

        public Dictionary<string, object> Tasks
        {
            get
            {
                return _tasks;
            }
        }

        public Project(string projectName)
        {
            this._projectName = projectName;
            load();
        }


        public void AppendTasks(string name, object task, List<string> environments)
        {
            if (_tasks.ContainsKey(name))
                throw new Exception("Task name already exist. Please edit name so we can proceed.");
            _tasks.Add(name, task);
        }

        public object this[string index]
        {
            get
            {
                return _tasks[index];
            }
            set
            {
                _tasks[index] = value;
            }
        }

        /// <summary>
        /// Initialize project from web api calls 
        /// </summary>
        private void load()
        {
            
        }

        private bool isValidEnvironment(string[] environments)
        {
            if (environments.Length != _environments.Length)
                return false;
            
            environments = environments.OrderBy(i => i).ToArray();
            //environments.Count(f => f.Equals("1"))
            
            environments.Where(rrrrrrr);
            foreach (var env in environments)
            {
                if (!_environments.Contains(env))
                    return false;
            }
            foreach (var env in _environments)
            {
                if (!environments.Contains(env))
                    return false;
            }
            return true;
        }

        private bool rrrrrrr(string arg)
        {
            throw new NotImplementedException();
        }
    }
}
