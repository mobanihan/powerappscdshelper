using System;
using System.Net.Http;

namespace PowerAppCDSHelper.Utils
{
    public class WebCallDelegate
    {
        public static dynamic CallApi(Model.Authentication authentication, Func<HttpClient, HttpResponseMessage> func)
        {
            int retries = 0;
            var client = new HttpClient();
            do
            {
                try
                {
                    var response = func(client);
                    if (!response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            authentication.NewToken();
                            throw new UnauthorizedAccessException("The user is not authorized. 401");
                        }
                        else
                        {
                            throw new HttpRequestException(
                                string.Format("Unable to fetch project details. \nresponse Status code is {0}, ReasonPhrase is {1}"
                                , (int)response.StatusCode, response.ReasonPhrase));
                        }
                    }
                    else
                    {
                        return
                                            AsyncUtil.RunSync<string>(() =>
                                            response.Content.ReadAsStringAsync());
                    }
                }
                catch (Exception)
                {
                    retries++;
                    if (retries >= SharedConstants.MAX_RETRY_COUNT)
                        throw;
                }

            } while (retries < SharedConstants.MAX_RETRY_COUNT);
            return null;
        }
    }
}
