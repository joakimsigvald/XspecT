using XspecT.Internal.TestData;
using XspecT.Internal.Verification;

namespace XspecT.Internal.Pipelines;

internal record Command(Delegate Invocation, string Expression); 

internal class SpecActor<TSUT, TResult>
{
    private readonly Stack<Command> _setUp = new();
    private Command _act;
    private readonly Stack<Command> _tearDown = new();
    private Exception _error;
    private TResult _result;

    internal void When(Command act)
    {
        if (_act is not null)
            throw new SetupFailed("Cannot call When twice in the same pipeline");
        _act = act;
    }

    internal void After(Command setUp) => _setUp.Push(setUp);

    internal void Before(Command tearDown) => _tearDown.Push(tearDown);

    internal TestResult<TResult> Execute(TSUT sut, Context context)
    {
        if (_act is null)
            throw new SetupFailed("When must be called before Then");
        AddToSpecification();
        bool hasResult = false;
        try
        {
            while (_setUp.TryPop(out var setup)) Invoke(setup, sut);
            hasResult = GetResult(sut);
        }
        catch (SetupFailed)
        {
            throw;
        }
        catch (Exception ex)
        {
            _error = ex;
        }
        finally
        {
            while (_tearDown.TryPop(out var tearDown)) Invoke(tearDown, sut);
        }
        return new(_result, _error, context, hasResult);
    }

    private void AddToSpecification() 
    {
        Specification.AddWhen(_act.Expression);
        foreach (var setUp in _setUp.Reverse())
            Specification.AddAfter(setUp.Expression);
        foreach (var tearDown in _tearDown)
            Specification.AddBefore(tearDown.Expression);
    }

    private bool GetResult(TSUT sut)
    {
        const string cue = "could not resolve to an object. (Parameter 'serviceType')";
        try
        {
            var hasResult = _act.Invocation switch
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

    private static string Invoke(Command command, TSUT sut)
    {
        switch (command.Invocation)
        {
            case Action<TSUT> act:
                act(sut);
                break;
            case Func<TSUT, Task> actAsync:
                AsyncHelper.Execute(() => actAsync(sut));
                break;
            default: throw new NotImplementedException();
        };
        return command.Expression;
    }
}