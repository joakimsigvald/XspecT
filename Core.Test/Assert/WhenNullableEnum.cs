﻿using XspecT.Assert;
using XspecT.Test.TestData;

namespace XspecT.Test.Assert;

public class WhenNullableEnum : Spec<MyEnum?>
{
    [Fact]
    public void EnumIsSame()
    {
        The<MyEnum?>().Is(The<MyEnum?>());
        Specification.Is("The MyEnum? is the MyEnum?");
    }

    [Fact]
    public void EnumIsNotNull()
    {
        The<MyEnum?>().Is().Not(null);
        Specification.Is("The MyEnum? is not null");
    }

    [Fact]
    public void NullIsNull()
    {
        The((MyEnum?)null).Is(null);
        Specification.Is("The (MyEnum?)null is null");
    }
}
