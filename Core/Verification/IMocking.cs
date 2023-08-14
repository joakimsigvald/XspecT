using Moq;

namespace XspecT.Verification;

public interface IMocking
{
    Mock<TObject> TheMocked<TObject>() where TObject : class;
}