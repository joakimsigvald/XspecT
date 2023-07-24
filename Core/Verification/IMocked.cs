using Moq;

namespace XspecT.Verification;

public interface IMocked
{
    Mock<TObject> The<TObject>() where TObject : class;
}