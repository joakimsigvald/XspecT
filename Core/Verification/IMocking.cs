using Moq;

namespace XspecT.Verification;

public interface IMocking
{
    Mock<TObject> _<TObject>() where TObject : class;
}