﻿using Moq;

namespace XspecT.Internal.TestData;

internal class FluentDefaultProvider : DefaultValueProvider
{
    private readonly Context _context;

    internal FluentDefaultProvider(Context context) => _context = context;

    protected override object GetDefaultValue(Type type, Mock mock)
    {
        var (val, found) = _context.Use(type);
        return found ? val
            : IsReturningSelf(type, mock) ? mock.Object
            : IsTask(type) ? GetTask(type, mock)
            : _context.Create(type);
    }

    private static bool IsReturningSelf(Type type, Mock mock)
        => !type.IsAssignableFrom(typeof(object)) && type.IsAssignableFrom(mock.Object.GetType());

    private static bool IsTask(Type type) => typeof(Task).IsAssignableFrom(type);

    private Task GetTask(Type type, Mock mock)
        => type == typeof(Task) ? Task.CompletedTask : Task.FromResult(GetTaskValue(type, mock));

    private dynamic GetTaskValue(Type type, Mock mock) => GetDefaultValue(type.GenericTypeArguments.Single(), mock);
}