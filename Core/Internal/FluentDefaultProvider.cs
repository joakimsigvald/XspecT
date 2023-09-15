using Moq;

namespace XspecT.Internal;

internal class FluentDefaultProvider : DefaultValueProvider
{
    private readonly TestDataGenerator _testDataGenerator;

    internal FluentDefaultProvider(TestDataGenerator context) => _testDataGenerator = context;

    protected override object GetDefaultValue(Type type, Mock mock)
        => _testDataGenerator.TryUse(type, out var val) ? val : GetValue(type, mock);

    private object GetValue(Type type, Mock mock)
        => IsReturningSelf(type, mock) ? mock.Object
        : IsTask(type) ? GetTask(type, mock)
        : _testDataGenerator.CreateDefaultValue(type);

    private static bool IsReturningSelf(Type type, Mock mock) => type.IsAssignableFrom(mock.Object.GetType());

    private static bool IsTask(Type type) => typeof(Task).IsAssignableFrom(type);

    private object GetTask(Type type, Mock mock)
        => type == typeof(Task) ? Task.CompletedTask : Task.FromResult(GetTaskValue(type, mock));

    private dynamic GetTaskValue(Type type, Mock mock) => GetDefaultValue(type.GenericTypeArguments.Single(), mock);
}