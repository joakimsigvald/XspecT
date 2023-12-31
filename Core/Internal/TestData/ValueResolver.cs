﻿using Moq.AutoMock.Resolvers;

namespace XspecT.Internal.TestData;

internal class ValueResolver : IMockResolver
{
    private readonly Context _context;

    internal ValueResolver(Context context) => _context = context;

    public void Resolve(MockResolutionContext context)
    {
        if (context.RequestType.IsValueType || context.RequestType == typeof(string))
            context.Value = GetValue(context.RequestType);
    }

    private object GetValue(Type type)
    {
        var (val, found) = _context.Use(type);
        return found ? val : _context.Create(type);
    }
}