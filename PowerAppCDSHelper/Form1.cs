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

namespace PowerAppCDSHelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            using (var fs = File.Open(openFileDialog.FileName, FileMode.Open))
            {
                var reader = new StreamReader(fs);
                var content = reader.ReadToEnd();
                content = content.Replace("$type", Constants.TYPE_REPLACE_STRING);
                content = content.Replace("$id", Constants.ID_REPLACE_VALUE);

                //var dd = JToken.Parse(content).Cast<SaveRequest>();
                //dynamic values = JsonConvert.DeserializeObject(content);

                //SaveRequest sr = (SaveRequest)values;

                var theEnd = JsonConvert.DeserializeObject<SaveRequest>(content, new JsonSerializerSettings()
                {

                });

            }
        }
    }
}
