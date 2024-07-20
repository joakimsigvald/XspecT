using XspecT.Internal.TestData;
namespace XspecT.Test;

internal static class Helper
{
    internal static void VerifyDescription(string description)
        => Xunit.Assert.Equal(Specification.Description, description);
}