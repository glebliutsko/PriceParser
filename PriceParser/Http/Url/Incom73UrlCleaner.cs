using System.Web;

namespace PriceParser.Http.Url;

public class Incom73UrlCleaner : IUrlCleaner
{
    public Uri CleanUrl(Uri url)
    {
        var query = HttpUtility.ParseQueryString(url.Query);
        query.Remove("search");
        
        var uriBuilder = new UriBuilder(url);
        uriBuilder.Query = query.ToString();
        return uriBuilder.Uri;
    }
}