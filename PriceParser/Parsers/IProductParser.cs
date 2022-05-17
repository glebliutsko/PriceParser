using PriceParser.Parsers.Models;

namespace PriceParser.Parsers;

public interface IProductParser
{
    Task Initialize();
    bool IsSupportUrl(Uri url);
    Task<Product> ParseAsync(Uri url);
}