using PriceParser.Http.Url;
using PriceParser.Parsers.HtmlSearcher;
using PriceParser.Utils;

namespace PriceParser.Parsers;

public class Incom73ProductParser : WebPageProductParserBase
{
    public Incom73ProductParser() :
        base(new CssSelectorHtmlSearcher("h1.product-card-info__title", "span.price-current b"), new Incom73UrlCleaner())
    {
    }

    public override bool IsSupportUrl(Uri url) =>
        DomainInaccurateComparator.DefaultInstance.Compare("incom73.ru", url.IdnHost);
}