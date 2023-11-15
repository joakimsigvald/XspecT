using XspecT.Internal.TestData;
using XspecT.Internal.Verification;

namespace XspecT.Internal.Pipelines;

internal class SpecActor<TResult>
{
    private Action _command;
    private Func<TResult> _function;
    private Exception _error;
    private TResult _result;

    internal void When(Action command) => _command = command;
    internal void When(Func<TResult> function) => _function = function;

    internal TestResult<TResult> Execute(Context context)
    {
        CatchError(_command ?? GetResult);
        return new(_result, _error, context, _command is null);
    }

    private void GetResult()
    {
        const string cue = "could not resolve to an object. (Parameter 'serviceType')";
        try
        {
            _result = _function();
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