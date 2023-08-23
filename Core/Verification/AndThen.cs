using XspecT.Fixture;

namespace XspecT.Verification;

public class AndThen<TResult>
{
    protected readonly TestResult<TResult> Parent;
    public AndThen(TestResult<TResult> parent) => Parent = parent;
    public TestResult<TResult> And() => Parent;
    public TSpec And<TSpec>(TSpec spec) where TSpec : SpecBase<TResult> => spec;
}
