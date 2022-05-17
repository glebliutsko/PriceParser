namespace PriceParser.Http.Url;

internal class MegastroyUrlCleaner : IUrlCleaner
{
    public static string[] CitySubdomains =
    {
        "yoshkarola",
        "sterlitamak",
        "saransk",
        "ulyanovsk",
        "kazan",
        "yoshkarola",
        "cheboksary",
        "chelny"
    };

    public Uri CleanUrl(Uri url)
    {
        var domainWithoutCity = RemoteCitySubdomain(url.IdnHost);
        var uriBuilder = new UriBuilder(url);
        uriBuilder.Host = domainWithoutCity;
        return uriBuilder.Uri;
    }

    private static string RemoteCitySubdomain(string domain)
    {
        var domainSplitLevels = domain.Split(".");
        var domainWithoutCity = domainSplitLevels.Where(x => !CitySubdomains.Contains(x)).ToArray();
        return string.Join(".", domainWithoutCity);
    }
}