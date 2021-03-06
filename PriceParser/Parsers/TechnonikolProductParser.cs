using System.Net;
using PriceParser.DomainComparator;
using PriceParser.Parsers.HtmlSearcher;
using PriceParser.Parsers.Models;
using PriceParser.Utils;

namespace PriceParser.Parsers;

public class TechnonikolProductParser : WebPageProductParserBase
{
    private readonly TechnonikolShop _shop;

    public TechnonikolProductParser(TechnonikolShop shop) : base(new TechnonikolHtmlSearcher())
    {
        _shop = shop;
    }

    public override Task Initialize()
    {
        Loader.AddCookie(new Cookie("current_city_code", _shop.CityId, null, ".shop.tn.ru"));
        Loader.AddCookie(new Cookie("current_city_name", _shop.CityName, null, ".shop.tn.ru"));
        Loader.AddCookie(new Cookie("delivery_center_id", _shop.DeliveryCenterId.ToString(), null, ".shop.tn.ru"));
        return Task.CompletedTask;
    }

    public override bool IsSupportUrl(Uri url) =>
        InaccurateDomainComparator.DefaultInstance.Compare("shop.tn.ru", url.IdnHost);
}