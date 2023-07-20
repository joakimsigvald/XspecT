using XspecT.Verification;

namespace XspecT.Fixture;

public interface ITestPipeline<TResult>
{
    ITestPipeline<TResult> Given(params Action[] arrangements);
    ITestPipeline<TResult> Given<TValue>(TValue value);
    ITestPipeline<TResult> Given<TValue1, TValue2>(TValue1 value1, TValue2 value2);
    ITestPipeline<TResult> Given<TValue1, TValue2, TValue3>(TValue1 value1, TValue2 value2, TValue3 value3);
    ITestPipeline<TResult> Using<TValue>(TValue value);
    TestResult<TResult> Then { get; }
}