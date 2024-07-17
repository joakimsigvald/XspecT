using System.Linq.Expressions;
using XspecT.Internal.TestData;
using XspecT.Internal.Verification;

namespace XspecT.Internal.Pipelines;

internal class SpecActor<TSUT, TResult>
{
    private readonly Stack<Action> _setUp = new();
    private Expression _act;
    private readonly Stack<Action> _tearDown = new();
    private Exception _error;
    private TResult _result;

    internal void When(Expression act)
    {
        if (_act is not null)
            throw new SetupFailed("Cannot call When twice in the same pipeline");
        _act = act;
    }

    internal void After(Action setUp) => _setUp.Push(setUp);

    internal void Before(Action tearDown) => _tearDown.Push(tearDown);

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
            return _act switch
            {
                Expression<Func<TSUT, Task<TResult>>> act => ExecuteFunctionAsync(act),
                Expression<Func<TSUT, Task>> act => ExecuteCommandAsync(act),
                Expression<Func<TSUT, TResult>> act => ExecuteFunction(act),
                Expression<Action<TSUT>> act => ExecuteCommand(act),
                _ => throw new SetupFailed("Failed to run method under test, unexpected signature")
            };
        }
        catch (ArgumentException ex) when (ex.Message.Contains(cue))
        {
            throw new SetupFailed($"Failed to run method under test, because an instance of {ex.Message.Split(cue)[0].Trim()} could not be provided.", ex);
        }

        bool ExecuteCommand(Expression<Action<TSUT>> act)
        {
            var function = act.Compile();
            function(sut);
            return false;
        }

        bool ExecuteFunction(Expression<Func<TSUT, TResult>> act)
        {
            Context.AddPhrase($"when {act.GetName()}");
            var function = act.Compile();
            _result = function(sut);
            return true;
        }

        bool ExecuteCommandAsync(Expression<Func<TSUT, Task>> act)
        {
            var function = act.Compile();
            AsyncHelper.Execute(() => function(sut));
            return false;
        }

        bool ExecuteFunctionAsync(Expression<Func<TSUT, Task<TResult>>> act)
        {
            var function = act.Compile();
            _result = AsyncHelper.Execute(() => function(sut));
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