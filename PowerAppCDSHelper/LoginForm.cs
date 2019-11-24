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
        string signout_url = "https://admin.powerplatform.microsoft.com/signout";
        string microsoft_signout_url = "https://login.microsoftonline.com/common/oauth2/logout";
        public static string AccessToken;

        //using System.Runtime.InteropServices;
        //[DllImport("wininet.dll", SetLastError = true, CharSet = CharSet.Auto)]
        //public static extern bool InternetSetOption(IntPtr hInternet, int dwOption,
        //    IntPtr lpBuffer, int dwBufferLength);

        [DllImport("wininet.dll", SetLastError = true)]

        private static extern long DeleteUrlCacheEntry(string lpszUrlName);
        public LoginForm()
        {
            InitializeComponent();

            
            webBrowser1.Navigate(login_url);
            webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
        }

        public LoginForm(bool signout)
        {
            InitializeComponent();

            if (signout)
            {
                webBrowser1.Navigate(microsoft_signout_url);
                webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted1;
            }

        }

        private void WebBrowser1_DocumentCompleted1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.Authority.Equals(new Uri(microsoft_signout_url).Authority))
            {
                this.Dispose();
            }
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
                try
                {
                    byte[] result = wc.UploadData(token_url, "POST", System.Text.Encoding.UTF8.GetBytes("{\"resource\":\"https://IntegratorApp.com\",\"origin\":\"DataIntegration\"}"));


                    dynamic obj = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(result));
                    LoginForm.AccessToken = obj.accessToken.ToString();


                    var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().ReadJwtToken(LoginForm.AccessToken);
                    
                    MainForm mfrm = Application.OpenForms["MainForm"] as MainForm;
                    mfrm.WriteOutput(string.Format("\nLogged in user: {0} ({1})", token.Payload["name"], token.Payload["unique_name"]));

                    this.Dispose();
                }
                catch (Exception ex)
                {

                }
            }
        }
      
    }
}
