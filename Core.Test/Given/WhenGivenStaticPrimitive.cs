namespace XspecT.Test.Given;

public class WhenGivenStaticPrimitive : Spec<string>
{
    [Theory]
    [InlineData("abc")]
    public void GivenDefaultValue_ThenUseDefault(string value)
    {
        Given().Default(value).When(_ => _).Then().Result.Is(value);
        VerifyDescription(
            """
            Given value as default
            When _
            Then Result is value
            """);
    }

    [Theory]
    [InlineData("abc")]
    public void GivenDefaultSetup_ThenUseDefaultSetup(string value)
    {
        Given().Default<string>(_ => value).When(_ => _).Then().Result.Is(value);
        VerifyDescription(
            """
            Given value as default
            When _
            Then Result is value
            """);
    }

    [Theory]
    [InlineData("abc", "def")]
    public void GivenDefaultAndAValue_ThenUseAValue(string defaultValue, string aValue)
        => Given().Default(defaultValue).And().A(aValue)
        .When(_ => The<string>())
        .Then().Result.Is(aValue);

    [Theory]
    [InlineData("abc", "def")]
    public void GivenDefaultAndASecondValue_ThenUseTheSecondValue(string defaultValue, string aValue)
        => Given().Default(defaultValue).And().ASecond(aValue)
        .When(_ => TheSecond<string>())
        .Then().Result.Is(aValue);

    [Theory]
    [InlineData("abc", "def")]
    public void GivenDefaultAndAThirdValue_ThenUseTheThirdValue(string defaultValue, string aValue)
        => Given().Default(defaultValue).And().AThird(aValue)
        .When(_ => TheThird<string>())
        .Then().Result.Is(aValue);

    [Theory]
    [InlineData("abc", "def")]
    public void GivenDefaultAndAFourthValue_ThenUseTheFourthValue(string defaultValue, string aValue)
        => Given().Default(defaultValue).And().AFourth(aValue)
        .When(_ => TheFourth<string>())
        .Then().Result.Is(aValue);

    [Theory]
    [InlineData("abc", "def")]
    public void GivenDefaultAndAFifthValue_ThenUseTheFifthValue(string defaultValue, string aValue)
        => Given().Default(defaultValue).And().AFifth(aValue)
        .When(_ => TheFifth<string>())
        .Then().Result.Is(aValue);

    [Theory]
    [InlineData("abc")]
    public void GivenTransformSecondValue_ThenApplyTransformOn_TheSecondValue(string value)
        => Given().ASecond<string>(_ => value)
        .When(_ => TheSecond<string>())
        .Then().Result.Is(value);

    [Theory]
    [InlineData("abc")]
    public void GivenTransformThirdValue_ThenApplyTransformOn_TheThirdValue(string value)
        => Given().AThird<string>(_ => value)
        .When(_ => TheThird<string>())
        .Then().Result.Is(value);

    [Theory]
    [InlineData("abc")]
    public void GivenTransformFourthValue_ThenApplyTransformOn_TheFourthValue(string value)
        => Given().AFourth<string>(_ => value)
        .When(_ => TheFourth<string>())
        .Then().Result.Is(value);

    [Theory]
    [InlineData("abc")]
    public void GivenTransformFifthValue_ThenApplyTransformOn_TheFifthValue(string value)
        => Given().AFifth<string>(_ => value)
        .When(_ => TheFifth<string>())
        .Then().Result.Is(value);
}