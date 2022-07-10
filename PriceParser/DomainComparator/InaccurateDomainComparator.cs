namespace PriceParser.DomainComparator;

public class InaccurateDomainComparator : IDomainComparator
{
    // Стандартный компаратор игнорирующие поддомены m и www. 
    private static readonly Lazy<InaccurateDomainComparator> _defaultInstance =
        new(() => new InaccurateDomainComparator(new[] {"m", "www"}));

    private readonly string[] _notMeaningfulDomain;

    public InaccurateDomainComparator(string[] notMeaningfulDomain)
    {
        _notMeaningfulDomain = notMeaningfulDomain;
    }

    public static InaccurateDomainComparator DefaultInstance => _defaultInstance.Value;

    public bool Compare(string domain1, string domain2)
    {
        var domain1Levels = GetDomainLevelsWithoutNotMeaningfulDomain(domain1);
        var domain2Levels = GetDomainLevelsWithoutNotMeaningfulDomain(domain2);

        return domain1Levels.SequenceEqual(domain2Levels);
    }

    private string[] GetDomainLevelsWithoutNotMeaningfulDomain(string domain)
    {
        var subdomains = domain.Split(".");
        return subdomains.Where(x => !_notMeaningfulDomain.Contains(x)).ToArray();
    }
}