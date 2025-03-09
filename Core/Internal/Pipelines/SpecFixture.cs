using System.Runtime.CompilerServices;
using XspecT.Internal.Specification;

namespace XspecT.Internal.Pipelines;

internal class SpecFixture<TSUT> : IDisposable
{
    private bool _disposed;
    private Lazy<TSUT> _sut;
    private readonly List<Command> _setUp = [];
    private readonly List<Command> _tearDown = [];

    ~SpecFixture() => Dispose(false);

    internal void After(Command setUp) => _setUp.Insert(0, setUp);
    internal void Before(Command tearDown) => _tearDown.Add(tearDown);
    internal void SetUp(Lazy<TSUT> sut)
    {
        _sut = sut;
        foreach (var setUp in _setUp)
            Invoke<object>(setUp);
    }
    internal bool IsSetUp => _sut is not null;
    internal TSUT SubjectUnderTest => _sut.Value;
    internal void TearDown()
    {
        foreach (var tearDown in _tearDown)
            Invoke<object>(tearDown);
    }

    internal void AddToSpecification()
    {
        foreach (var setUp in _setUp.Reverse<Command>())
            SpecificationGenerator.AddAfter(setUp.Expression);
        foreach (var tearDown in _tearDown)
            SpecificationGenerator.AddBefore(tearDown.Expression);
        SpecificationGenerator.AddThen();
    }

    internal (TResult result, bool hasResult) Invoke<TResult>(
        Command command, [CallerArgumentExpression(nameof(command))] string? commandName = null)
    {
        var sut = _sut.Value;
        return command.Invocation switch
        {
            Func<TSUT, Task<TResult>> queryAsync => (AsyncHelper.Execute(() => queryAsync(sut)), true),
            Func<Task<TResult>> queryAsync => (AsyncHelper.Execute(() => queryAsync()), true),
            Func<TSUT, Task> actAsync => ActOnSubjectAndReturnAsync(actAsync),
            Func<Task> actAsync => ActAndReturnAsync(actAsync),
            Func<TSUT, TResult> query => (query(sut), true),
            Func<TResult> query => (query(), true),
            Action<TSUT> act => ActOnSubjectAndReturn(act),
            Action act => ActAndReturn(act),
            _ => throw new SetupFailed($"Failed to run {commandName!}, unexpected signature")
        };

        (TResult, bool) ActAndReturn(Action act)
        {
            act();
            return (default!, false);
        }

        (TResult, bool) ActOnSubjectAndReturn(Action<TSUT> act)
        {
            act(_sut.Value);
            return (default!, false);
        }

        (TResult, bool) ActAndReturnAsync(Func<Task> actAsync)
        {
            AsyncHelper.Execute(() => actAsync());
            return (default!, false);
        }

        (TResult, bool) ActOnSubjectAndReturnAsync(Func<TSUT, Task> actAsync)
        {
            AsyncHelper.Execute(() => actAsync(sut));
            return (default!, false);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Calls any teardown methods provided in the test pipeline with the method `Before`.
    /// Override this method to perform custom teardown in your test class.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    protected void Dispose(bool disposing)
    {
        if (_disposed)
            return;
        if (disposing)
            TearDown();
        _disposed = true;
    }
}