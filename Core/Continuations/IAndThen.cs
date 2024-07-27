namespace XspecT.Continuations;

/// <summary>
/// A continuation object to apply additional assertions to a test-run
/// </summary>
public interface IAndThen<TResult>
{
    /// <summary>
    /// Provides the result of the test-run, to apply additional assertions
    /// </summary>
    /// <returns></returns>
    ITestResult<TResult> And();

    /// <summary>
    /// Provides any subject to apply additional assertions on
    /// </summary>
    /// <typeparam name="TSubject"></typeparam>
    /// <param name="subject"></param>
    /// <returns></returns>
    TSubject And<TSubject>(TSubject subject);
}