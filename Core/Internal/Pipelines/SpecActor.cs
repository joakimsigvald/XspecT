using System.Runtime.CompilerServices;
using XspecT.Internal.TestData;
using XspecT.Internal.Verification;

namespace XspecT.Internal.Pipelines;

internal record Command(Delegate Invocation, string Expression);

internal class SpecActor<TSUT, TResult>
{
    private readonly List<Command> _setUp = [];
    private Command _methodUnderTest;
    private Lazy<TSUT> _sut;
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
    internal void TearDown()
    {
        foreach (var tearDown in _tearDown)
            Invoke(tearDown, _sut);
    }

    internal TestResult<TResult> Execute(Lazy<TSUT> sut, Context context)
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
            _sut = sut;
        }
        return new(_result, _error, context, hasResult);
    }

    private void AddToSpecification()
    {
        SpecificationGenerator.AddWhen(_methodUnderTest.Expression);
        foreach (var setUp in _setUp.Reverse<Command>())
            SpecificationGenerator.AddAfter(setUp.Expression);
        foreach (var tearDown in _tearDown)
            SpecificationGenerator.AddBefore(tearDown.Expression);
        SpecificationGenerator.AddThen();
    }

    private bool GetResult(Lazy<TSUT> sut)
    {
        const string cue = "could not resolve to an object. (Parameter 'serviceType')";
        try
        {
            (_result, var hasResult) = Invoke(_methodUnderTest, sut);
            return hasResult;
        }
        catch (ArgumentException ex) when (ex.Message.Contains(cue))
        {
            throw new SetupFailed($"Failed to run method under test, because an instance of {ex.Message.Split(cue)[0].Trim()} could not be provided.", ex);
        }
    }

    private static (TResult result, bool hasResult) Invoke(Command command, Lazy<TSUT> sut, [CallerArgumentExpression(nameof(command))] string commandName = null)
    {
        return command.Invocation switch
        {
            Func<TSUT, Task<TResult>> queryAsync => (AsyncHelper.Execute(() => queryAsync(sut.Value)), true),
            Func<Task<TResult>> queryAsync => (AsyncHelper.Execute(() => queryAsync()), true),
            Func<TSUT, Task> actAsync => ActOnSubjectAndReturnAsync(actAsync),
            Func<Task> actAsync => ActAndReturnAsync(actAsync),
            Func<TSUT, TResult> query => (query(sut.Value), true),
            Func<TResult> query => (query(), true),
            Action<TSUT> act => ActOnSubjectAndReturn(act),
            Action act => ActAndReturn(act),
            _ => throw new SetupFailed($"Failed to run {commandName}, unexpected signature")
        };

        (TResult, bool) ActAndReturn(Action act)
        {
            act();
            return (default, false);
        }

        (TResult, bool) ActOnSubjectAndReturn(Action<TSUT> act)
        {
            act(sut.Value);
            return (default, false);
        }

        (TResult, bool) ActAndReturnAsync(Func<Task> actAsync)
        {
            AsyncHelper.Execute(() => actAsync());
            return (default, false);
        }

        (TResult, bool) ActOnSubjectAndReturnAsync(Func<TSUT, Task> actAsync)
        {
            AsyncHelper.Execute(() => actAsync(sut.Value));
            return (default, false);
        }
    }
}