using Moq;

namespace XspecT.Internal;

internal class Context
{
    private const int _maxValueCount = 5;

    private readonly IDictionary<Type, object[]> _mentions = new Dictionary<Type, object[]>();
    private readonly IDictionary<Type, IDictionary<string, object>> _labeledMentions
        = new Dictionary<Type, IDictionary<string, object>>();
    private readonly TestDataGenerator _testDataGenerator;

    public Context() => _testDataGenerator = new(this);

    internal void Use(Type type, object value) => _testDataGenerator.Use(type, value);

    internal TValue CreateInstance<TValue>() where TValue : class
        => _testDataGenerator.CreateInstance<TValue>();

    internal TValue Mention<TValue>(int index, Action<TValue> setup = null)
        => setup is null ? Produce<TValue>(index) : ApplyTo(setup, Produce<TValue>(index));

    internal object Mention(Type type, int index = 0) 
        => Retreive(type, index) ?? Mention(type, Create(type), index);

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
        => Mention(typeof(TValue), value, index) is TValue v ? v : default;

    internal TValue[] MentionMany<TValue>(int count)
        => Retreive(typeof(TValue[])) is TValue[] arr
        ? Reuse(arr, count)
        : MentionMany((Action<TValue>)null, count);

    private object Mention(Type type, object value, int index = 0) => GetMentions(type)[index] = value;

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

    internal TValue Create<TValue>() => _testDataGenerator.Create<TValue>();
    private object Create(Type type) => _testDataGenerator.CreateDefaultValue(type);

    private object[] GetMentions(Type type)
        => _mentions.TryGetValue(type, out var val) ? val : _mentions[type] = new object[_maxValueCount];

    private object Retreive(Type type, int index = 0)
        => _mentions.TryGetValue(type, out var vals) ? vals[index]
        : index == 0 && _testDataGenerator.TryUse(type, out var val) ? val
        : null;

    internal Mock<TObject> GetMock<TObject>() where TObject : class => _testDataGenerator.GetMock<TObject>();
}