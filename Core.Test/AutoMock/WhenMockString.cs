namespace XspecT.Test.AutoMock;

public class WhenMockString : Spec<StaticStringService, string>
{
    public WhenMockString() => Given(A<string>).When(_ => _.GetValue());
    public class UsingAString : WhenMockString
    {
        [Fact] public void Then_It_Has_TheString() => Then().Result.Is(The<string>());
    }

    public class GivenItWasProvided : WhenMockString
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("hej")]
        public void Then_It_Has_ProvidedValue(string value) => Given(value).Then().Result.Is(value);
    }
}

public class StaticStringService(string value)
{
    private readonly string _value = value;
    public string GetValue() => _value;
}

public class WhenSubjectIsString : Spec<string>
{
    [Fact] public void ThenUseDefaultString() => Given("abc").When(_ => _).Then().Result.Is("abc");
}