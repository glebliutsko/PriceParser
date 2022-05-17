using AngleSharp.Dom;
using PriceParser.Utils;

namespace PriceParser.Parsers.HtmlSearcher;

public class CssSelectorHtmlSearcher : IHtmlSearcher
{
    private readonly string _nameSelector;
    private readonly string _priceSelector;

    public CssSelectorHtmlSearcher(string nameSelector, string priceSelector)
    {
        _nameSelector = nameSelector;
        _priceSelector = priceSelector;
    }

    public string? FindName(IDocument document) =>
        document.QuerySelector(_nameSelector)?.GetTextNode()?.Text.Trim();

    public decimal? FindPrice(IDocument document)
    {
        var priceString = document.QuerySelector(_priceSelector)?.GetTextNode()?.Text.Trim();
        return decimal.TryParse(priceString, out var price) ? price : null;
    }
}