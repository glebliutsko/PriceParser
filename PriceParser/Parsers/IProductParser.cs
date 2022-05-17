using PriceParser.Sites.Parsers.Models;

namespace PriceParser.Sites.Parsers;

public interface IProductParser
{
    Task Initialize();
    bool IsSupportUrl(Uri url);
    Task<Product> ParseAsync(Uri url);
}