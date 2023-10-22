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
        public void Then_It_Has_ProvidedValue(int value) => Using(value).Then().Result.Is(value);
    }
}