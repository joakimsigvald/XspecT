using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.AutoMock;

public class WhenMockString : SubjectSpec<StaticStringService, string>
{
    public WhenMockString() => When(_ => _.GetValue());
    public class GivenItWasNotProvided : WhenMockString
    {
        [Fact] public void Then_It_Has_FirstString() => Then().Result.Is(A<string>());
    }

    public class GivenItWasProvided : WhenMockString
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("hej")]
        public void Then_It_Has_ProvidedValue(string value) => Using(value).Then().Result.Is(value);
    }
}

public class StaticStringService
{
    private readonly string _value;
    public StaticStringService(string value) => _value = value;
    public string GetValue() => _value;
}