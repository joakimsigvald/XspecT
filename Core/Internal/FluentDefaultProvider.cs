using Moq;

namespace XspecT.Internal;

internal class FluentDefaultProvider : DefaultValueProvider
{
    private readonly Context _context;

    internal FluentDefaultProvider(Context context) => _context = context;

    protected override object GetDefaultValue(Type type, Mock mock)
        => _context.TryUse(type, out var val) ? val : GetValue(type, mock);

    private object GetValue(Type type, Mock mock)
        => IsReturningSelf(type, mock) ? mock.Object
        : IsTask(type) ? GetTask(type, mock)
        : _context.CreateDefaultValue(type);

    private static bool IsReturningSelf(Type type, Mock mock) => type.IsAssignableFrom(mock.Object.GetType());

    private static bool IsTask(Type type) => typeof(Task).IsAssignableFrom(type);

    private object GetTask(Type type, Mock mock)
        => type == typeof(Task) ? Task.CompletedTask : Task.FromResult(GetTaskValue(type, mock));

    private dynamic GetTaskValue(Type type, Mock mock) => GetDefaultValue(type.GenericTypeArguments.Single(), mock);
}