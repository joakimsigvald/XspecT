﻿using XspecT.Assert;

namespace XspecT.Test.Given;

public class WhenGivenFullName : Spec<MyValueTypeModel, MyFullName>
{
    [Fact]
    public void GivenAutoGeneratedPrimitiveIsName_ThenValueIsUsed()
    {
        Given(A<MyFullName>()).When(_ => _.Name)
            .Then().Result.Is(The<MyFullName>())
            .And(Result).Primitive.first.Primitive.Is().Not().NullOrEmpty()
            .And(Result).Primitive.middle.Primitive.Is().Not().NullOrEmpty()
            .And(Result).Primitive.last.Primitive.Is().Not().NullOrEmpty();
        Specification.Is(
            """
            Given a MyFullName
            When _.Name
            Then Result is the MyFullName
              and Result.Primitive.first.Primitive is not null or empty
              and Result.Primitive.middle.Primitive is not null or empty
              and Result.Primitive.last.Primitive is not null or empty
            """);
    }
}