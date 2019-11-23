using System;

namespace PowerAppCDSHelper.Model
{
    public class Authentication
    {
        public static Func<string> GetToken;

        public string OnGetToken()
        {
            if (GetToken != null)
                return GetToken();
            throw new InvalidOperationException("Can't generate token");
        }

        public string Token { get; private set; }

        public Authentication()
        {
            Token = OnGetToken();
        }

        public Authentication(string token)
        {
            Token = token;
        }

        public string NewToken()
        {
            Token = OnGetToken();
            return Token;
        }
    }
}
