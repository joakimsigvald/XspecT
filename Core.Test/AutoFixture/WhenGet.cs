﻿using static XspecT.Test.Helper;

namespace XspecT.Test.AutoFixture;

public class WhenGet : Spec<MyRetriever, MyModel>
{
    public WhenGet() => When(_ => _.Get(An<int>()));

    [Fact]
    public void A_Value_Mentioned_Twice_Is_Same_Value()
    {
        Given<IMyRepository>().That(_ => _.Get(The<int>())).Returns(() => A<MyModel>())
            .Then().Result.Is(The<MyModel>());
        VerifyDescription(
@"Given IMyRepository.Get(the int) returns a MyModel,
 when Get(an int),
 then Result is the MyModel");
    }

    [Fact]
    public void Another_Value_Is_Not_Same_As_A_Value()
    {
        Given<IMyRepository>().That(_ => _.Get(Another<int>())).Returns(() => ASecond<MyModel>())
            .Then().Result.Is().Not(TheSecond<MyModel>());
        VerifyDescription(
@"Given IMyRepository.Get(another int) returns a second MyModel,
 when Get(an int),
 then Result is not the second MyModel");
    }

    [Fact]
    public void Another_Value_Mentioned_Twice_Are_Different_Values()
    {
        Given<IMyRepository>().That(_ => _.Get(The<int>())).Returns(() => Another<MyModel>())
            .Then().Result.Is().Not(Another<MyModel>());
        VerifyDescription(
@"Given IMyRepository.Get(the int) returns another MyModel,
 when Get(an int),
 then Result is not another MyModel");
    }

    [Fact]
    public void A_Value_Of_Different_Type_Is_Different_Value()
    {
        Given<IMyRepository>().That(_ => _.Get(The<byte>())).Returns(() => ASecond<MyModel>())
            .Then().Result.Is().Not(TheSecond<MyModel>());
        VerifyDescription(
@"Given IMyRepository.Get(the byte) returns a second MyModel,
 when Get(an int),
 then Result is not the second MyModel");
    }

    [Fact]
    public void A_Value_Is_Same_As_Any_Using_Value()
    {
        Given(new MyModel()).Then().Result.Is(The<MyModel>());
        VerifyDescription(
@"Given MyModel,
 when Get(an int),
 then Result is the MyModel");
    }

    [Fact]
    public void A_Value_Is_Same_As_Another_Value_If_Using()
    {
        Given(Another<MyModel>()).Then().Result.Is(The<MyModel>());
        VerifyDescription(
@"Given MyModel,
 when Get(an int),
 then Result is the MyModel");
    }

    [Fact]
    public void ASecond_Value_Mentioned_Twice_Is_Same_Value()
    {
        Given<IMyRepository>().That(_ => _.Get(The<int>())).Returns(() => ASecond<MyModel>())
            .Then().Result.Is(TheSecond<MyModel>());
        VerifyDescription(
@"Given IMyRepository.Get(the int) returns a second MyModel,
 when Get(an int),
 then Result is the second MyModel");
    }

    [Fact]
    public void ASecond_Value_Is_Not_Same_As_A_Value()
    {
        Given<IMyRepository>().That(_ => _.Get(The<int>())).Returns(() => A<MyModel>())
            .Then().Result.Is().Not(ASecond<MyModel>());
        VerifyDescription(
@"Given IMyRepository.Get(the int) returns a MyModel,
 when Get(an int),
 then Result is not a second MyModel");
    }

    [Fact]
    public void AThird_Value_Mentioned_Twice_Is_Same_Value()
    {
        Given<IMyRepository>().That(_ => _.Get(The<int>())).Returns(() => AThird<MyModel>())
            .Then().Result.Is(TheThird<MyModel>());
        VerifyDescription(
@"Given IMyRepository.Get(the int) returns a third MyModel,
 when Get(an int),
 then Result is the third MyModel");
    }

    [Fact]
    public void AThird_Value_Is_Not_Same_As_ASecond_Value()
    {
        Given<IMyRepository>().That(_ => _.Get(The<int>())).Returns(() => ASecond<MyModel>())
            .Then().Result.Is().Not(AThird<MyModel>());
        VerifyDescription(
@"Given IMyRepository.Get(the int) returns a second MyModel,
 when Get(an int),
 then Result is not a third MyModel");
    }
}