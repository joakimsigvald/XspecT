using XspecT.Assert;

namespace XspecT.Test.Given;

public class WhenGivenInterface : Spec<MyService, string>
{
    [Fact]
    public void ThenUseValueInPipeline()
    {
        Given<IMySettings>(new MySettings { ConnectionString = ASecond<string>() })
            .When(_ => _.GetConnectionString())
            .Then().Result.Is(TheSecond<string>());
        Specification.Is(
            """
            Given new MySettings { ConnectionString = ASecond<string>() }
            When _.GetConnectionString()
            Then Result is the second string
            """);
    }
}