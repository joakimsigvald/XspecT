using static XspecT.Test.Helper;
namespace XspecT.Test.AutoMock;

public class WhenSubjectIsString : Spec<string>
{
    [Fact]
    public void ThenUseDefaultString()
    {
        Given("abc").When(_ => _).Then().Result.Is("abc");
        VerifyDescription(
            """
            Given "abc"
            When _ => _
            Then Result is "abc"
            """);
    }
}