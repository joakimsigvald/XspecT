using Moq;

namespace XspecT.Internal.TestData;

internal class FluentDefaultProvider : DefaultValueProvider
{
    private readonly DataProvider _context;

    internal FluentDefaultProvider(DataProvider context) => _context = context;

    protected override object GetDefaultValue(Type type, Mock mock)
    {
        var (val, found) = _context.Use(type);
        return found ? val
            : IsReturningSelf(type, mock) ? mock.Object
            : IsTask(type) ? GetTask(type, mock)
            : _context.Create(type);
    }

    private static bool IsReturningSelf(Type type, Mock mock)
        => !type.IsAssignableFrom(typeof(object)) && type.IsAssignableFrom(mock.Object.GetType());

    private static bool IsTask(Type type) => typeof(Task).IsAssignableFrom(type);

    private Task GetTask(Type type, Mock mock)
        => type == typeof(Task)
        ? Task.CompletedTask
        : GetTaskOf(type.GenericTypeArguments.Single(), mock);

    private Task GetTaskOf(Type valueType, Mock mock)
    {
        dynamic value = GetDefaultValue(valueType, mock);
        if (value is IMocked) 
        {
            var mockName = mock.Object.GetType().Name[..^5];
            throw new SetupFailed(
                @$"{mockName} returns a Task<{valueType.Name}>. 
Interface types returned as task must be provided explicitly in the test setup.
You can provide a default interface instance with 'Given<{mockName}>().Returns(A<{valueType.Name}>)'.");
        }
        return Task.FromResult(value);
    }
}