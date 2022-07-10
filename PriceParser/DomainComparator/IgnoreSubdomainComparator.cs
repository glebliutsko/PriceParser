namespace PriceParser.DomainComparator;

public class IgnoreSubdomainComparator : IDomainComparator
{
    private static readonly Lazy<IgnoreSubdomainComparator> _defaultInstance =
        new(() => new IgnoreSubdomainComparator(2));

    private readonly int _levelDomain;

    public IgnoreSubdomainComparator(int levelDomain)
    {
        _levelDomain = levelDomain;
    }

    public static IgnoreSubdomainComparator DefaultInstance => _defaultInstance.Value;

    public bool Compare(string domain1, string domain2)
    {
        var domain1Prepare = TakeLastSubdomain(domain1);
        var domain2Prepare = TakeLastSubdomain(domain2);

        return domain1Prepare.SequenceEqual(domain2Prepare);
    }

    private string[] TakeLastSubdomain(string domain)
    {
        var subdomains = domain.Split('.');
        return subdomains.TakeLast(_levelDomain).ToArray();
    }
}