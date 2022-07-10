using PriceParser.DomainComparator;
using PriceParser.Parsers.HtmlSearcher;
using PriceParser.Utils;

namespace PriceParser.Parsers;

public class SiMarketParser : WebPageProductParserBase
{
    public SiMarketParser() : base(new SiMarketHtmlSearcher())
    {
    }

    public override bool IsSupportUrl(Uri url) =>
        InaccurateDomainComparator.DefaultInstance.Compare("si-market.ru", url.IdnHost);
}