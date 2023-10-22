namespace XspecT.Verification;

public interface IAndThen<TResult>
{
    ITestResult<TResult> And();
    TSubject And<TSubject>(TSubject subject);
}