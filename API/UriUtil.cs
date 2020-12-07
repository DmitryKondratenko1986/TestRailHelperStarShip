using System;

namespace TestRailHelperStarShip.API
{
    public class UriUtil
    {
        public static string CombineUrl(string host, string additional)
        {
            var uri = new Uri(host);
            return new Uri(uri, additional).AbsoluteUri;
        }
        public static string GetBasicAuthUri(string baseUri, string login, string password)
        {
            var uri = new Uri(baseUri);
            var userInfoStr = $"{login}:{password}@";
            var uriPart = baseUri.Split("://")[1];
            var authUri = $"{uri.Scheme}://{userInfoStr}{uriPart}";
            return authUri;
        }
    }
}
