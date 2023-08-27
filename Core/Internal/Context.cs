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

    internal TValue Mention<TValue>(Action<TValue> setup, int index)
        => setup is null
        ? Retreive(typeof(TValue), index) is TValue val ? val : Mention(Create(setup), index)
        : Retreive(typeof(TValue), index) is null 
        ? Mention(Create(setup), index)
        : throw new SetupFailed($"A {index + 1}th instance of {typeof(TValue).Name} has already been created. Cannot recreate it with new setup");

    internal TValue Mention<TValue>(TValue value, int index = 0)
        => Retreive(typeof(TValue), index) is null
        ? (GetMentions(typeof(TValue))[index] = value) is TValue v ? v : throw new Exception($"Created value has unexpected type or is null: {value}")
        : throw new SetupFailed($"A {index + 1}th instance of {typeof(TValue).Name} has already been created. Cannot recreate it with new setup");

    internal TValue[] MentionMany<TValue>(Action<TValue> setup, int count)
        => Mention(Enumerable.Range(0, count).Select(i => Mention(setup, i)).ToArray());

    internal TValue[] MentionMany<TValue>(Action<TValue, int> setup, int count)
        => Mention(Enumerable.Range(0, count).Select(i => Mention<TValue>(_ => setup(_, i), i)).ToArray());

    internal TValue Create<TValue>(Action<TValue> setup)
    {
        var val = typeof(TValue).IsInterface ? _mocker.Get<TValue>() : _fixture.Create<TValue>();
        if (setup is not null)
            setup(val);
        return val;
    }

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