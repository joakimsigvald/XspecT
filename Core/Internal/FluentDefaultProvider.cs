using AutoFixture;
using AutoFixture.Kernel;
using Moq;
using XspecT.Fixture.Exceptions;

namespace XspecT.Internal;

internal class FluentDefaultProvider : DefaultValueProvider
{
    private readonly IFixture _fixture;
    private readonly IDictionary<Type, object> _usings;

    internal FluentDefaultProvider(IFixture fixture, IDictionary<Type, object> usings)
    {
        _fixture = fixture;
        _usings = usings;
    }

    protected override object GetDefaultValue(Type type, Mock mock)
        => _usings.TryGetValue(type, out var val) ? val : _usings[type] = GetValue(type, mock);

    private object GetValue(Type type, Mock mock)
        => IsReturningSelf(type, mock) ? mock.Object
        : IsTask(type) ? GetTask(type, mock)
        : Create(type);

    private static bool IsReturningSelf(Type type, Mock mock) => type.IsAssignableFrom(mock.Object.GetType());

    private static bool IsTask(Type type) => typeof(Task).IsAssignableFrom(type);

    private object GetTask(Type type, Mock mock)
        => type == typeof(Task) ? Task.CompletedTask : Task.FromResult(GetTaskValue(type, mock));

    private dynamic GetTaskValue(Type type, Mock mock) => GetDefaultValue(type.GenericTypeArguments.Single(), mock);

    private object Create(Type type)
    {
        try
        {
            return _fixture.Create(type, new SpecimenContext(_fixture));
        }
        catch (ObjectCreationException oce)
        {
            throw new SetupFailed($"Failed to provide default value for type {type.Name}", oce);
        }
    }
}