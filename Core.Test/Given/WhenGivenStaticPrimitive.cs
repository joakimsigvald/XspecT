using XspecT.Assert;

namespace XspecT.Test.Given;

public class WhenGivenStaticPrimitive : Spec<string>
{
    [Theory]
    [InlineData("abc")]
    public void GivenDefaultValue_ThenUseDefault(string value)
    {
        Given().Default(value).When(_ => _).Then().Result.Is(value);
        Specification.Is(
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
        Specification.Is(
            """
            Given string { value }
            When _
            Then Result is value
            """);
    }

    [Theory]
    [InlineData("abc", "def")]
    public void GivenDefaultAndAValue_ThenUseAValue(string defaultValue, string aValue)
    {
        Given().Default(defaultValue).And().A(aValue)
            .When(_ => The<string>())
            .Then().Result.Is(aValue);
        Specification.Is(
            """
            Given defaultValue as default
              and a string { aValue }
            When the string
            Then Result is aValue
            """);
    }

    [Theory]
    [InlineData("abc", "def")]
    public void GivenDefaultAndASecondValue_ThenUseTheSecondValue(string defaultValue, string aValue)
    {
        Given().Default(defaultValue).And().ASecond(aValue)
            .When(_ => TheSecond<string>())
            .Then().Result.Is(aValue);
        Specification.Is(
            """
            Given defaultValue as default
              and a second string { aValue }
            When the second string
            Then Result is aValue
            """);
    }

    [Theory]
    [InlineData("abc", "def")]
    public void GivenDefaultAndAThirdValue_ThenUseTheThirdValue(string defaultValue, string aValue)
    {
        Given().Default(defaultValue).And().AThird(aValue)
            .When(_ => TheThird<string>())
            .Then().Result.Is(aValue);
        Specification.Is(
            """
            Given defaultValue as default
              and a third string { aValue }
            When the third string
            Then Result is aValue
            """);
    }

    [Theory]
    [InlineData("abc", "def")]
    public void GivenDefaultAndAFourthValue_ThenUseTheFourthValue(string defaultValue, string aValue)
    {
        Given().Default(defaultValue).And().AFourth(aValue)
            .When(_ => TheFourth<string>())
            .Then().Result.Is(aValue);
        Specification.Is(
            """
            Given defaultValue as default
              and a fourth string { aValue }
            When the fourth string
            Then Result is aValue
            """);
    }

    [Theory]
    [InlineData("abc", "def")]
    public void GivenDefaultAndAFifthValue_ThenUseTheFifthValue(string defaultValue, string aValue)
    {
        Given().Default(defaultValue).And().AFifth(aValue)
            .When(_ => TheFifth<string>())
            .Then().Result.Is(aValue);
        Specification.Is(
            """
            Given defaultValue as default
              and a fifth string { aValue }
            When the fifth string
            Then Result is aValue
            """);
    }

    [Theory]
    [InlineData("abc")]
    public void GivenTransformSecondValue_ThenApplyTransformOn_TheSecondValue(string value)
    {
        Given().ASecond<string>(_ => value)
            .When(_ => TheSecond<string>())
            .Then().Result.Is(value);
        Specification.Is(
            """
            Given a second string { value }
            When the second string
            Then Result is value
            """);
    }

    [Theory]
    [InlineData("abc")]
    public void GivenTransformThirdValue_ThenApplyTransformOn_TheThirdValue(string value)
    {
        Given().AThird<string>(_ => value)
            .When(_ => TheThird<string>())
            .Then().Result.Is(value);
        Specification.Is(
            """
            Given a third string { value }
            When the third string
            Then Result is value
            """);
    }

    [Theory]
    [InlineData("abc")]
    public void GivenTransformFourthValue_ThenApplyTransformOn_TheFourthValue(string value)
    {
        Given().AFourth<string>(_ => value)
            .When(_ => TheFourth<string>())
            .Then().Result.Is(value);
        Specification.Is(
            """
            Given a fourth string { value }
            When the fourth string
            Then Result is value
            """);
    }

    [Theory]
    [InlineData("abc")]
    public void GivenTransformFifthValue_ThenApplyTransformOn_TheFifthValue(string value)
    {
        Given().AFifth<string>(_ => value)
            .When(_ => TheFifth<string>())
            .Then().Result.Is(value);
        Specification.Is(
            """
            Given a fifth string { value }
            When the fifth string
            Then Result is value
            """);
    }
}