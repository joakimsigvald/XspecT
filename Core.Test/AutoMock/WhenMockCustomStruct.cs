﻿namespace XspecT.Test.AutoMock;

public class WhenMockCustomStruct : Spec<StaticValueService, MyValue<int>>
{
    public WhenMockCustomStruct() => When(_ => _.GetValue());
    public class GivenItWasNotProvided : WhenMockCustomStruct
    {
        [Fact]
        public void Then_It_Has_RandomCustomStruct()
        {
            Then().Result.Is().Not(A<MyValue<int>>()).And(Result).Value.Is().Not(0);
            Description.Is(
                """
                When _.GetValue()
                Then Result is not a MyValue<int>
                 and Result's Value is not 0
                """);
        }
    }

    public class GivenItWasProvided : WhenMockCustomStruct
    {
        [Fact]
        public void Then_It_Has_ProvidedValue()
        {
            Given(A<MyValue<int>>()).Then().Result.Is(The<MyValue<int>>());
            Description.Is(
                """
                Given a MyValue<int>
                When _.GetValue()
                Then Result is the MyValue<int>
                """);
        }
    }
}

public class StaticValueService(MyValue<int> value)
{
    private readonly MyValue<int> _value = value;
    public MyValue<int> GetValue() => _value;
}

public struct MyValue<TValue> where TValue : struct
{ 
    public TValue Value { get; set; }
}