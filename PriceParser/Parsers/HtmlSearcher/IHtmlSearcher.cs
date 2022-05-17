using AngleSharp.Html.Dom;

namespace PriceParser.Parsers.HtmlSearcher;

public interface IHtmlSearcher
{
    string? FindName(IHtmlDocument document);
    decimal? FindPrice(IHtmlDocument document);
}