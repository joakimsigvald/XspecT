using Moq;

namespace XspecT.Internal.TestData;

internal class Context
{
    private readonly Dictionary<Type, object> _defaultValues = [];
    private readonly Dictionary<Type, Dictionary<int, object>> _numberedMentions = [];
    private readonly Dictionary<Type, Dictionary<string, object>> _labeledMentions = [];
    private readonly TestDataGenerator _testDataGenerator;

    public Context() => _testDataGenerator = new(this);

    internal TValue CreateInstance<TValue>() where TValue : class
        => _testDataGenerator.CreateInstance<TValue>();

    internal TValue Mention<TValue>(int index, Action<TValue> setup = null, bool asDefault = false)
        => setup is null ? Produce<TValue>(index, asDefault) : ApplyTo(setup, Produce<TValue>(index));

    internal object Mention(Type type, int index = 0)
    {
        var (val, found) = Retreive(type, index);
        return found ? val : Mention(type, Create(type), index);
    }

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

    internal TValue Mention<TValue>(TValue value, int index = 0, bool asDefault = false)
    {
        if (asDefault)
            Use(value);
        Mention(typeof(TValue), value, index);
        return value;
    }

    internal TValue[] MentionMany<TValue>(int count, int? minCount)
    {
        var (val, found) = Retreive(typeof(TValue[]));
        return found && val is TValue[] arr
            ? Reuse(arr, count, minCount)
            : MentionMany((Action<TValue>)null, count);
    }

    internal TValue[] MentionMany<TValue>(Action<TValue> setup, int count)
        => Mention(Enumerable.Range(0, count).Select(i => Mention(i, setup)).ToArray());

    internal TValue[] MentionMany<TValue>(Action<TValue, int> setup, int count)
        => Mention(Enumerable.Range(0, count).Select(i => Mention<TValue>(i, _ => setup(_, i))).ToArray());

    internal TValue Create<TValue>() => _testDataGenerator.Create<TValue>();
    internal object Create(Type type) => _testDataGenerator.Create(type);

    internal (object val, bool found) Retreive(Type type, int index = 0)
    {
        var typeMap = _numberedMentions.TryGetValue(type, out var map) ? map : null;
        return typeMap is null
            ? (null, false)
            : typeMap.TryGetValue(index, out var val) ? (val, true) : (null, false);
    }

    internal (object val, bool found) Use(Type type)
        => _defaultValues.TryGetValue(type, out var value) ? (value, true) : (null, false);

    internal Mock<TObject> GetMock<TObject>() where TObject : class => _testDataGenerator.GetMock<TObject>();

    private Dictionary<string, object> ProduceMentions(Type type)
        => _labeledMentions.TryGetValue(type, out var mentions) ? mentions : _labeledMentions[type] = [];

    private TValue Produce<TValue>(int index, bool asDefault = false)
    {
        var (val, found) = Retreive(typeof(TValue), index);
        return (TValue)(found ? val : Mention(Create<TValue>(), index, asDefault));
    }

    private void Use<TService>(TService service)
    {
        if (service is Moq.Internals.InterfaceProxy)
            return;

        _testDataGenerator.Use(typeof(TService), service);
        _defaultValues[typeof(TService)] = service;
        if (typeof(Task).IsAssignableFrom(typeof(TService)))
            return;

        Use(Task.FromResult(service));
    }

    private object Mention(Type type, object value, int index = 0) => GetMentions(type)[index] = value;

    private TValue[] Reuse<TValue>(TValue[] arr, int count, int? minCount)
        => arr.Length >= minCount || arr.Length == count ? arr
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

    private Dictionary<int, object> GetMentions(Type type)
        => _numberedMentions.TryGetValue(type, out var val) ? val : _numberedMentions[type] = [];
}