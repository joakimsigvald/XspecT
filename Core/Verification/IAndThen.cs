namespace XspecT.Verification;

public interface IAndThen<TResult>
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <returns></returns>
    ITestResult<TResult> And();

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TSubject"></typeparam>
    /// <param name="subject"></param>
    /// <returns></returns>
    TSubject And<TSubject>(TSubject subject);
}