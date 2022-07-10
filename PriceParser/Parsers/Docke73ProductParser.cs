using PriceParser.DomainComparator;
using PriceParser.Parsers.HtmlSearcher;
using PriceParser.Utils;

namespace PriceParser.Parsers;

public class Docke73ProductParser : WebPageProductParserBase
{
    public Docke73ProductParser() : base(new Docke73HtmlSearcher())
    {
    }

    public override bool IsSupportUrl(Uri url) =>
        InaccurateDomainComparator.DefaultInstance.Compare("docke73.ru", url.IdnHost);
}