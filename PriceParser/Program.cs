﻿using ClosedXML.Excel;
using PriceParser.Configuration;
using PriceParser.Sites.Parsers;
using PriceParser.Sites.Parsers.Models;

namespace PriceParser;

internal class OutputProduct
{
    public string Name { get; set; }
    public Uri Url { get; set; }
    public Product Product { get; set; }
}

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
        foreach (var product in _products)
        {
            worksheet.Cell($"A{row}").Value = product.Name;
            worksheet.Cell($"B{row}").Value = product.Url.IdnHost;
            worksheet.Cell($"C{row}").Value = product.Url;
            worksheet.Cell($"D{row}").Value = product.Product.Name;
            worksheet.Cell($"E{row}").Value = product.Product.Price;

            row++;
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
        new SarayProductParser()
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
        {
            parser.Initialize().Wait();
        }

        var products = ProductConfiguration.FromDirectory(directoryConfiguration);
        var result = new ExcelOutput();
        foreach (var product in products)
        foreach (var url in product.Urls)
            try
            {
                var parser = GetParser(url);
                if (parser == null)
                {
                    Console.WriteLine("Not support url");
                    continue;
                }

                var productData = parser.ParseAsync(url).Result;
                result.AddProduct(new OutputProduct {Name = product.Name, Product = productData, Url = url});
                Console.WriteLine(url);
            }
            catch (Exception e)
            {
                Console.WriteLine("!!!!!!!!Ошибка!!!!!!!");
                Console.WriteLine(url);
                Console.WriteLine(e);
                throw;
            }

        result.Save();
    }
}