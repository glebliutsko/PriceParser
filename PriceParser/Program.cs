using System.Web;
using ClosedXML.Excel;
using PriceParser.Configuration;
using PriceParser.Parsers;
using PriceParser.Parsers.Models;

namespace PriceParser;

internal record OutputProduct(string Name, Uri Url, Product Product);

internal class ExcelOutput
{
    private readonly List<OutputProduct> _products = new();

    public void AddProduct(OutputProduct product)
    {
        _products.Add(product);
    }

    public void Save()
    {
        var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Цены");

        worksheet.Cell("A1").Value = "Название";
        worksheet.Cell("B1").Value = "Сайт";
        worksheet.Cell("C1").Value = "Ссылка";
        worksheet.Cell("D1").Value = "Название (на сайте)";
        worksheet.Cell("E1").Value = "Цена";

        var row = 2;
        var previousName = string.Empty;
        var isGrey = true;
        foreach (var product in _products)
        {
            if (product.Name != previousName)
                isGrey = !isGrey;

            if (isGrey)
                worksheet.Row(row).Style.Fill.BackgroundColor = XLColor.FromHtml("#CFCFCF");
            
            worksheet.Cell($"A{row}").Value = product.Name;
            worksheet.Cell($"B{row}").Value = product.Url.IdnHost;
            worksheet.Cell($"C{row}").Value = product.Url;
            
            if (product.Product == null)
            {
                worksheet.Row(row).Style.Fill.BackgroundColor = XLColor.Red;
                worksheet.Cell($"D{row}").Value = "Error";
                worksheet.Cell($"E{row}").Value = "Error";
            }
            else
            {
                worksheet.Cell($"D{row}").Value = product.Product.Name;
                worksheet.Cell($"E{row}").Value = product.Product.Price;
            }

            row++;

            previousName = product.Name;
        }

        workbook.SaveAs(DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss") + ".xlsx");
    }
}

public class Program
{
    private const string PATH_DIRECTORY_CONFIGURATION = "./products/";

    private static readonly IProductParser[] _parsers =
    {
        new Ig73ProductParser(),
        new Intergrup73ProductParser(),
        new MegastroyProductParser(new MegastroyShop(6, "ulyanovsk.megastroy.com", 10)),
        new SarayProductParser(),
        new TechnonikolProductParser(new TechnonikolShop("73000001000", HttpUtility.UrlEncode("Ульяновск"), 21)),
        new Incom73ProductParser(),
        new SiMarketParser()
    };

    private static IProductParser? GetParser(Uri url) =>
        _parsers.FirstOrDefault(parser => parser.IsSupportUrl(url));

    private static void Main(string[] args)
    {
        var directoryConfiguration = new DirectoryInfo(PATH_DIRECTORY_CONFIGURATION);
        if (!directoryConfiguration.Exists)
        {
            directoryConfiguration.Create();
            Console.WriteLine("Папка с настройками создана.");
            Environment.Exit(0);
        }

        foreach (var parser in _parsers)
            parser.Initialize().Wait();

        var products = ProductConfiguration.FromDirectory(directoryConfiguration);
        var result = new ExcelOutput();
        foreach (var product in products)
        {
            foreach (var url in product.Urls)
            {
                try
                {
                    var parser = GetParser(url);
                    if (parser == null)
                    {
                        Console.WriteLine("Not support url");
                        continue;
                    }

                    var productData = parser.ParseAsync(url).Result;
                    result.AddProduct(new OutputProduct(product.Name, url, productData));
                    Console.WriteLine(url);
                }
                catch (Exception e)
                {
                    Console.WriteLine("!!!!!!!!Ошибка!!!!!!!");
                    Console.WriteLine(url);
                    Console.WriteLine(e);
                    
                    result.AddProduct(new OutputProduct(product.Name, url, null));
                }
            }
        }

        result.Save();
    }
}