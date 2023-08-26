using Moq;
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

    internal TestResult<TResult> Execute(Moq.AutoMock.AutoMocker mocker)
    {
        CatchError(_command ?? GetResult);
        return new(_result, _error, mocker);
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
            throw new ExecuteMethodUnderTestFailed(ex.Message.Split(cue)[0].Trim(), ex);
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