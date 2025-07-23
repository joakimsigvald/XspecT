﻿using Moq;
using XspecT.Internal.Specification;

namespace XspecT.Internal.TestData;

internal class Context
{
    private readonly DataProvider _dataProvider = new();
    private readonly Dictionary<Type, Dictionary<object, int>> _tagIndices = [];
    private readonly Dictionary<Type, HashSet<object?>> _generatedValues = [];

    internal TSUT CreateSUT<TSUT>()
    {
        var sutType = typeof(TSUT);
        return sutType.IsClass && sutType != typeof(string)
            ? _dataProvider.Instantiate<TSUT>()
            : Create<TSUT>();
    }

    internal TValue Apply<TValue>(Action<TValue> setup, int index)
        => Produce<TValue>(index, (v, i) =>
        {
            setup(v);
            return v;
        });

    internal TValue Apply<TValue>(Func<TValue, TValue> transform, int index)
        => Produce<TValue>(index, (v, i) => transform(v));

    internal TValue Apply<TValue>(int index, Func<TValue, int, TValue> transform)
        => Produce(index, transform);

    internal TValue Produce<TValue>(int? index, Func<TValue, int, TValue>? transform = null)
    {
        if (index is null)
            return ApplyUniqueConstraint(Create<TValue>);

        var (val, found) = _dataProvider.Retrieve(typeof(TValue), index.Value);
        return found && transform is null
            ? (TValue)val!
            : Assign(ApplyUniqueConstraint(Get), index.Value);

        TValue Get()
        {
            var newValue = found
                ? (TValue)val!
                : _dataProvider.TryGetDefault(typeof(TValue), out var defaultValue)
                ? (TValue)defaultValue!
                : Create<TValue>();
            return transform is null ? newValue : transform(newValue, index.Value);
        }
    }

    internal TValue Produce<TValue>(Tag<TValue> tag) => Produce<TValue>(GetTagIndex(tag));

    internal TValue Assign<TValue>(Tag<TValue> tag, TValue value)
        => Assign(value, GetTagIndex(tag));

    internal TValue Apply<TValue>(Tag<TValue> tag, Action<TValue> setup)
        => Apply(setup, GetTagIndex(tag));

    internal TValue Apply<TValue>(Tag<TValue> tag, Func<TValue, TValue> transform)
        => Apply(transform, GetTagIndex(tag));

    internal Dictionary<object, int> GetTagIndices(Type type)
        => _tagIndices.TryGetValue(type, out var val) ? val : _tagIndices[type] = [];

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
        => _dataProvider.AddDefaultSetup(typeof(TValue), _ => setup((TValue)_)!);

    internal TValue Assign<TValue>(TValue value, int index = 0)
    {
        Assign(typeof(TValue), value, index);
        return value;
    }

    internal TValue[] AssignMany<TValue>(TValue[] values)
        => Assign(values);

    internal TValue[] MentionMany<TValue>(int count, int? minCount)
    {
        var (val, found) = _dataProvider.Retrieve(typeof(TValue[]));
        return found && val is TValue[] arr
            ? Assign(Reuse(arr, count, minCount))
            : MentionMany<TValue>(count);
    }

    internal TValue[] ApplyMany<TValue>(Action<TValue> setup, int count)
        => Assign(Enumerable.Range(0, count).Select(i => Apply(setup, i)).ToArray());

    internal TValue[] ApplyMany<TValue>(Action<TValue, int> setup, int count)
        => Assign(Enumerable.Range(0, count).Select(i => Apply<TValue>(_ => setup(_, i), i)).ToArray());

    internal TValue[] ApplyMany<TValue>(Func<TValue, TValue> transform, int count)
        => Assign(Enumerable.Range(0, count).Select(i => Apply(transform, i)).ToArray());

    internal TValue[] ApplyMany<TValue>(Func<TValue, int, TValue> transform, int count)
        => Assign(Enumerable.Range(0, count).Select(i => Apply(i, transform)).ToArray());

    internal TValue Create<TValue>() => _dataProvider.Create<TValue>();

    internal Mock<TObject> GetMock<TObject>() where TObject : class
        => _dataProvider.GetMock<TObject>();

    internal void Use<TService>(TService service, ApplyTo applyTo) => _dataProvider.Use(service, applyTo);

    internal void SetupThrows<TService>(Func<Exception> ex)
        => _dataProvider.SetDefaultException(typeof(TService), ex);

    internal void SetUnique<TValue>()
    {
        if (!_generatedValues.ContainsKey(typeof(TValue)))
            _generatedValues[typeof(TValue)] = [];
    }

    private TValue ApplyUniqueConstraint<TValue>(Func<TValue> generateValue)
    {
        var type = typeof(TValue);
        if (!_generatedValues.TryGetValue(type, out var generated))
            return generateValue();

        const int attempts = 10;
        for (var i = 0; i < attempts; i++)
        {
            var value = generateValue();
            if (generated.Add(value))
                return value;
        }
        throw new SetupFailed($"Failed to find a unique value of {type.Alias()} after {attempts} attempts");
    }

    private int GetTagIndex<TValue>(Tag<TValue> tag)
    {
        var typedTagIndices = GetTagIndices(typeof(TValue));
        return typedTagIndices.TryGetValue(tag, out var index)
            ? index
            : typedTagIndices[tag] = GetNextTagIndex(typedTagIndices);
    }

    private static int GetNextTagIndex(Dictionary<object, int> typedTagIndices)
        => typedTagIndices.Count > 0 ? typedTagIndices.Values.Min() - 1 : -1;

    private TValue[] MentionMany<TValue>(int count)
        => count == 0 ? Assign(Array.Empty<TValue>())
        : Assign(Enumerable.Range(0, count)
            .Select(i => Produce<TValue>(i))
            .ToArray());

    private object? Assign(Type type, object? value, int index = 0)
        => _dataProvider.GetMentions(type)[index] = value;

    private TValue[] Reuse<TValue>(TValue[] arr, int count, int? minCount)
        => arr.Length >= minCount || arr.Length == count ? arr
        : arr.Length > count ? arr[..count]
        : Extend(arr, count);

    private TValue[] Extend<TValue>(TValue[] arr, int count)
        => [
            .. arr, 
            .. Enumerable.Range(arr.Length, count - arr.Length).Select(i => Produce<TValue>(i))];
}