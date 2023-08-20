using AutoFixture;
using AutoFixture.Kernel;
using Moq.AutoMock;
using XspecT.Fixture.Exceptions;

namespace XspecT.Internal;

internal class Context
{
    private readonly IDictionary<Type, object> _usings = new Dictionary<Type, object>();
    private readonly IDictionary<Type, object[]> _mentions = new Dictionary<Type, object[]>();

    private readonly AutoMocker _mocker;
    private readonly IFixture _fixture = CreateAutoFixture();

    public Context(AutoMocker mocker) => _mocker = mocker;

    internal bool TryGetValue(Type type, out object val) => _usings.TryGetValue(type, out val);

    internal void Use(Type type, object value)
    {
        _usings[type] = value;
        _mocker.Use(type, value);
    }

    internal TValue Mention<TValue>(int index = 0)
        => Retreive(typeof(TValue), index) is TValue val ? val : Mention(Create<TValue>(), index);

    internal TValue Mention<TValue>(TValue value, int index = 0)
        => (GetMentions(typeof(TValue))[index] = value) is TValue v ? v : default;

    internal TValue Create<TValue>()
        => typeof(TValue).IsInterface ? _mocker.Get<TValue>() : _fixture.Create<TValue>();

    internal object CreateDefaultValue(Type type)
    {
        try
        {
            return _fixture.Create(type, new SpecimenContext(_fixture));
        }
        catch (Exception ex)
        {
            try
            {
                return Activator.CreateInstance(type);
            }
            catch
            {
                throw new SetupFailed($"Failed to create value for type {type.Name}", ex);
            }
        }
    }

    private object[] GetMentions(Type type)
        => _mentions.TryGetValue(type, out var val) ? val : _mentions[type] = new object[5];

    private object Retreive(Type type, int index = 0)
        => _mentions.TryGetValue(type, out var vals) ? vals[index]
        : index == 0 && _usings.TryGetValue(type, out var val) ? val
        : null;

    private static IFixture CreateAutoFixture()
    {
        AutoFixture.Fixture fixture = new() { RepeatCount = 0 };
        var customization = new SupportMutableValueTypesCustomization();
        customization.Customize(fixture);
        return fixture;
    }
}