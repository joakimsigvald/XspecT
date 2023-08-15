using XspecT.Fixture;
using XspecT.Test.Subjects.RecordStructDefaults;
using XspecT.Verification;

namespace XspecT.Test.Tests.KeyService;

public class KeyServiceSpec : SubjectSpec<Subjects.RecordStructDefaults.KeyService, Key>
{
}

public class WhenKeyKey : KeyServiceSpec
{
    public WhenKeyKey() => When(_ => _.GetKey());
    [Fact] public void ThenGetsKey() => Result.A.Is().NotNullOrEmpty();
}