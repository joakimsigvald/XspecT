using XspecT.Fixture;

namespace XspecT.Verification;

public interface IAndThen<TResult>
{
    ITestResult<TResult> And();
    TSpec And<TSpec>(TSpec spec) where TSpec : SpecBase<TResult>;
}