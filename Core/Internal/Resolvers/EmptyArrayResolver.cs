using Moq.AutoMock.Resolvers;

namespace XspecT.Internal.Resolvers;

internal class EmptyArrayResolver : IMockResolver
{
    public void Resolve(MockResolutionContext context)
    {
        if (!context.RequestType.IsArray || context.RequestType == typeof(string))
            return;
        context.Value = CreateEmptyArray(context.RequestType);
    }

    private static object CreateEmptyArray(Type type)
    {
        Type elmType = type.GetElementType()
            ?? throw new InvalidOperationException($"Could not determine element type for '{type}'");
        return Array.CreateInstance(elmType, new int[type.GetArrayRank()]);
    }
}