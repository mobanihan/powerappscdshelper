using Microsoft.Win32;
using Newtonsoft.Json;
using PowerAppCDSHelper.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerAppCDSHelper
{
    public partial class Form2 : Form
    {
        string login_url = "https://admin.powerplatform.microsoft.com/account/login";
        string token_url = "https://admin.powerplatform.microsoft.com/account/token";
        string support_url = "https://admin.powerplatform.microsoft.com/support";
        public static string AccessToken;

        public Form2()
        {
            InitializeComponent();

            var appName = Process.GetCurrentProcess().ProcessName + ".exe";
            SetIE8KeyforWebBrowserControl(appName);

            webBrowser1.Navigate("javascript:void((function(){var a,b,c,e,f;f=0;a=document.cookie.split('; ');for(e=0;e<a.length&&a[e];e++){f++;for(b='.'+location.host;b;b=b.replace(/^(?:%5C.|[^%5C.]+)/,'')){for(c=location.pathname;c;c=c.replace(/.$/,'')){document.cookie=(a[e]+'; domain='+b+'; path='+c+'; expires='+new Date((new Date()).getTime()-1e11).toGMTString());}}}})())");

            webBrowser1.Navigate(login_url);
            webBrowser1.Navigated += WebBrowser1_Navigated;
        }

        private void WebBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (e.Url.ToString().ToLower().Contains(support_url))
            {
                try
                {
                    var cookies = FullWebBrowserCookie.GetCookieInternal(webBrowser1.Url, false);
                    WebClient wc = new WebClient();
                    wc.Headers.Add("Cookie: " + cookies);
                    wc.Headers.Add("Content-Type", "application/json");
                    wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; Touch; .NET4.0E; .NET4.0C; .NET CLR 3.5.30729; .NET CLR 2.0.50727; .NET CLR 3.0.30729; Tablet PC 2.0; rv:11.0) like Gecko");
                    byte[] result = wc.UploadData(token_url, "POST", System.Text.Encoding.UTF8.GetBytes("{\"resource\":\"https://IntegratorApp.com\",\"origin\":\"DataIntegration\"}"));

                    dynamic obj = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(result));
                    Form2.AccessToken = obj.accessToken.ToString();
                    this.Dispose();
                }
                catch
                {

                }
            }
        }
        private void SetIE8KeyforWebBrowserControl(string appName)
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
    }
}
