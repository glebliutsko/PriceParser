using AngleSharp.Dom;

namespace PriceParser.Utils;

public static class ElementExtension
{
    public static IText? GetTextNode(this IElement element) =>
        element.ChildNodes.OfType<IText>().FirstOrDefault();
}