using System.Runtime.CompilerServices;
using XspecT.Internal.TestData;
using XspecT.Internal.Verification;

namespace XspecT.Internal.Pipelines;

internal record Command(Delegate Invocation, string Expression);

internal class SpecActor<TSUT, TResult>
{
    private readonly List<Command> _setUp = [];
    private Command _methodUnderTest;
    private readonly List<Command> _tearDown = [];
    private Exception _error;
    private TResult _result;

    internal void When(Command act)
    {
        if (_methodUnderTest is not null)
            throw new SetupFailed("Cannot call When twice in the same pipeline");
        _methodUnderTest = act;
    }

    internal void After(Command setUp) => _setUp.Insert(0, setUp);

    internal void Before(Command tearDown) => _tearDown.Add(tearDown);

    internal TestResult<TResult> Execute(TSUT sut, Context context)
    {
        if (_methodUnderTest is null)
            throw new SetupFailed("When must be called before Then");
        AddToSpecification();
        bool hasResult = false;
        try
        {
            foreach (var setUp in _setUp)
                Invoke(setUp, sut);
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
            foreach (var tearDown in _tearDown)
                Invoke(tearDown, sut);
        }
        return new(_result, _error, context, hasResult);
    }

    private void AddToSpecification()
    {
        Specification.AddWhen(_methodUnderTest.Expression);
        foreach (var setUp in _setUp.Reverse<Command>())
            Specification.AddAfter(setUp.Expression);
        foreach (var tearDown in _tearDown)
            Specification.AddBefore(tearDown.Expression);
    }

    private bool GetResult(TSUT sut)
    {
        const string cue = "could not resolve to an object. (Parameter 'serviceType')";
        try
        {
            (_result, var hasResult) = Invoke(_methodUnderTest, sut);
            Specification.AddThen();
            return hasResult;
        }
        catch (ArgumentException ex) when (ex.Message.Contains(cue))
        {
            throw new SetupFailed($"Failed to run method under test, because an instance of {ex.Message.Split(cue)[0].Trim()} could not be provided.", ex);
        }
    }

    private static (TResult result, bool hasResult) Invoke(Command command, TSUT sut, [CallerArgumentExpression(nameof(command))] string commandName = null)
    {
        return command.Invocation switch
        {
            Func<TSUT, Task<TResult>> queryAsync => (AsyncHelper.Execute(() => queryAsync(sut)), true),
            Func<TSUT, Task> actAsync => ActAndReturnAsync(actAsync),
            Func<TSUT, TResult> query => (query(sut), true),
            Action<TSUT> act => ActAndReturn(act),
            _ => throw new SetupFailed($"Failed to run {commandName}, unexpected signature")
        };

        (TResult, bool) ActAndReturn(Action<TSUT> act)
        {
            act(sut);
            return (default, false);
        }

        (TResult, bool) ActAndReturnAsync(Func<TSUT, Task> actAsync)
        {
            AsyncHelper.Execute(() => actAsync(sut));
            return (default, false);
        }
    }
}