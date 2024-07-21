using XspecT.Internal.TestData;
namespace XspecT.Test;

internal static class Helper
{
    internal static void VerifyDescription(string expected)
        => Xunit.Assert.Equal(expected, Specification.Description);
}