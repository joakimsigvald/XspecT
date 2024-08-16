﻿using XspecT.Test.Given;

namespace XspecT.Test.AutoMock;

public class WhenReturnsDefaultValue : Spec<MyValueIntService, string>
{
    [Fact]
    public void GivenMethodReturnsValue_ThenUseDefaultValue()//TODO
    {
        When(_ => _.GetValue(A<MyValueInt>()))
            .Given<IMyValueIntRepo>().Returns(A<string>)
            .Then().Result.Is(The<string>());
        Specification.Is(
            """
            Given IMyValueIntRepo returns a string
            When _.GetValue(a MyValueInt)
            Then Result is the string
            """);
    }

    [Fact]
    public void GivenMethodReturnsTaskOfValue_ThenUseDefaultValue()
    {
        When(_ => _.GetValueAsync(A<MyValueInt>()))
            .Given<IMyValueIntRepo>().Returns(A<string>)
            .Then().Result.Is(The<string>());
        Specification.Is(
            """
            Given IMyValueIntRepo returns a string
            When _.GetValueAsync(a MyValueInt)
            Then Result is the string
            """);
    }

    [Fact]
    public void GivenSpecificSetupOfMethodReturns_ThenSpecificValue()
    {
        When(_ => _.GetValueAsync(A<MyValueInt>()))
            .Given<IMyValueIntRepo>().Returns(A<string>)
            .AndThat(_ => _.GetAsync(The<MyValueInt>()))
            .Returns(ASecond<string>)
            .Then().Result.Is(TheSecond<string>());
        Specification.Is(
            """
            Given IMyValueIntRepo returns a string
              and GetAsync(the MyValueInt) returns a second string
            When _.GetValueAsync(a MyValueInt)
            Then Result is the second string
            """);
    }
}