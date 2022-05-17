using AngleSharp;
using PriceParser.Http;
using PriceParser.Http.Url;
using PriceParser.Parsers.HtmlSearcher;
using PriceParser.Parsers.Models;

namespace PriceParser.Parsers;

public abstract class WebPageProductParserBase : IProductParser
{
    private readonly IHtmlSearcher _searcher;
    private readonly IUrlCleaner? _urlCleaner;

    protected WebPageProductParserBase(IHtmlSearcher searcher, IUrlCleaner? urlCleaner = null)
    {
        _searcher = searcher;
        _urlCleaner = urlCleaner;
    }

    protected HttpLoader Loader { get; } = new();

    public virtual Task Initialize() =>
        Task.CompletedTask;

    public abstract bool IsSupportUrl(Uri url);

    public async Task<Product> ParseAsync(Uri url)
    {
        if (_urlCleaner != null)
            url = _urlCleaner.CleanUrl(url);

        var content = await Loader.Load(url);

        var context = BrowsingContext.New(AngleSharp.Configuration.Default);
        var document = await context.OpenAsync(req => req.Content(content));

        var name = _searcher.FindName(document);
        var price = _searcher.FindPrice(document);

        return new Product(name, Convert.ToDecimal(price));
    }
}