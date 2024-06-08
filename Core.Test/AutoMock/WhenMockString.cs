using XspecT.Assert;

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

public class StaticStringService
{
    private readonly string _value;
    public StaticStringService(string value) => _value = value;
    public string GetValue() => _value;
}