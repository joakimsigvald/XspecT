using XspecT.Assert;
using Xunit;

namespace XspecT.Test.Verification;

public class WhenDictionary : StaticSpec<Dictionary<string, int>>
{
    [Fact]
    public void IsEqual()
    {
        var pairs = new[] { ("a", 1), ("b", 2) };
        var dict1 = pairs.ToDictionary(p => p.Item1, p => p.Item2);
        var dict2 = pairs.ToDictionary(p => p.Item1, p => p.Item2);
        dict1.Is().EqualTo(dict2);
    }
}