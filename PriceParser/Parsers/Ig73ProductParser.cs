using PriceParser.Utils;

namespace PriceParser.Sites.Parsers;

internal class Ig73ProductParser : WebPageProductParserBase
{
    public Ig73ProductParser() : base(new CssSelectorHtmlSearcher("h1.e-product-info__title", "span.e-product-prices__general"))
    {
    }

    public override bool IsSupportUrl(Uri url)
    {
        return DomainInaccurateComparator.DefaultInstance.Compare("ig73.ru", url.IdnHost);
    }
}