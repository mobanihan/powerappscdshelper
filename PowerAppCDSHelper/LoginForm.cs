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
    public partial class LoginForm : Form
    {
        string login_url = "https://admin.powerplatform.microsoft.com/account/login";
        string token_url = "https://admin.powerplatform.microsoft.com/account/token";
        string support_url = "https://admin.powerplatform.microsoft.com/support";
        public static string AccessToken;

        public LoginForm()
        {
            InitializeComponent();

            webBrowser1.Navigate(login_url);
            webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.Authority.Equals(new Uri(support_url).Authority))
            {
                //redirect finished and the cookies are ready to be fetched. 
                var cookies = FullWebBrowserCookie.GetCookieInternal(webBrowser1.Url, false);
                WebClient wc = new WebClient();
                wc.Headers.Add("Cookie: " + cookies);
                wc.Headers.Add("Content-Type", "application/json");
                wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; Touch; .NET4.0E; .NET4.0C; .NET CLR 3.5.30729; .NET CLR 2.0.50727; .NET CLR 3.0.30729; Tablet PC 2.0; rv:11.0) like Gecko");
                byte[] result = wc.UploadData(token_url, "POST", System.Text.Encoding.UTF8.GetBytes("{\"resource\":\"https://IntegratorApp.com\",\"origin\":\"DataIntegration\"}"));

                dynamic obj = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(result));
                LoginForm.AccessToken = obj.accessToken.ToString();
                this.Dispose();
            }
        }
      
    }
}
