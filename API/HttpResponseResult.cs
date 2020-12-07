using System;
using System.Net;

namespace TestRailHelperStarShip.API
{
    public class HttpResponseResult
    {
        public HttpResponseResult(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
        public HttpResponseResult(HttpStatusCode statusCode, string content) : this(statusCode)
        {
            Content = content;
        }
        public string Content { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public T GetApiData<T>(Func<string, T> dataProviderFunc)
        {
            return dataProviderFunc.Invoke(Content);
        }
    }
}
