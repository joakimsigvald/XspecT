using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.AutoMock;

public class WhenMockInt : SubjectSpec<StaticIntService, int>
{
    public WhenMockInt() => Given(An<int>).When(_ => _.GetValue());
    public class UsingAValue : WhenMockInt
    {
        [Fact] public void Then_It_Has_theValue() => Then().Result.Is(The<int>());
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