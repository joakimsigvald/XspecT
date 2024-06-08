using XspecT.Assert;
using XspecT.Test.AutoFixture;

namespace XspecT.Test.AutoMock;

public class WhenReturnsDefaultInt : Spec<MyDefaultService, int>
{
    [Fact]
    public void ThenReturnZero()
        => Given<IDefaultRetreiver>().That(_ => _.GetInt()).ReturnsDefault()
        .When(_ => _.GetInt()).Then().Result.Is(0);
}

public class WhenReturnsDefaultModel : Spec<MyDefaultService, MyModel>
{
    [Fact]
    public void ThenReturnNull()
        => Given<IDefaultRetreiver>().That(_ => _.GetModel()).ReturnsDefault()
        .When(_ => _.GetModel()).Then().Result.Is().Null();
}

public interface IDefaultRetreiver
{
    int GetInt();
    object GetObject();
    MyModel GetModel();
}

public class MyDefaultService(IDefaultRetreiver retreiver)
{
    private readonly IDefaultRetreiver _retreiver = retreiver;
    public int GetInt() => _retreiver.GetInt();
    public object GetObject() => _retreiver.GetObject();
    public MyModel GetModel() => _retreiver.GetModel();
}