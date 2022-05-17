namespace PriceParser.Utils;

public class DomainInaccurateComparator
{
    // Стандартный компаратор игнорирующие поддомены m и www. 
    private static readonly Lazy<DomainInaccurateComparator> _defaultInstance =
        new(() => new DomainInaccurateComparator(new[] {"m", "www"}));

    private readonly string[] _notMeaningfulDomain;

    public DomainInaccurateComparator(string[] notMeaningfulDomain)
    {
        _notMeaningfulDomain = notMeaningfulDomain;
    }

    public static DomainInaccurateComparator DefaultInstance => _defaultInstance.Value;

    private string[] GetDomainLevelsWithoutNotMeaningfulDomain(string domain)
    {
        var domainLevels = domain.Split(".").ToArray();
        return domainLevels.Where(x => !_notMeaningfulDomain.Contains(x)).ToArray();
    }

    public bool Compare(string domain1, string domain2)
    {
        var domain1Levels = GetDomainLevelsWithoutNotMeaningfulDomain(domain1);
        var domain2Levels = GetDomainLevelsWithoutNotMeaningfulDomain(domain2);

        return domain1Levels.SequenceEqual(domain2Levels);
    }
}