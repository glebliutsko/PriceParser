using System.Net;
using PriceParser.DomainComparator;
using PriceParser.Http.Url;
using PriceParser.Parsers.HtmlSearcher;
using PriceParser.Parsers.Models;
using PriceParser.Utils;

namespace PriceParser.Parsers;

public class MegastroyProductParser : WebPageProductParserBase
{
    private readonly MegastroyShop _shop;

    public MegastroyProductParser(MegastroyShop shop)
        : base(new CssSelectorHtmlSearcher("h1", "div.price div b"), new MegastroyUrlCleaner())
    {
        _shop = shop;
    }

    public override Task Initialize()
    {
        Loader.AddCookie(new Cookie("city_id", _shop.CityId.ToString(), null, ".megastroy.com"));
        Loader.AddCookie(new Cookie("confirmed_domain", _shop.ConfirmedDomain, null, ".megastroy.com"));
        Loader.AddCookie(new Cookie("is_city_confirmed", "1", null, ".megastroy.com"));
        Loader.AddCookie(new Cookie("market_id", _shop.MarketId.ToString(), null, ".megastroy.com"));
        return Task.CompletedTask;
    }

    public override bool IsSupportUrl(Uri url)
    {
        return new InaccurateDomainComparator(MegastroyUrlCleaner.CitySubdomains.Union(new[] {"www", "m"}).ToArray())
            .Compare("megastroy.com", url.IdnHost);
    }
}