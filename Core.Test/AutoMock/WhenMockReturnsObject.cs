﻿using XspecT.Assert;

namespace XspecT.Test.AutoMock;

public class WhenMockReturnsObject : Spec<MyValueIntService, object>
{
    public WhenMockReturnsObject() => When(_ => _.GetObject());

    [Fact]
    public void Then_ReturnAnObject()
    {
        Result.GetType().Is(typeof(object));
        Specification.Is(
            """
            When _.GetObject()
            Then Result.GetType() is typeof object
            """);
    }
}