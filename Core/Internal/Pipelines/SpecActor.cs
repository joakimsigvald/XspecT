using XspecT.Internal.TestData;
using XspecT.Internal.Verification;

namespace XspecT.Internal.Pipelines;

internal class SpecActor<TResult>
{
    private readonly Stack<Action> _setUp = new();
    private Action _command;
    private Func<TResult> _function;
    private readonly Stack<Action> _tearDown = new();
    private Exception _error;
    private TResult _result;

    internal void When(Action command)
    {
        if (_command is not null || _function is not null)
            throw new SetupFailed("Cannot call When twice in the same pipeline");
        _command = command;
    }

    internal void When(Func<TResult> function)
    {
        if (_command is not null || _function is not null)
            throw new SetupFailed("Cannot call When twice in the same pipeline");
        _function = function;
    }

    internal void After(Action setUp) => _setUp.Push(setUp);

    internal void Before(Action tearDown) => _tearDown.Push(tearDown);

    internal TestResult<TResult> Execute(Context context)
    {
        while(_setUp.TryPop(out var setup)) setup();
        try
        {
            CatchError(_command ?? GetResult);
            return new(_result, _error, context, _command is null);
        }
        finally
        {
            while (_tearDown.TryPop(out var tearDown)) tearDown();
        }
    }

    private void GetResult()
    {
        if (_function is null)
            throw new SetupFailed("When must be called before Then");
        const string cue = "could not resolve to an object. (Parameter 'serviceType')";
        try
        {
            _result = _function();
            var st = Environment.StackTrace;
            return;
        }
        catch (ArgumentException ex) when (ex.Message.Contains(cue))
        {
            throw new SetupFailed($"Failed to run method under test, because an instance of {ex.Message.Split(cue)[0].Trim()} could not be provided.", ex);
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