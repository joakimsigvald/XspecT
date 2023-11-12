namespace XspecT.Internal.Pipelines;

internal class StaticPipeline<TResult> : Pipeline<TResult>
{
    private object _arguments;

    internal void SetArguments(object args)
    {
        if (HasRun)
            throw new SetupFailed("Given must be called before Then");
        if (_arguments is not null)
            throw new SetupFailed("Can only supply method arguments once");
        _arguments = args;
    }

    internal void When<TValue>(Action<TValue> act)
        => SetAction(() =>
        {
            var arg = _arguments is TValue val ? val : default;
            act(arg);
        });

    internal void When<TValue1, TValue2>(Action<TValue1, TValue2> act)
        => SetAction(() =>
        {
            var (arg1, arg2) = _arguments is ValueTuple<TValue1, TValue2> t ? t : default;
            act(arg1, arg2);
        });

    internal void When<TValue1, TValue2, TValue3>(
        Action<TValue1, TValue2, TValue3> act)
        => SetAction(() =>
        {
            var (arg1, arg2, arg3) = _arguments is ValueTuple<TValue1, TValue2, TValue3> t ? t : default;
            act(arg1, arg2, arg3);
        });

    internal void When<TValue>(Func<TValue, TResult> act)
        => SetAction(() =>
        {
            var arg = _arguments is TValue val ? val : default;
            return act(arg);
        });

    internal void When<TValue1, TValue2>(Func<TValue1, TValue2, TResult> act)
        => SetAction(() =>
        {
            var (arg1, arg2) = _arguments is ValueTuple<TValue1, TValue2> t ? t : default;
            return act(arg1, arg2);
        });

    internal void When<TValue1, TValue2, TValue3>(Func<TValue1, TValue2, TValue3, TResult> act)
        => SetAction(() =>
        {
            var (arg1, arg2, arg3) = _arguments is ValueTuple<TValue1, TValue2, TValue3> t ? t : default;
            return act(arg1, arg2, arg3);
        });
}