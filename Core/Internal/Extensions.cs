using Moq;
using System.Reflection;
namespace XspecT.Internal;

internal static class Extensions
{
    internal static Type GetMockedType(this Mock mock)
    {
        var mockType = mock.GetType();
        var mockedTypeProperty = mockType.GetProperty("MockedType", BindingFlags.NonPublic | BindingFlags.Instance);
        return mockedTypeProperty.GetValue(mock) as Type;
    }
}