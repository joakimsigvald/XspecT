using AutoFixture.Kernel;
using Moq;

namespace XspecT.Internal;

internal class FluentDefaultProvider : DefaultValueProvider
{
    private readonly AutoFixture.Fixture _fixture;
    private readonly IDictionary<Type, object> _usings;

    internal FluentDefaultProvider(AutoFixture.Fixture fixture, IDictionary<Type, object> usings)
    {
        _fixture = fixture;
        _usings = usings;
    }

    protected override object GetDefaultValue(Type type, Mock mock)
        => IsReturningSelf(type, mock) ? mock.Object
        : IsTask(type) ? GetTask(type)
        : ProduceDefaultValue(type);

    private bool IsReturningSelf(Type type, Mock mock) => type.IsAssignableFrom(mock.Object.GetType());

    private bool IsTask(Type type) => typeof(Task).IsAssignableFrom(type);

    private object GetTask(Type type) 
        => type == typeof(Task) ? Task.CompletedTask : Task.FromResult(GetTaskValue(type));

    private dynamic GetTaskValue(Type type) => ProduceDefaultValue(type.GenericTypeArguments.Single());

    private object ProduceDefaultValue(Type type) 
        => _usings.TryGetValue(type, out var val) ? val : _usings[type] = Create(type);

    private object Create(Type type) => _fixture.Create(type, new SpecimenContext(_fixture));
}