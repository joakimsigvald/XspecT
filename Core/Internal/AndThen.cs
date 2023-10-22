using XspecT.Verification;

namespace XspecT.Internal;

internal class AndThen<TResult> : IAndThen<TResult>
{
    internal protected readonly TestResult<TResult> Parent;
    internal AndThen(TestResult<TResult> parent) => Parent = parent;
    public ITestResult<TResult> And() => Parent;
    public TSubject And<TSubject>(TSubject subject) => subject;
}