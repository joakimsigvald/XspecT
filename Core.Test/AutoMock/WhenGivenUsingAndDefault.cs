using XspecT.Assert;

namespace XspecT.Test.AutoMock;

public class WhenGivenUsingAndDefault : Spec<MyStringWrapper, string> 
{
    [Fact]
    public void ThenApplyBothAsValues()
        => Given().Default("ABC").And().Using("DEF")
        .When(_ => _.GetStrings(A<string>()))
        .Then().Result.Is("DEFABC");

    [Fact]
    public void ThenApplyBothAsLambdas()
        => Given().Default(() => "ABC").And().Using(() => "DEF")
        .When(_ => _.GetStrings(A<string>()))
        .Then().Result.Is("DEFABC");
}
