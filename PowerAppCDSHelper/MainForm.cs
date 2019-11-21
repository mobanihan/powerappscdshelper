using Microsoft.Win32;
using PowerAppCDSHelper.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerAppCDSHelper
{
    public partial class MainForm : Form
    {
        ProjectStore store;
        public MainForm()
        {
            InitializeComponent();
            var appName = Process.GetCurrentProcess().ProcessName + ".exe";
            SetIE11KeyforWebBrowserControl(appName);

            backgroundWorker1.RunWorkerAsync();
        }

        public static string NewAccessToken()
        {
            var dialog = new LoginForm().ShowDialog();
            return LoginForm.AccessToken;
        }
 
        public static string GetAccessToken()
        {
            if (string.IsNullOrEmpty(LoginForm.AccessToken))
                return MainForm.NewAccessToken();
            return LoginForm.AccessToken;
        }

        private void btn_combine_Click(object sender, EventArgs e)
        {
            var destinationProject = store[dest.SelectedText]; 
            foreach (TreeNode node in tree.Nodes)
            {
                if (node.Checked)
                {
                    if (node.Parent != null && !node.Parent.Checked)
                    {
                        destinationProject.AppendTasks(node.Name, 
                            store[node.Parent.Name].Tasks[node.Name], store[node.Name].Environments);
                    }
                    else if (node.Parent == null)
                    {
                        foreach (var task in store[node.Name].Tasks)
                        {
                            destinationProject.AppendTasks(task.Key, 
                                task.Value, store[node.Name].Environments);
                        }
                    }
                }
            }
            destinationProject.Update();
        }


        private void Init()
        {
            store = ProjectStore.GetInstance(new Authentication(MainForm.GetAccessToken()));
            foreach (var project in store.Projects)
            {
                var node = tree.Nodes.Add(project.ProjectName, project.DisplayName);
                foreach (var task in project.Tasks)
                {
                    node.Nodes.Add(task.Key);
                }
            }
        }
        #region Sys
        private void SetIE11KeyforWebBrowserControl(string appName)
        {
            RegistryKey Regkey = null;
            try
            {
                // For 64 bit machine
                if (Environment.Is64BitOperatingSystem)
                    Regkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Wow6432Node\\Microsoft\\Internet Explorer\\MAIN\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);
                else  //For 32 bit machine
                    Regkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);

                // If the path is not correct or
                // if the user haven't priviledges to access the registry
                if (Regkey == null)
                {
                    MessageBox.Show("Application Settings Failed - Address Not found");
                    return;
                }

                string FindAppkey = Convert.ToString(Regkey.GetValue(appName));

                // Check if key is already present
                if (FindAppkey == "11000")
                {
                    //MessageBox.Show("Required Application Settings Present");
                    Regkey.Close();
                    return;
                }

                // If a key is not present add the key, Key value 8000 (decimal)
                if (string.IsNullOrEmpty(FindAppkey))
                    Regkey.SetValue(appName, unchecked((int)0x2AF8), RegistryValueKind.DWord);

                // Check for the key after adding
                FindAppkey = Convert.ToString(Regkey.GetValue(appName));

                if (FindAppkey == "11000")
                {

                }
                //MessageBox.Show("Application Settings Applied Successfully");
                else
                    MessageBox.Show("Application Settings Failed, Ref: " + FindAppkey);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Application Settings Failed");
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Close the Registry
                if (Regkey != null)
                    Regkey.Close();
            }
        }
        #endregion

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Init();
        }
    }
}
