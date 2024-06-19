﻿using Moq;

namespace XspecT.Internal.TestData;

internal class DataProvider
{
    private readonly Dictionary<Type, object> _defaultValues = new();
    private readonly Dictionary<Type, Dictionary<int, object>> _numberedMentions = new();
    private readonly TestDataGenerator _testDataGenerator;
    private readonly Dictionary<Type, Func<object, object>> _defaultSetups = new();

    public DataProvider() => _testDataGenerator = new(this.CreateAutoFixture(), this.CreateAutoMocker());

    internal (object val, bool found) Retreive(Type type, int index = 0)
    {
        var typeMap = _numberedMentions.TryGetValue(type, out var map) ? map : null;
        return typeMap?.TryGetValue(index, out var val)
            ?? TryGetDefault(type, out val)
            ? (val, found: true) : (null, found: false);
    }

    internal bool TryGetDefault(Type type, out object val)
        => _defaultValues.TryGetValue(type, out val);

    internal Dictionary<int, object> GetMentions(Type type)
        => _numberedMentions.TryGetValue(type, out var val) ? val : _numberedMentions[type] = new();

    internal TValue Instantiate<TValue>()
    {
        var type = typeof(TValue);
        var instance = TryGetDefault(typeof(TValue), out var val)
            ? val
            : _testDataGenerator.Instantiate<TValue>();
        return (TValue)ApplyDefaultSetup(type, instance);
    }

    internal void Use<TService>(TService service)
    {
        _defaultValues[typeof(TService)] = service;
        if (service is Moq.Internals.InterfaceProxy)
            return;

        _testDataGenerator.Use(typeof(TService), service);
        if (typeof(Task).IsAssignableFrom(typeof(TService)))
            return;

        Use(Task.FromResult(service));
    }

    internal (object val, bool found) Use(Type type)
        => TryGetDefault(type, out var value) ? (value, true) : (null, false);

    internal void AddDefaultSetup(Type type, Func<object, object> setup)
        => _defaultSetups[type] =
        _defaultSetups.TryGetValue(type, out var previousSetup)
        ? MergeDefaultSetups(previousSetup, setup)
        : setup;

    private static Func<object, object> MergeDefaultSetups(Func<object, object> setup1, Func<object, object> setup2)
        => obj => setup2(setup1(obj));

    internal TValue Create<TValue>()
        => (TValue)ApplyDefaultSetup(typeof(TValue), _testDataGenerator.Create<TValue>());

    internal object Create(Type type) => ApplyDefaultSetup(type, _testDataGenerator.Create(type));

    internal Mock<TObject> GetMock<TObject>() where TObject : class => _testDataGenerator.GetMock<TObject>();
    internal Mock GetMock(Type type) => _testDataGenerator.GetMock(type);

    internal object ApplyDefaultSetup(Type type, object newValue)
        => _defaultSetups.TryGetValue(type, out var setup)
        ? setup(newValue)
        : newValue;
}