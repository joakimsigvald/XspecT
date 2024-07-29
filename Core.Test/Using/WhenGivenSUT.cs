using XspecT.Assert;
using XspecT.Test.Given;

namespace XspecT.Test.Using;

public class WhenGivenSUT : Spec<MyService, (int, string)> 
{
    public WhenGivenSUT()
        => Given(new MyService(An<IMyRepository>(), new MySettings { ConnectionString = A<string>() }, () => DateTime.Now))
            .And<IMyRepository>().That(_ => _.GetNextId()).Returns(An<int>)
        .When(_ => (_.GetNextId(), _.GetConnectionString()));

    [Fact]
    public void ThenUseSUT()
        => Then().Result.Is((The<int>(), The<string>()));
}