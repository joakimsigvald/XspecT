using XspecT.Fixture.Exceptions;
using XspecT.Fixture.Pipelines;

using static XspecT.Internal.AsyncHelper;

namespace XspecT.Fixture;

public abstract class StaticSpec<TResult> : SpecBase<TResult>
{
    private object _arguments;

    public IStaticTestPipeline<TValue, TResult> Given<TValue>(TValue value)
    {
        SetArguments(value);
        return new StaticTestPipeline<TValue, TResult>(this);
    }

    public IStaticTestPipeline<TValue1, TValue2, TResult> Given<TValue1, TValue2>(TValue1 value1, TValue2 value2)
    {
        SetArguments((value1, value2));
        return new StaticTestPipeline<TValue1, TValue2, TResult>(this);
    }

    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> Given<TValue1, TValue2, TValue3>(
        TValue1 value1, TValue2 value2, TValue3 value3)
    {
        SetArguments((value1, value2, value3));
        return new StaticTestPipeline<TValue1, TValue2, TValue3, TResult>(this);
    }

    public IStaticTestPipeline<TValue, TResult> When<TValue>(Action<TValue> act)
    {
        SetAction(() =>
        {
            var arg = _arguments is TValue val ? val : default;
            act(arg);
        });
        return new StaticTestPipeline<TValue, TResult>(this);
    }

    public IStaticTestPipeline<TValue1, TValue2, TResult> When<TValue1, TValue2>(Action<TValue1, TValue2> act)
    {
        SetAction(() =>
        {
            var (arg1, arg2) = _arguments is ValueTuple<TValue1, TValue2> t ? t : default;
            act(arg1, arg2);
        });
        return new StaticTestPipeline<TValue1, TValue2, TResult>(this);
    }

    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When<TValue1, TValue2, TValue3>(
        Action<TValue1, TValue2, TValue3> act)
    {
        SetAction(() =>
        {
            var (arg1, arg2, arg3) = _arguments is ValueTuple<TValue1, TValue2, TValue3> t ? t : default;
            act(arg1, arg2, arg3);
        });
        return new StaticTestPipeline<TValue1, TValue2, TValue3, TResult>(this);
    }

    public IStaticTestPipeline<TValue, TResult> When<TValue>(Func<TValue, TResult> act)
    {
        SetAction(() =>
        {
            var arg = _arguments is TValue val ? val : default;
            return act(arg);
        });
        return new StaticTestPipeline<TValue, TResult>(this);
    }

    public IStaticTestPipeline<TValue1, TValue2, TResult> When<TValue1, TValue2>(Func<TValue1, TValue2, TResult> act)
    {
        SetAction(() =>
        {
            var (arg1, arg2) = _arguments is ValueTuple<TValue1, TValue2> t ? t : default;
            return act(arg1, arg2);
        });
        return new StaticTestPipeline<TValue1, TValue2, TResult>(this);
    }

    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When<TValue1, TValue2, TValue3>(
        Func<TValue1, TValue2, TValue3, TResult> act)
    {
        SetAction(() =>
        {
            var (arg1, arg2, arg3) = _arguments is ValueTuple<TValue1, TValue2, TValue3> t ? t : default;
            return act(arg1, arg2, arg3);
        });
        return new StaticTestPipeline<TValue1, TValue2, TValue3, TResult>(this);
    }

    public IStaticTestPipeline<TValue, TResult> When<TValue>(Func<TValue, Task> action)
        => When<TValue>(v => Execute(() => action(v)));
    public IStaticTestPipeline<TValue1, TValue2, TResult> When<TValue1, TValue2>(Func<TValue1, TValue2, Task> action)
        => When<TValue1, TValue2>((v1, v2) => Execute(() => action(v1, v2)));
    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When<TValue1, TValue2, TValue3>(
        Func<TValue1, TValue2, TValue3, Task> action)
        => When<TValue1, TValue2, TValue3>((v1, v2, v3) => Execute(() => action(v1, v2, v3)));

    public IStaticTestPipeline<TValue, TResult> When<TValue>(Func<TValue, Task<TResult>> func)
    => When<TValue>(v => Execute(() => func(v)));
    public IStaticTestPipeline<TValue1, TValue2, TResult> When<TValue1, TValue2>(Func<TValue1, TValue2, Task<TResult>> func)
        => When<TValue1, TValue2>((v1, v2) => Execute(() => func(v1, v2)));
    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When<TValue1, TValue2, TValue3>(
        Func<TValue1, TValue2, TValue3, Task<TResult>> func)
        => When<TValue1, TValue2, TValue3>((v1, v2, v3) => Execute(() => func(v1, v2, v3)));

    private void SetArguments(object args)
    {
        if (HasRun)
            throw new SetupFailed("Given must be called before Then");
        if (_arguments is not null)
            throw new SetupFailed("Can only supply method arguments once");
        _arguments = args;
    }
}