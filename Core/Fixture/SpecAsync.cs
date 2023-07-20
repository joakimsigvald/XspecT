namespace XspecT.Fixture;

/// <summary>
/// Not intended for direct override. Override either TestStaticAsync or TestSubjectAsync instead
/// </summary>
public abstract class SpecAsync<TResult> : SpecBase<TResult>
{
    protected ITestPipeline<TResult> When(Func<Task> action) => When(() => Execute(action));
    protected ITestPipeline<TResult> When(Func<Task<TResult>> func) => When(() => Execute(func));
    public override sealed void Dispose() => Execute(TearDown);
    protected virtual Task TearDown() => Task.CompletedTask;

    private static readonly TaskFactory _taskFactory = new
        (CancellationToken.None,
        TaskCreationOptions.None,
        TaskContinuationOptions.None,
        TaskScheduler.Default);

    private static void Execute(Func<Task> action)
        => _taskFactory.StartNew(action).Unwrap().GetAwaiter().GetResult();

    private static TResult Execute(Func<Task<TResult>> func)
        => _taskFactory.StartNew(func).Unwrap().GetAwaiter().GetResult();
}