using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.AutoFixture;

public class WhenGet : SubjectSpec<MyRetreiver, MyModel>
{
    public WhenGet() => When(_ => _.Get(An<int>()));

    [Fact]
    public void A_Value_Mentioned_Twice_Is_Same_Value()
        => Given<IMyRepository>().That(_ => _.Get(The<int>())).Returns(A<MyModel>)
        .Then().Result.Is(The<MyModel>());

    [Fact]
    public void Another_Value_Is_Not_Same_As_A_Value()
        => Given<IMyRepository>().That(_ => _.Get(Another<int>())).Returns(A<MyModel>)
        .Then().Result.Is().Not(The<MyModel>());

    [Fact]
    public void Another_Value_Mentioned_Twice_Are_Different_Values()
        => Given<IMyRepository>().That(_ => _.Get(The<int>())).Returns(Another<MyModel>)
        .Then().Result.Is().Not(Another<MyModel>());

    [Fact]
    public void A_Value_Of_Different_Type_Is_Different_Value()
        => Given<IMyRepository>().That(_ => _.Get(The<byte>())).Returns(A<MyModel>)
        .Then().Result.Is().Not(The<MyModel>());

    [Fact]
    public void A_Value_Is_Same_As_Any_Using_Value()
        => Using(new MyModel()).Then().Result.Is(The<MyModel>());

    [Fact]
    public void A_Value_Is_Same_As_Another_Value_If_Using()
        => Using(Another<MyModel>()).Then().Result.Is(The<MyModel>());

    [Fact]
    public void ASecond_Value_Mentioned_Twice_Is_Same_Value()
        => Given<IMyRepository>().That(_ => _.Get(The<int>())).Returns(ASecond<MyModel>)
        .Then().Result.Is(TheSecond<MyModel>());

    [Fact]
    public void ASecond_Value_Is_Not_Same_As_A_Value()
        => Given<IMyRepository>().That(_ => _.Get(The<int>())).Returns(A<MyModel>)
        .Then().Result.Is().Not(ASecond<MyModel>());

    [Fact]
    public void AThird_Value_Mentioned_Twice_Is_Same_Value()
        => Given<IMyRepository>().That(_ => _.Get(The<int>())).Returns(AThird<MyModel>)
        .Then().Result.Is(TheThird<MyModel>());

    [Fact]
    public void AThird_Value_Is_Not_Same_As_ASecond_Value()
        => Given<IMyRepository>().That(_ => _.Get(The<int>())).Returns(ASecond<MyModel>)
        .Then().Result.Is().Not(AThird<MyModel>());
}