using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.AutoMock;

public class WhenMockNTuple : SubjectSpec<StaticNTupleService, (int, string, int, float)>
{
    public WhenMockNTuple() => Using(A<(int, string, int, float)>).When(_ => _.GetValue());
    public class UsingAValue : WhenMockNTuple
    {
        [Fact] public void Then_It_Has_TheValue() => Then().Result.Is(The<(int, string, int, float)>());
    }

    public class GivenItWasProvided : WhenMockNTuple
    {
        [Theory]
        [InlineData(0, null, 1, 2)]
        [InlineData(1, "", 2, 3)]
        [InlineData(2, "hej", 3, 4)]
        public void Then_It_Has_ProvidedValue(int v1, string v2, int v3, float v4)
            => Given((v1, v2, v3, v4)).Then().Result.Is((v1, v2, v3, v4));
    }
}

public class StaticNTupleService
{
    private readonly (int, string, int, float) _value;
    public StaticNTupleService((int, string, int, float) value) => _value = value;
    public (int, string, int, float) GetValue() => _value;
}