using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceParser.Utils;

namespace PriceParser.Parsers.HtmlSearcher;

public class Docke73HtmlSearcher : IHtmlSearcher
{
    public string? FindName(IHtmlDocument document) =>
        document.QuerySelector(".standart-width-sw h1")?.GetTextNode()?.Text.Trim();

    public decimal? FindPrice(IHtmlDocument document)
    {
        var priceElement = document.QuerySelector(".price-tovar-sw");
        if (priceElement == null)
            return null;
        
        var priceString = priceElement.ChildNodes.OfType<IText>().LastOrDefault()?.Text;
        if (string.IsNullOrEmpty(priceString))
            return null;

        try
        {
            // Цена на сайте имеет такой вид: 869₽/м²
            priceString = priceString.Split("₽")[0];
        }
        catch (IndexOutOfRangeException)
        {
            return null;
        }
        
        return decimal.TryParse(priceString, out var price) ? price : null;
    }
}