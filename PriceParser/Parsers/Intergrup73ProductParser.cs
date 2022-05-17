using PriceParser.Utils;

namespace PriceParser.Sites.Parsers;

public class Intergrup73ProductParser : WebPageProductParserBase
{
    public Intergrup73ProductParser() : base(new CssSelectorHtmlSearcher("h1.company-header-title", "span[data-price-type]"))
    {
    }

    public override bool IsSupportUrl(Uri url) =>
        DomainInaccurateComparator.DefaultInstance.Compare("intergrup73.ru", url.IdnHost);
}