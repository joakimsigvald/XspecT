using Moq;

namespace XspecT.Verification;

public interface IMocked
{
    Mock<TObject> GetMock<TObject>() where TObject : class;
}