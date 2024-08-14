using XspecT.Internal.TestData;
using XspecT.Internal.Verification;

namespace XspecT.Internal.Pipelines;

internal class SpecActor<TSUT, TResult>
{
    private string _setUpExpr;
    private readonly Stack<Action> _setUp = new();
    private Delegate _act;
    private string _actExpr;
    private string _tearDownExpr;
    private readonly Stack<Action> _tearDown = new();
    private Exception _error;
    private TResult _result;

    internal void When(Delegate act, string actExpr)
    {
        if (_act is not null)
            throw new SetupFailed("Cannot call When twice in the same pipeline");
        _act = act;
        _actExpr = actExpr;
    }

    internal void After(Action setUp, string setUpExpr)
    {
        _setUp.Push(setUp);
        _setUpExpr = setUpExpr;
    }

    internal void Before(Action tearDown, string tearDownExpr)
    {
        _tearDown.Push(tearDown);
        _tearDownExpr = tearDownExpr;
    }

    internal TestResult<TResult> Execute(TSUT sut, Context context)
    {
        while(_setUp.TryPop(out var setup)) setup();
        try
        {
            bool hasResult = false;
            CatchError(() => hasResult = GetResult(sut));
            return new(_result, _error, context, hasResult);
        }
        finally
        {
            while (_tearDown.TryPop(out var tearDown)) tearDown();
        }
    }

    private bool GetResult(TSUT sut)
    {
        if (_act is null)
            throw new SetupFailed("When must be called before Then");
        const string cue = "could not resolve to an object. (Parameter 'serviceType')";
        try
        {
            Specification.AddWhen(_actExpr);
            if (_setUpExpr is not null)
                Specification.AddAfter(_setUpExpr);
            if (_tearDownExpr is not null)
                Specification.AddBefore(_tearDownExpr);
            var hasResult = _act switch
            {
                Func<TSUT, Task<TResult>> act => ExecuteFunctionAsync(act),
                Func<TSUT, Task> act => ExecuteCommandAsync(act),
                Func<TSUT, TResult> act => ExecuteFunction(act),
                Action<TSUT> act => ExecuteCommand(act),
                _ => throw new SetupFailed("Failed to run method under test, unexpected signature")
            };
            Specification.AddThen();
            return hasResult;
        }
        catch (ArgumentException ex) when (ex.Message.Contains(cue))
        {
            throw new SetupFailed($"Failed to run method under test, because an instance of {ex.Message.Split(cue)[0].Trim()} could not be provided.", ex);
        }

        bool ExecuteCommand(Action<TSUT> act)
        {
            act(sut);
            return false;
        }

        bool ExecuteFunction(Func<TSUT, TResult> act)
        {
            _result = act(sut);
            return true;
        }

        bool ExecuteCommandAsync(Func<TSUT, Task> act)
        {
            AsyncHelper.Execute(() => act(sut));
            return false;
        }

        bool ExecuteFunctionAsync(Func<TSUT, Task<TResult>> act)
        {
            _result = AsyncHelper.Execute(() => act(sut));
            return true;
        }
    }

    private void CatchError(Action act)
    {
        try
        {
            act();
        }
        catch (SetupFailed)
        {
            throw;
        }
        catch (Exception ex)
        {
            _error = ex;
        }
    }
}