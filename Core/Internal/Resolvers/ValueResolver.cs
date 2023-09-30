using Moq.AutoMock.Resolvers;

namespace XspecT.Internal.Resolvers;

internal class ValueResolver : IMockResolver
{
    private readonly Context _context;

    internal ValueResolver(Context context) => _context = context;

    public void Resolve(MockResolutionContext context)
    {
        if (context.RequestType.IsValueType || context.RequestType == typeof(string))
            context.Value = _context.Mention(context.RequestType);
    }
}