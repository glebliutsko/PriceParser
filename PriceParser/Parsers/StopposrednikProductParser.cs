using PriceParser.DomainComparator;
using PriceParser.Parsers.HtmlSearcher;

namespace PriceParser.Parsers;

public class StopposrednikProductParser : WebPageProductParserBase
{
    public StopposrednikProductParser() : base(new CssSelectorHtmlSearcher(".company-header-title[itemprop=name]", ".bp-price.fsn"))
    {
    }

    public override bool IsSupportUrl(Uri url) =>
        IgnoreSubdomainComparator.DefaultInstance.Compare("stopposrednik.ru", url.IdnHost);
}