using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PowerAppCDSHelper.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using PowerAppCDSHelper.Utils;

namespace PowerAppCDSHelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string getToken()
        {
            return txt_pass.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var srcSelected = lst_projectSource.SelectedItem.ToString();
                var destSelected = lst_projectDest.SelectedItem.ToString();

                if (string.IsNullOrEmpty(srcSelected) || string.IsNullOrEmpty(destSelected))
                    return;

                if (!srcSelected.ToLower().Contains("mbh test") || !destSelected.ToLower().Contains("mbh test"))
                {
                    MessageBox.Show("should select test project", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var srcString = WebUtils.ListProjectTasks(getToken(), srcSelected);
                var destString = WebUtils.ListProjectTasks(getToken(), destSelected);

                dynamic src = JsonConvert.DeserializeObject(srcString);
                dynamic dest = JsonConvert.DeserializeObject(destString);

                var sourceMappingTasks = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(src.entityMappingTasks.ToString());
                var destMappingTasks = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(dest.entityMappingTasks.ToString());

                if (!compareDictionaries(listOfPartitions(sourceMappingTasks), listOfPartitions(destMappingTasks)))
                {
                    MessageBox.Show("Source and Destination has different attributes and can't be mapped together, please check environments, environment types and partitions"
                        , "Mapping Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var orderPointer = endOfOrder(destMappingTasks) + 1;

                //var mappingTasks = JsonConvert.DeserializeObject<List<Dictionary<string, object
                //>>>(src.entityMappingTasks.ToString());
                foreach (var item in sourceMappingTasks)
                {
                    item["order"] = orderPointer;
                    destMappingTasks.Add(item);
                    //mappingTasks.Add(item.Key, item.Value);
                    ++orderPointer;
                }


                dest.entityMappingTasks = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(destMappingTasks));


                var result = WebUtils.PutProject(getToken(), destSelected, JsonConvert.SerializeObject(dest));

                MessageBox.Show("Done", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //OpenFileDialog openFileDialog = new OpenFileDialog();
                //var result = openFileDialog.ShowDialog();
                //if (result == DialogResult.Cancel)
                //    return;

                //using (var fs = File.Open(openFileDialog.FileName, FileMode.Open))
                //{
                //    var reader = new StreamReader(fs);
                //    var content = reader.ReadToEnd();
                //    content = content.Replace("$type", Constants.TYPE_REPLACE_STRING);
                //    content = content.Replace("$id", Constants.ID_REPLACE_VALUE);

                //    //var dd = JToken.Parse(content).Cast<SaveRequest>();
                //    //dynamic values = JsonConvert.DeserializeObject(content);

                //    //SaveRequest sr = (SaveRequest)values;

                //    var theEnd = JsonConvert.DeserializeObject<SaveRequest>(content, new JsonSerializerSettings()
                //    {

                //    });

                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            var result = WebUtils.ListProjects(getToken());

            List<Project> projects = JsonConvert.DeserializeObject<List<Project>>(result);

            fillProjectList(projects);
        }

        private void fillProjectList(List<Project> projects)
        {
            lst_projectSource.Items.Clear();
            lst_projectDest.Items.Clear();

            foreach (var item in projects)
            {
                lst_projectSource.Items.Add(item.projectName);
                lst_projectDest.Items.Add(item.projectName);
            }
        }

        private int endOfOrder(List<Dictionary<string, dynamic>> mappingTasks)
        {
            var idx = 0;
            foreach (var item in mappingTasks)
            {
                if (Convert.ToInt32(item["order"]) > idx)
                    idx = Convert.ToInt32(item["order"]);
            }
            return idx;
        }

        private Dictionary<string, string> listOfPartitions(List<Dictionary<string, dynamic>> mappingTasks)
        {
            // get the first value 
            var result = new Dictionary<string, string>();
            if (mappingTasks.Count() > 0)
            {
                var value = mappingTasks[0];
                result.Add("leftEnvironmentType", value.ContainsKey("leftEnvironmentType") ? value["leftEnvironmentType"] : "");
                result.Add("centerEnvironmentType", value.ContainsKey("centerEnvironmentType") ? value["centerEnvironmentType"] : "");
                result.Add("rightEnvironmentType", value.ContainsKey("rightEnvironmentType") ? value["rightEnvironmentType"] : "");

                result.Add("leftEnvironment", value.ContainsKey("leftEnvironment") ? value["leftEnvironment"] : "");
                result.Add("centerEnvironment", value.ContainsKey("centerEnvironment") ? value["centerEnvironment"] : "");
                result.Add("rightEnvironment", value.ContainsKey("rightEnvironment")?value["rightEnvironment"]:"");

                result.Add("leftPartitionName", value.ContainsKey("leftPartitionName") ? value["leftPartitionName"] : "");
                result.Add("centerPartitionName", value.ContainsKey("centerPartitionName") ? value["centerPartitionName"] : "");
                result.Add("rightPartitionName", value.ContainsKey("rightPartitionName") ? value["rightPartitionName"] : "");
            }
            return result;
        }

        private bool compareDictionaries(Dictionary<string, string> left, Dictionary<string, string> right)
        {
            foreach (var item in left)
            {
                if (item.Value != right[item.Key])
                    return false;
            }
            return true;
        }

    }
}
