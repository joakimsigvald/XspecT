namespace XspecT.Test.AutoMock;

public class WhenMockStringAndInt : Spec<StaticStringAndIntService, string>
{
    public WhenMockStringAndInt() => When(_ => _.GetValue());

    [Fact]
    public void Then_It_Has_TheStringAndInt()
    {
        Given(A<string>).And(An<int>).Then().Result.Is($"{The<string>()}:{The<int>()}");
        Description.Is(
            """
            Given an int
             and a string
            When _.GetValue()
            Then Result is "{The<string>()}:{The<int>()}"
            """);
    }

    public class GivenStringWasProvided : WhenMockStringAndInt
    {
        [Theory]
        [InlineData("hej")]
        public void Then_It_Has_ProvidedValue(string value)
        {
            Given(value).And(A<string>).And(An<int>).Then().Result.Does().Contain(value);
            Description.Is(
                """
                Given value
                 and an int
                 and a string
                When _.GetValue()
                Then Result contains value
                """);
        }
    }

    public class GivenIntWasProvided : WhenMockStringAndInt
    {
        [Theory]
        [InlineData(123)]
        [InlineData(456)]
        public void Then_It_Has_ProvidedValue(int value)
        {
            Given(A<string>).And(An<int>).And(value).Then().Result.Does().Contain($"{value}");
            Description.Is(
                """
                Given value
                 and an int
                 and a string
                When _.GetValue()
                Then Result contains "{value}"
                """);
        }
    }
}

public class StaticStringAndIntService(string strVal, int intVal)
{
    private readonly string _strVal = strVal;
    private readonly int _intVal = intVal;

    public string GetValue() => $"{_strVal}:{_intVal}";
}