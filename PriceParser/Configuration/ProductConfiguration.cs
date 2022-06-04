namespace PriceParser.Configuration;

public class ProductConfiguration
{
    public ProductConfiguration(string name, Uri[] urls)
    {
        Name = name;
        Urls = urls;
    }

    public string Name { get; init; }
    public Uri[] Urls { get; init; }

    public static ProductConfiguration FromFile(FileInfo file)
    {
        if (!file.Exists)
            throw new FileNotFoundException($"{file.FullName} not found.");


        List<string> contentLines;
        using (var reader = file.OpenText())
        {
            var content = reader.ReadToEnd();
            contentLines = content.Split(new[] {"\n", "\r\n"}, StringSplitOptions.None)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();
        }

        var name = contentLines[0];
        contentLines.RemoveAt(0);
        contentLines.Remove(string.Empty);
        var urls = contentLines.Select(x => new Uri(x)).ToArray();

        return new ProductConfiguration(name, urls);
    }

    public static List<ProductConfiguration> FromDirectory(DirectoryInfo directory)
    {
        if (!directory.Exists)
            throw new DirectoryNotFoundException($"{directory.FullName} not found.");

        var result = new List<ProductConfiguration>();
        foreach (var file in directory.GetFiles("*.txt"))
            result.Add(FromFile(file));

        return result;
    }
}