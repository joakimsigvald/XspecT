using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.AutoMock;

public class WhenMockInt : SubjectSpec<StaticIntService, int>
{
    public WhenMockInt() => When(_ => _.GetValue());
    public class GivenItWasNotProvided : WhenMockInt
    {
        [Fact] public void Then_It_Has_FirstInt() => Then().Result.Is(An<int>());
    }

    public class GivenItWasProvided : WhenMockInt
    {
        [Theory]
        [InlineData(0)]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public void Then_It_Has_ProvidedValue(int value) => Given(value).Then().Result.Is(value);
    }
}

public class StaticIntService
{
    private readonly int _value;
    public StaticIntService(int value) => _value = value;
    public int GetValue() => _value;
}