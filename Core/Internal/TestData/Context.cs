using Moq;
using System.Text;

namespace XspecT.Internal.TestData;

internal class Context
{
    [ThreadStatic]
    private static StringBuilder _specificationBuilder;

    internal static void AddPhrase(string phrase) 
        => _specificationBuilder.Append(phrase);

    public Context() => _specificationBuilder = new();

    /// <summary>
    /// 
    /// </summary>
    internal static string Specification => _specificationBuilder?.ToString().TrimStart() ?? string.Empty;

    private readonly DataProvider _dataProvider = new();

    internal TSUT CreateSUT<TSUT>()
    {
        var sutType = typeof(TSUT);
        return sutType.IsClass && sutType != typeof(string)
            ? _dataProvider.Instantiate<TSUT>()
            : Create<TSUT>();
    }

    internal TValue Mention<TValue>(int index, Action<TValue> setup)
        => ApplyTo(setup, Mention<TValue>(index));

    internal TValue Mention<TValue>(int index, Func<TValue, TValue> setup)
        => (TValue)Mention(typeof(TValue), setup.Invoke(Mention<TValue>(index)), index);

    internal TValue Mention<TValue>(int index)
    {
        var (val, found) = _dataProvider.Retrieve(typeof(TValue), index);
        return (TValue)(found ? val : Mention(Create<TValue>(), index));
    }

    internal static TValue ApplyTo<TValue>(Action<TValue> setup, TValue value)
    {
        setup.Invoke(value);
        return value;
    }

    internal void SetDefault<TModel>(Action<TModel> setup) where TModel : class
        => _dataProvider.AddDefaultSetup(
            typeof(TModel),
            obj =>
            {
                if (obj is TModel model)
                    setup(model);
                return obj;
            });

    internal void SetDefault<TValue>(Func<TValue, TValue> setup)
        => _dataProvider.AddDefaultSetup(typeof(TValue), _ => setup((TValue)_));

    internal TValue Mention<TValue>(TValue value, int index = 0)
    {
        Mention(typeof(TValue), value, index);
        return value;
    }

    internal TValue[] MentionMany<TValue>(int count, int? minCount)
    {
        var (val, found) = _dataProvider.Retrieve(typeof(TValue[]));
        return found && val is TValue[] arr
            ? Mention(Reuse(arr, count, minCount))
            : MentionMany<TValue>(count);
    }

    internal TValue[] MentionMany<TValue>(Action<TValue> setup, int count)
        => Mention(Enumerable.Range(0, count).Select(i => Mention(i, setup)).ToArray());

    internal TValue[] MentionMany<TValue>(Action<TValue, int> setup, int count)
        => Mention(Enumerable.Range(0, count).Select(i => Mention<TValue>(i, _ => setup(_, i))).ToArray());

    internal TValue Create<TValue>() => _dataProvider.Create<TValue>();

    internal Mock<TObject> GetMock<TObject>() where TObject : class 
        => _dataProvider.GetMock<TObject>();

    internal void Use<TService>(TService service, ApplyTo applyTo) => _dataProvider.Use(service, applyTo);

    internal void SetupThrows<TService>(Func<Exception> ex)
        => _dataProvider.SetDefaultException(typeof(TService), ex);

    private TValue[] MentionMany<TValue>(int count)
        => count == 0 ? Mention(Array.Empty<TValue>()) 
        : Mention(Enumerable.Range(0, count).Select(i => Mention<TValue>(i)).ToArray());

    private object Mention(Type type, object value, int index = 0)
        => _dataProvider.GetMentions(type)[index] = value;

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
}