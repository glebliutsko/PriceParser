using PriceParser.Utils;

namespace PriceParser.Sites.Parsers;

internal class SarayProductParser : WebPageProductParserBase
{
    public SarayProductParser() : base(new CssSelectorHtmlSearcher("h1", "span.price_value"))
    {
    }

    public override bool IsSupportUrl(Uri url)
    {
        return DomainInaccurateComparator.DefaultInstance.Compare("saray.ru", url.IdnHost);
    }
}