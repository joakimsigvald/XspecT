using XspecT.Assert;

using static XspecT.Test.Helper;

namespace XspecT.Test.AutoFixture;

public class WhenGet : Spec<MyRetriever, MyModel>
{
    public WhenGet() => When(_ => _.Get(An<int>()));

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void A_Value_Mentioned_Twice_Is_Same_Value(bool describe)
    {
        Given<IMyRepository>().That(_ => _.Get(The<int>())).Returns(A<MyModel>)
            .Then().Result.Is(The<MyModel>());
        if (describe)
            VerifyDescription(
@"Given IMyRepository that Get with the int returns a MyModel,
 when Get with an int,
 then result is the MyModel");
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Another_Value_Is_Not_Same_As_A_Value(bool fail)
    {
        Given<IMyRepository>().That(_ => _.Get(Another<int>())).Returns(ASecond<MyModel>)
            .Then().Result.Is().Not(TheSecond<MyModel>());

        if (fail)
            VerifyDescription(
@"Given IMyRepository that Get with another int returns a second MyModel,
 when Get with an int,
 then result is not the second MyModel");
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Another_Value_Mentioned_Twice_Are_Different_Values(bool fail)
    {
        Given<IMyRepository>().That(_ => _.Get(The<int>())).Returns(Another<MyModel>)
            .Then().Result.Is().Not(Another<MyModel>());

        if (fail)
            VerifyDescription(
@"Given IMyRepository that Get with the int returns another MyModel,
 when Get with an int,
 then result is not another MyModel");
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void A_Value_Of_Different_Type_Is_Different_Value(bool fail)
    {
        Given<IMyRepository>().That(_ => _.Get(The<byte>())).Returns(ASecond<MyModel>)
            .Then().Result.Is().Not(TheSecond<MyModel>());

        if (fail)
            VerifyDescription(
@"Given IMyRepository that Get with the byte returns a second MyModel,
 when Get with an int,
 then result is not the second MyModel");
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void A_Value_Is_Same_As_Any_Using_Value(bool fail)
    {
        Given(new MyModel()).Then().Result.Is(The<MyModel>());

        if (fail)
            VerifyDescription(
@"Given MyModel,
 when Get with an int,
 then result is the MyModel");
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void A_Value_Is_Same_As_Another_Value_If_Using(bool fail)
    {
        Given(Another<MyModel>()).Then().Result.Is(The<MyModel>());

        if (fail)
            VerifyDescription(
@"Given MyModel,
 when Get with an int,
 then result is the MyModel");
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ASecond_Value_Mentioned_Twice_Is_Same_Value(bool fail)
    {
        Given<IMyRepository>().That(_ => _.Get(The<int>())).Returns(ASecond<MyModel>)
            .Then().Result.Is(TheSecond<MyModel>());

        if (fail)
            VerifyDescription(
@"Given IMyRepository that Get with the int returns a second MyModel,
 when Get with an int,
 then result is the second MyModel");
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ASecond_Value_Is_Not_Same_As_A_Value(bool fail)
    {
        Given<IMyRepository>().That(_ => _.Get(The<int>())).Returns(A<MyModel>)
            .Then().Result.Is().Not(ASecond<MyModel>());

        if (fail)
            VerifyDescription(
@"Given IMyRepository that Get with the int returns a MyModel,
 when Get with an int,
 then result is not a second MyModel");
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void AThird_Value_Mentioned_Twice_Is_Same_Value(bool fail)
    {
        Given<IMyRepository>().That(_ => _.Get(The<int>())).Returns(AThird<MyModel>)
            .Then().Result.Is(TheThird<MyModel>());

        if (fail)
            VerifyDescription(
@"Given IMyRepository that Get with the int returns a third MyModel,
 when Get with an int,
 then result is the third MyModel");
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void AThird_Value_Is_Not_Same_As_ASecond_Value(bool fail)
    {
        Given<IMyRepository>().That(_ => _.Get(The<int>())).Returns(ASecond<MyModel>)
            .Then().Result.Is().Not(AThird<MyModel>());

        if (fail)
            VerifyDescription(
@"Given IMyRepository that Get with the int returns a second MyModel,
 when Get with an int,
 then result is not a third MyModel");
    }
}