using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerAppCDSHelper.Model
{
    public class Authentication
    {
        public string Token { get; private set; }

        public Authentication()
        {

        }

        public Authentication(string token)
        {
            Token = token;
        }

     
        public string NewToken()
        {
            // do some magic
            return MainForm.GetAccessToken();
        }
    }
}
