using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.AutoFixture;

public class WhenGet : SubjectSpec<Retreiver, Model>
{
    public WhenGet() => When(_ => _.Get(An<int>()));

    [Fact]
    public void A_Value_Mentioned_Twice_Is_Same_Value()
        => Given<IRepository>().That(_ => _.Get(The<int>())).Returns(A<Model>)
        .Then().Result.Is(The<Model>());

    [Fact]
    public void Another_Value_Is_Not_Same_As_A_Value()
        => Given<IRepository>().That(_ => _.Get(Another<int>())).Returns(A<Model>)
        .Then().Result.Is().Not(The<Model>());

    [Fact]
    public void Another_Value_Mentioned_Twice_Are_Different_Values()
        => Given<IRepository>().That(_ => _.Get(The<int>())).Returns(Another<Model>)
        .Then().Result.Is().Not(Another<Model>());

    [Fact]
    public void A_Value_Of_Different_Type_Is_Different_Value()
        => Given<IRepository>().That(_ => _.Get(The<byte>())).Returns(A<Model>)
        .Then().Result.Is().Not(The<Model>());

    [Fact]
    public void A_Value_Is_Same_As_Any_Using_Value()
        => Using(new Model()).Then().Result.Is(The<Model>());

    [Fact]
    public void A_Value_Is_Same_As_Another_Value_If_Using()
        => Using(Another<Model>()).Then().Result.Is(The<Model>());

    [Fact]
    public void ASecond_Value_Mentioned_Twice_Is_Same_Value()
        => Given<IRepository>().That(_ => _.Get(The<int>())).Returns(ASecond<Model>)
        .Then().Result.Is(TheSecond<Model>());

    [Fact]
    public void ASecond_Value_Is_Not_Same_As_A_Value()
        => Given<IRepository>().That(_ => _.Get(The<int>())).Returns(A<Model>)
        .Then().Result.Is().Not(ASecond<Model>());

    [Fact]
    public void AThird_Value_Mentioned_Twice_Is_Same_Value()
        => Given<IRepository>().That(_ => _.Get(The<int>())).Returns(AThird<Model>)
        .Then().Result.Is(TheThird<Model>());

    [Fact]
    public void AThird_Value_Is_Not_Same_As_ASecond_Value()
        => Given<IRepository>().That(_ => _.Get(The<int>())).Returns(ASecond<Model>)
        .Then().Result.Is().Not(AThird<Model>());
}