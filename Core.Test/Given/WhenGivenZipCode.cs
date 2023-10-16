﻿using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.Given;

public class WhenGivenZipCode : SubjectSpec<MyValueTypeModel, MyZipCode>
{
    [Fact]
    public void GivenAutoGeneratedPrimitiveIsRestrictedInt_ThenValueIsUsed()
        => When(_ => _.ZipCode)
        .Then().Result.Is(The<MyZipCode>())
        .And(Result).Primitive.Is().NotLessThan(0).And.LessThan(100_000);
}