using Moq;
using Moq.AutoMock;

namespace XspecT.Fixture;

public class FluentDefaultProvider : DefaultValueProvider
{
    internal AutoMocker DefaultMocker { get; init; }

    protected override object GetDefaultValue(Type type, Mock mock)
        => type.IsAssignableFrom(mock.Object.GetType())
        ? mock.Object
        : type == typeof(Task)
        ? Task.CompletedTask
        : type.IsAssignableFrom(typeof(Task)) && type.GenericTypeArguments.Length > 0
        ? Task.FromResult(DefaultMocker.Get(type.GenericTypeArguments[0]))
        : DefaultMocker.Get(type);
}