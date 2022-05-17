using PriceParser.Parsers.HtmlSearcher;
using PriceParser.Utils;

namespace PriceParser.Parsers;

internal class Ig73ProductParser : WebPageProductParserBase
{
    public Ig73ProductParser() : base(new CssSelectorHtmlSearcher("h1.e-product-info__title", "span.e-product-prices__general"))
    {
    }

    public override bool IsSupportUrl(Uri url) =>
        DomainInaccurateComparator.DefaultInstance.Compare("ig73.ru", url.IdnHost);
}