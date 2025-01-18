using XspecT.Continuations;
using XspecT.Internal.Specification;

namespace XspecT.Internal.Verification;

internal class AndThen<TSUT, TResult> : IAndThen<TResult>
{
    internal protected readonly TestResult<TSUT, TResult> Parent;
    internal AndThen(TestResult<TSUT, TResult> parent) => Parent = parent;
    public ITestResult<TResult> And()
    {
        SpecificationGenerator.AddThen();
        return Parent;
    }

    public TSubject And<TSubject>(TSubject subject)
    {
        SpecificationGenerator.AddThen();
        return subject;
    }
}