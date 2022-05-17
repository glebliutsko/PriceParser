using AngleSharp.Dom;

namespace PriceParser.Sites.Parsers;

public class CssSelectorHtmlSearcher : IHtmlSearcher
{
    private readonly string _nameSelector;
    private readonly string _priceSelector;

    public CssSelectorHtmlSearcher(string nameSelector, string priceSelector)
    {
        _nameSelector = nameSelector;
        _priceSelector = priceSelector;
    }

    public string? FindName(IDocument document)
    {
        return GetTextNodes(document, _nameSelector)?.Text.Trim();
    }

    public decimal? FindPrice(IDocument document)
    {
        var priceString = GetTextNodes(document, _priceSelector)?.Text.Trim();
        return decimal.TryParse(priceString, out var price) ? price : null;
    }

    private IText? GetTextNodes(IDocument document, string selector)
    {
        var parentNode = document.QuerySelector(selector);
        return parentNode?.ChildNodes.OfType<IText>().FirstOrDefault();
    }
}