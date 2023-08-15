using XspecT.Verification;

namespace XspecT.Fixture.Pipelines;

public interface ITestPipeline<TResult>
{
    TestResult<TResult> Then();
}