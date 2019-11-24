using Microsoft.Win32;
using PowerAppCDSHelper.Model;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PowerAppCDSHelper
{
    public partial class MainForm : Form
    {
        //GetToken
        //Init Store 
        //UpdateProgress.
        ProjectStore store;
        public MainForm()
        {
            InitializeComponent();
            //var appName = Process.GetCurrentProcess().ProcessName + ".exe";
            //SetIE11KeyforWebBrowserControl(appName);

            ProjectStore.ProgressChanged = new Action<int, int>((current, total) =>
            {
                var percent = Math.Ceiling(current / (float)total * 100);
                Invoke(new Action(() =>
                {
                    this.progressBar1.Value = (int)percent;
                }));
            });

            ProjectStore.Log = new Action<string, bool>((message, red) =>
            {
                Invoke(new Action(() =>
                {
                    WriteOutput(message, red);
                }));
            });

            Authentication.GetToken = new Func<string>(() =>
            {
                return NewAccessToken();
            });

            this.txt_output.AppendText("Starting....");
            refresh();
        }

        private void refresh()
        {
            new Thread(() =>
            {
                Init();
            })
                .Start();
        }

        public void WriteOutput(string message, bool red = false)
        {
            this.txt_output.SelectionStart = this.txt_output.TextLength;
            this.txt_output.SelectionColor = red ? Color.Red : Color.Black;
            this.txt_output.AppendText(message);
            this.txt_output.SelectionColor = Color.Black;

            this.txt_output.SelectionStart = this.txt_output.TextLength;
            this.txt_output.ScrollToCaret();
        }

        public string NewAccessToken()
        {
            return Invoke(new Func<string>(() =>
            {
                var dialog = new LoginForm().ShowDialog();
                return LoginForm.AccessToken;
            })).ToString();
        }

        public string GetAccessToken()
        {
            if (string.IsNullOrEmpty(LoginForm.AccessToken))
                return this.NewAccessToken();
            return LoginForm.AccessToken;
        }

        private void btn_combine_Click(object sender, EventArgs e)
        {
            var selectedItem = (dest.SelectedItem as ComboBoxItem);
            if (selectedItem == null)
            {
                MessageBox.Show("Please specify the destination then click combine button.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var result = MessageBox.Show(string.Format("Are you sure you want to proceed moving selected tasks to {0}", selectedItem.Text),
                "Confirmation Box", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
                return;

            var destinationProject = store[selectedItem.Value];
            foreach (TreeNode node in tree.Nodes)
            {
                if (node.Checked)
                {
                    foreach (var task in store[node.Name].Tasks)
                    {
                        try
                        {
                            destinationProject.AppendTasks(task.Key,
                                task.Value, store[node.Name].Environments);
                        }
                        catch (Exception ex)
                        {
                            Invoke(new Action(() =>
                            {
                                this.WriteOutput(string.Format("\nError occur on project {0}! Error Details {1}"
                                    , node.Text, ex.Message), true);
                            }));
                        }
                    }
                }
                else
                {
                    foreach (TreeNode subNode in node.Nodes)
                    {
                        try
                        {

                            if (subNode.Checked)
                            {
                                destinationProject.AppendTasks(subNode.Name,
                                    store[node.Name].Tasks[subNode.Name], store[node.Name].Environments);
                            }
                        }
                        catch (Exception ex)
                        {
                            Invoke(new Action(() =>
                            {
                                this.WriteOutput(string.Format("\nError occur on project {0}! on Task {1}! Error Details {2}",
                                    node.Text, subNode.Text, ex.Message), true);
                            }));
                        }
                    }
                }

            }
            try
            {
                destinationProject.Update();
                this.WriteOutput(string.Format("\nSucceed!, the project {0} was updated succesfully.", destinationProject.DisplayName));
            }
            catch (Exception ex)
            {
                this.WriteOutput(string.Format("\nError updating project, error details: {0}", ex.Message), true);
                destinationProject.Reset();
            }
        }


        private void Init()
        {
            Invoke(new Action(() =>
            {
                this.Enabled = false;
                tree.Nodes.Clear();
                dest.Items.Clear();
            }));
            store = ProjectStore.GetInstance(new Authentication());

            foreach (var project in store.Projects)
            {
                Invoke(new Action(() =>
                {
                    dest.Items.Add(new ComboBoxItem(project.DisplayName, project.ProjectName));
                    var node = tree.Nodes.Add(project.ProjectName, project.DisplayName);
                    foreach (var task in project.Tasks)
                    {
                        node.Nodes.Add(task.Key, task.Key);
                    }
                }));
            }
            Invoke(new Action(() =>
            {
                progressBar1.Value = 100;
                txt_output.AppendText("\nDone.");
                this.Enabled = true;
            }));
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

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.Unknown)
                return;

            validateTreeViewSelection(e.Node);
        }

        private void validateTreeViewSelection(TreeNode eNode)
        {
            if (eNode.Parent == null)
            {
                foreach (TreeNode node in eNode.Nodes)
                {
                    node.Checked = eNode.Checked;
                }
            }
            else
            {
                bool _parentChecked = true;
                foreach (TreeNode node in eNode.Parent.Nodes)
                {
                    if (!node.Checked)
                    {
                        _parentChecked = false;
                        break;
                    }
                }
                eNode.Parent.Checked = _parentChecked;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This application to combine common data service projects tasks together automatically instead of doing this manually.\nAuthor: Mohammad Bani Hani\nEmail: jemo800@gmail.com"
                , "Common Data Service Helper", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var dialogResult = new LoginForm(true).ShowDialog();
            refresh();
        }
    }
}
