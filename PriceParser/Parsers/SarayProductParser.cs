using PriceParser.DomainComparator;
using PriceParser.Parsers.HtmlSearcher;
using PriceParser.Utils;

namespace PriceParser.Parsers;

internal class SarayProductParser : WebPageProductParserBase
{
    public SarayProductParser() : base(new CssSelectorHtmlSearcher("h1", "span.price_value"))
    {
    }

    public override bool IsSupportUrl(Uri url) =>
        InaccurateDomainComparator.DefaultInstance.Compare("saray.ru", url.IdnHost);
}