using AutoFixture;
using AutoFixture.Kernel;
using Moq.AutoMock;
using XspecT.Fixture.Exceptions;

namespace XspecT.Internal;

internal class Context
{
    private const int _maxValueCount = 5;

    private readonly IDictionary<Type, object> _usings = new Dictionary<Type, object>();
    private readonly IDictionary<Type, object[]> _mentions = new Dictionary<Type, object[]>();
    private readonly IDictionary<Type, IDictionary<string, object>> _labeledMentions
        = new Dictionary<Type, IDictionary<string, object>>();

    private readonly AutoMocker _mocker;
    private readonly IFixture _fixture = CreateAutoFixture();

    public Context(AutoMocker mocker) => _mocker = mocker;

    internal bool TryUse(Type type, out object val) => _usings.TryGetValue(type, out val);

    internal void Use(Type type, object value)
    {
        _usings[type] = value;
        _mocker.Use(type, value);
    }

    internal TValue Mention<TValue>(int index, Action<TValue> setup = null) 
        => setup is null ? Produce<TValue>(index) : ApplyTo(setup, Produce<TValue>(index));

    private TValue Produce<TValue>(int index)
        => (TValue)(Retreive(typeof(TValue), index) ?? Mention(Create<TValue>(), index));

    internal static TValue ApplyTo<TValue>(Action<TValue> setup, TValue value)
    {
        setup?.Invoke(value);
        return value;
    }

    internal TValue Mention<TValue>(string label)
    {
        var mentions = ProduceMentions(typeof(TValue));
        return mentions.TryGetValue(label, out var val)
            ? (TValue)val
            : (TValue)(mentions[label] = Create<TValue>());
    }

    internal IDictionary<string, object> ProduceMentions(Type type)
        => _labeledMentions.TryGetValue(type, out var mentions)
        ? mentions
        : _labeledMentions[type] = new Dictionary<string, object>();

    internal TValue Mention<TValue>(TValue value, int index = 0)
        => Retreive(typeof(TValue), index) is null
        ? (GetMentions(typeof(TValue))[index] = value) is TValue v ? v : throw new Exception($"Created value has unexpected type or is null: {value}")
        : throw new SetupFailed($"A {index + 1}th instance of {typeof(TValue).Name} has already been created. Cannot recreate it with new setup");

    internal TValue[] MentionMany<TValue>(int count)
        => Retreive(typeof(TValue[])) is TValue[] arr
        ? Reuse(arr, count)
        : MentionMany((Action<TValue>)null, count);

    private TValue[] Reuse<TValue>(TValue[] arr, int count)
        => arr.Length == count ? arr
        : arr.Length > count ? arr[..count]
        : Extend(arr, count);

    private TValue[] Extend<TValue>(TValue[] arr, int count)
    {
        var oldLen = arr.Length;
        var newArr = new TValue[count];
        Array.Copy(arr, newArr, oldLen);
        for (var i = oldLen; i < count; i++)
            newArr[i] = Mention<TValue>(i);
        return newArr;
    }

    internal TValue[] MentionMany<TValue>(Action<TValue> setup, int count)
        => Mention(Enumerable.Range(0, count).Select(i => Mention(i, setup)).ToArray());

    internal TValue[] MentionMany<TValue>(Action<TValue, int> setup, int count)
        => Mention(Enumerable.Range(0, count).Select(i => Mention<TValue>(i, _ => setup(_, i))).ToArray());

    internal TValue Create<TValue>()
        => typeof(TValue).IsInterface
        ? _mocker.Get<TValue>()
        : _fixture.Create<TValue>();

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
        => _mentions.TryGetValue(type, out var val) ? val : _mentions[type] = new object[_maxValueCount];

    private object Retreive(Type type, int index = 0)
        => _mentions.TryGetValue(type, out var vals) ? vals[index]
        : index == 0 && TryUse(type, out var val) ? val
        : null;

    private static IFixture CreateAutoFixture()
    {
        AutoFixture.Fixture fixture = new() { RepeatCount = 0 };
        var customization = new SupportMutableValueTypesCustomization();
        customization.Customize(fixture);
        return fixture;
    }
}