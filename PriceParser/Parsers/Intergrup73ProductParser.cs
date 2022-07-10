using PriceParser.DomainComparator;
using PriceParser.Parsers.HtmlSearcher;
using PriceParser.Utils;

namespace PriceParser.Parsers;

public class Intergrup73ProductParser : WebPageProductParserBase
{
    public Intergrup73ProductParser() : base(new CssSelectorHtmlSearcher("h1.company-header-title", "span[data-price-type]"))
    {
    }

    public override bool IsSupportUrl(Uri url) =>
        InaccurateDomainComparator.DefaultInstance.Compare("intergrup73.ru", url.IdnHost);
}