using Moq.AutoMock.Resolvers;

namespace XspecT.Internal.Resolvers;

internal class TupleResolver : IMockResolver
{
    private readonly Context _context;

    internal TupleResolver(Context context) => _context = context;

    public void Resolve(MockResolutionContext context)
    {
        if (context.RequestType.Name != "ValueTuple`2")
            return;
        context.Value = _context.Mention(context.RequestType);
    }
}