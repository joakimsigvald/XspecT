using XspecT.Assert;

namespace XspecT.Test.AutoMock;

public class WhenMockStringAndInt : SubjectSpec<StaticStringAndIntService, string>
{
    public WhenMockStringAndInt() => When(_ => _.GetValue());

    [Fact]
    public void Then_It_Has_TheStringAndInt()
        => Given(A<string>).And(An<int>).Then().Result.Is($"{The<string>()}:{The<int>()}");

    public class GivenStringWasProvided : WhenMockString
    {
        [Theory]
        [InlineData("hej")]
        public void Then_It_Has_ProvidedValue(string value) 
            => Given(value).And(A<string>).And(An<int>).Then().Result.Contains(value);
    }

    public class GivenIntWasProvided : WhenMockString
    {
        [Theory]
        [InlineData(123)]
        [InlineData(456)]
        public void Then_It_Has_ProvidedValue(int value) 
            => Given(A<string>).And(An<int>).And(value).Then().Result.Contains($"{value}");
    }
}

public class StaticStringAndIntService
{
    private readonly string _strVal;
    private readonly int _intVal;

    public StaticStringAndIntService(string strVal, int intVal)
    {
        _strVal = strVal;
        _intVal = intVal;
    }

    public string GetValue() => $"{_strVal}:{_intVal}";
}