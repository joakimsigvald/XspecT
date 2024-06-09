using XspecT.Assert;

namespace XspecT.Test.Given;

public record MyRecord(int Id, string Name);

public class WhenGivenRecord : Spec<MyService, MyRecord>
{
    [Fact]
    public void GivenSetup_ThenReturnSetupValue()
        => Given<MyRecord>(_ => _ with { Name = A<string>()})
        .When(_ => MyService.Echo(The<MyRecord>()))
        .Then().Result.Name.Is(The<string>());

    [Fact]
    public void GivenTwoSetup_ThenReturnSecondSetupAppliedToFirstSetup()
        => Given<MyRecord>(_ => _ with { Name = A<string>() })
        .And<MyRecord>(_ => _ with { Name = _.Name + ASecond<string>() })
        .When(_ => MyService.Echo(The<MyRecord>()))
        .Then().Result.Name.Does().StartWith(The<string>())
        .And.EndWith(TheSecond<string>());

    [Fact]
    public void GivenThatSetup_ThenReturnSetupValue()
        => Given().That(() => A<MyRecord>(_ => _ with { Name = A<string>() }))
        .When(_ => MyService.Echo(The<MyRecord>()))
        .Then().Result.Name.Is(The<string>());

    [Fact]
    public void GivenTwoThatSetup_ThenReturnFirstSetupAppliedToSecondSetup()
        => Given().That(() => A<MyRecord>(_ => _ with { Name = _.Name + ASecond<string>() }))
        .And().That(() => A<MyRecord>(_ => _ with { Name = A<string>() }))
        .When(_ => MyService.Echo(The<MyRecord>()))
        .Then().Result.Name.Is().StartingWith(The<string>()).And.EndingWith(TheSecond<string>());
}