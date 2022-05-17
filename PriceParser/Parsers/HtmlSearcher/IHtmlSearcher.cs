using AngleSharp.Dom;

namespace PriceParser.Parsers.HtmlSearcher;

public interface IHtmlSearcher
{
    string? FindName(IDocument document);
    decimal? FindPrice(IDocument document);
}