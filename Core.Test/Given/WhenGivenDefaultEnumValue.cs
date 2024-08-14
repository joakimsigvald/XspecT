namespace XspecT.Test.Given;

public class WhenGivenDefaultEnumValue : Spec<MyService, MyEnum>
{
    public WhenGivenDefaultEnumValue() => When(_ => MyService.Echo(The<MyEnum>())).Given(MyEnum.Two);
    [Fact]
    public void ThenUseDefaultValue()
    {
        Result.Is(MyEnum.Two);
        Description.Is(
            """
            Given MyEnum.Two
            When MyService.Echo(the MyEnum)
            Then Result is MyEnum.Two
            """);
    }
}