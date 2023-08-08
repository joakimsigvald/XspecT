using XspecT.Fixture.Exceptions;
using XspecT.Verification;
namespace XspecT.Internal;

internal class SpecActor<TResult>
{
    private Action _command;
    private Func<TResult> _function;
    private Exception _error;
    private TResult _result;

    internal void When(Action command, Func<TResult> function)
    {
        if (_command != null || _function != null)
            throw new SetupFailed("When may only be called once");
        if (command is null == function is null)
            throw new SetupFailed("Either Command or Function must be supplied, but not both");
        _command = command;
        _function = function;
    }

    internal TestResult<TResult> Execute<TSpec>(TSpec spec)
        where TSpec : IMocking
    {
        CatchError(_command ?? GetResult);
        return new(_result, _error, spec);
    }

    private void GetResult() => _result = _function();

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