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
                Expression<Func<TSUT, Task<TResult>>> fun => ExecuteFunctionAsync(fun.Compile()),
                Expression<Func<TSUT, Task>> fun => ExecuteCommandAsync(fun.Compile()),
                Expression<Func<TSUT, TResult>> fun => ExecuteFunction(fun.Compile()),
                Expression<Action<TSUT>> command => ExecuteCommand(command.Compile()),
                _ => throw new SetupFailed("Failed to run method under test, unexpected signature")
            };
        }
        catch (ArgumentException ex) when (ex.Message.Contains(cue))
        {
            throw new SetupFailed($"Failed to run method under test, because an instance of {ex.Message.Split(cue)[0].Trim()} could not be provided.", ex);
        }

        bool ExecuteCommand(Action<TSUT> command)
        {
            command(sut);
            return false;
        }

        bool ExecuteFunction(Func<TSUT, TResult> function)
        {
            _result = function(sut);
            return true;
        }

        bool ExecuteCommandAsync(Func<TSUT, Task> function)
        {
            AsyncHelper.Execute(() => function(sut));
            return false;
        }

        bool ExecuteFunctionAsync(Func<TSUT, Task<TResult>> function)
        {
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