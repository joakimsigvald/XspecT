﻿namespace XspecT.Test.Given;

public class WhenGivenFullName : Spec<MyValueTypeModel, MyFullName>
{
    [Fact]
    public void GivenAutoGeneratedPrimitiveIsName_ThenValueIsUsed()
    {
        Given(A<MyFullName>()).When(_ => _.Name)
            .Then().Result.Is(The<MyFullName>())
            .And(Result).Primitive.first.Primitive.Is().NotNullOrEmpty()
            .And(Result).Primitive.middle.Primitive.Is().NotNullOrEmpty()
            .And(Result).Primitive.last.Primitive.Is().NotNullOrEmpty();
        Specification.Is(
            """
            Given a MyFullName
            When _.Name
            Then Result is the MyFullName
              and Result's Primitive.first.Primitive is not null or empty
              and Result's Primitive.middle.Primitive is not null or empty
              and Result's Primitive.last.Primitive is not null or empty
            """);
    }
}