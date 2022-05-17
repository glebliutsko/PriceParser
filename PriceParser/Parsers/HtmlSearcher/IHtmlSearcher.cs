using AngleSharp.Dom;

namespace PriceParser.Sites.Parsers;

public interface IHtmlSearcher
{
    string? FindName(IDocument document);
    decimal? FindPrice(IDocument document);
}