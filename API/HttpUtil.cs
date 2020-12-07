using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestRailHelperStarShip.Enums;

namespace TestRailHelperStarShip.API
{
    public class HttpUtil
    {
        public static HttpWebRequest CreateHttpGetRequest(string uri, string data, string contentType)
        {
            var uriWithData = UriUtil.CombineUrl(uri, data);
            var httpRequest = WebRequest.CreateHttp(uriWithData);
            httpRequest.ContentType = contentType;
            httpRequest.Method = "GET";
            return httpRequest;
        }
        public static HttpWebRequest CreateHttpGetRequest(string uri, string data, string contentType, string login, string password)
        {
            var request = CreateHttpGetRequest(uri, data, contentType);
            var basicAuthStr = StringUtil.MakeBasicAuthBase64String(login, password);
            request.Headers.Add("Authorization", "Basic " + basicAuthStr);
            return request;
        }
        public static HttpWebRequest CreateHttpPostRequest(string uri, string data, string contentType)
        {
            var request = WebRequest.CreateHttp(uri);
            request.ContentType = contentType;
            request.Method = "POST";
            if (data != null)
            {
                var byteArray = Encoding.UTF8.GetBytes(data);
                request.ContentLength = byteArray.Length;
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
                return request;
            }
            return request;
        }
        public static HttpWebRequest CreateHttpPostRequest(string uri, string data, string contentType, string login, string password)
        {
            var request = CreateHttpPostRequest(uri, data, contentType);
            var basicAuthStr = StringUtil.MakeBasicAuthBase64String(login, password);
            request.Headers.Add("Authorization", "Basic " + basicAuthStr);
            return request;
        }
        private static HttpWebRequest CreateHttpRequest(RequestMethod method, string uri, string data, string contentType)
        {
            HttpWebRequest request = null;
            switch (method)
            {
                case RequestMethod.Get:
                    request = CreateHttpGetRequest(uri, data, contentType); break;
                case RequestMethod.Post:
                    request = CreateHttpPostRequest(uri, data, contentType); break;
            }
            return request;
        }
        public static string MakePostDataStr(Dictionary<string, object> parametersDictionary)
        {
            var sb = new StringBuilder();
            foreach (var keyVal in parametersDictionary)
            {
                sb.Append($"{keyVal.Key}={keyVal.Value}");
            }
            return sb.ToString();
        }
        public static async Task<HttpResponseResult> GetApiDataAsync(RequestMethod method, string uri, string data, string contentType)
        {
            HttpWebRequest request = CreateHttpRequest(method, uri, data, contentType);
            var httpResponseData = string.Empty;
            HttpStatusCode statusCode = HttpStatusCode.Continue; //Status code 100
            try
            {
                using (var response = await request.GetResponseAsync())
                {
                    var httpResponse = response as HttpWebResponse;
                    statusCode = httpResponse.StatusCode;
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            httpResponseData = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                var httpResponse = (HttpWebResponse)ex.Response;
                statusCode = httpResponse.StatusCode;
            }
            var responseResult = new HttpResponseResult(statusCode, httpResponseData);
            return responseResult;
        }
        public static async Task<HttpResponseResult> GetApiDataAsync(HttpWebRequest request)
        {
            var httpResponseData = string.Empty;
            HttpStatusCode statusCode = HttpStatusCode.Continue; //Status code 100
            WebResponse response = null;
            try
            {
                using (response = await request.GetResponseAsync())
                {
                    var httpResponse = response as HttpWebResponse;
                    statusCode = httpResponse.StatusCode;
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            httpResponseData = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                var httpResponse = (HttpWebResponse)ex.Response;
                statusCode = httpResponse.StatusCode;
            }
            var responseResult = new HttpResponseResult(statusCode, httpResponseData);
            return responseResult;
        }
    }
}
