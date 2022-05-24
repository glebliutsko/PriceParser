using System.Globalization;
using AngleSharp.Html.Dom;
using PriceParser.Utils;

namespace PriceParser.Parsers.HtmlSearcher;

public class SiMarketHtmlSearcher : IHtmlSearcher
{
    public string? FindName(IHtmlDocument document) =>
        document.QuerySelector("div.offer-name")?.GetTextNode()?.Text.Trim();

    public decimal? FindPrice(IHtmlDocument document)
    {
        var providerFormat = new CultureInfo("en-US");
        const NumberStyles numberStyle = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;

        var priceElement = document.QuerySelector("div.actual_price");
        var priceString = priceElement?.Attributes["data-price"]?.Value;
        return decimal.TryParse(priceString, numberStyle, providerFormat, out var price) ? price : null;
    }
}