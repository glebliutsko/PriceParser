using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceParser.Utils;

namespace PriceParser.Parsers.HtmlSearcher;

public class TechnonikolHtmlSearcher : IHtmlSearcher
{
    public string? FindName(IHtmlDocument document) =>
        document.QuerySelector("h1.product__name")?.GetTextNode()?.Text.Trim();

    public decimal? FindPrice(IHtmlDocument document)
    {
        var priceElement = document.QuerySelector("ul.js-product-discount1");
        var priceString = priceElement?.Attributes["data-price"]?.Value;
        return decimal.TryParse(priceString, out var price) ? price : null;
    }
}