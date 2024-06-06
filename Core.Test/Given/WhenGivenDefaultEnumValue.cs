using XspecT.Assert;

namespace XspecT.Test.Given;

public class WhenGivenDefaultEnumValue : SubjectSpec<MyService, MyEnum>
{
    public WhenGivenDefaultEnumValue() => When(_ => MyService.Echo(The<MyEnum>())).Given(MyEnum.Two);
    [Fact] public void ThenUseDefaultValue() => Result.Is(MyEnum.Two);
}