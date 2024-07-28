namespace XspecT.Test.Assert;

public class WhenList : Spec<List<string>>
{
    [Fact]
    public void IsEqual()
    {
        var values = new[] { "a", "b" };
        var list1 = values.ToList();
        var list2 = values.ToList();
        list1.Is().EqualTo(list2);
    }
}