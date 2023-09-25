using Moq.AutoMock.Resolvers;

namespace XspecT.Internal.Resolvers;

internal class TupleResolver : IMockResolver
{
    private readonly TestDataGenerator _dataGenerator;

    internal TupleResolver(TestDataGenerator dataGenerator) => _dataGenerator = dataGenerator;

    public void Resolve(MockResolutionContext context)
    {
        if (context.RequestType.Name != "ValueTuple`2")
            return;
        var val = GetValue(context.RequestType);
        context.Value = val;
    }

    internal object GetValue(Type type)
        => _dataGenerator.TryUse(type, out var val) ? val : _dataGenerator.CreateDefaultValue(type);
}