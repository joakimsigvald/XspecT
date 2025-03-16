using XspecT.Assert;
using XspecT.Test.Subjects.RecordStructDefaults;

namespace XspecT.Test.Tests.KeyService;

public class KeyServiceSpec : Spec<Subjects.RecordStructDefaults.KeyService, Key<string, string>>
{
}

public class WhenKeyKey : KeyServiceSpec
{
    public WhenKeyKey() => When(_ => _.GetKey());

    [Fact]
    public void ThenGetsKey()
    {
        Result.A.Is().Not().NullOrEmpty();
        Specification.Is(
            """
            When _.GetKey()
            Then Result.A is not null or empty
            """);
    }
}