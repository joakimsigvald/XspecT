using Moq;

namespace XspecT.Verification;

public interface IMocking
{
    [Obsolete("Use GivenThat<TService>(_ => _.Setup... instead)")]
    Mock<TObject> TheMocked<TObject>() where TObject : class;
}