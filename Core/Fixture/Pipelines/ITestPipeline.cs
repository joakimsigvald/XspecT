using XspecT.Verification;

namespace XspecT.Fixture.Pipelines;

public interface ITestPipeline<TResult>
{
    TestResult<TResult> Then();
    TSpec Then<TSpec>(TSpec spec) where TSpec : SpecBase<TResult>;
}