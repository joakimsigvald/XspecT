using XspecT.Fixture;
using XspecT.Fixture.Exceptions;
using XspecT.Verification;

namespace XspecT.Test.AutoMock;

public class WhenMockInt : SubjectSpec<StaticIntService, int>
{
    public WhenMockInt() => When(() => SUT.GetValue());
    public class GivenItWasNotProvided : WhenMockInt
    {
        [Fact]
        public void Then_Throws_CreateSubjectUnderTestFailed()
            => Assert.Throws<CreateSubjectUnderTestFailed>(Then);
    }

    public class GivenItWasProvided : WhenMockInt
    {
        [Fact] public void Then_It_Has_ProvidedValue() 
            => Using(0).Then().Result.Is(0);
    }
}

public class StaticIntService
{
    private readonly int _value;
    public StaticIntService(int value) => _value = value;
    public int GetValue() => _value;
}