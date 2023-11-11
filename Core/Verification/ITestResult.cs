namespace XspecT.Verification;

public interface ITestResult<TResult>
{
    /// <summary>
    /// TODO
    /// </summary>
    TResult Result { get; }

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TError"></typeparam>
    /// <returns></returns>
    IAndThen<TResult> Throws<TError>();

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TError"></typeparam>
    /// <param name="error"></param>
    /// <returns></returns>
    IAndThen<TResult> Throws<TError>(Func<TError> error) where TError : Exception;

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TError"></typeparam>
    /// <param name="assert"></param>
    /// <returns></returns>
    IAndThen<TResult> Throws<TError>(Action<TError> assert);

    /// <summary>
    /// TODO
    /// </summary>
    /// <returns></returns>
    IAndThen<TResult> Throws();

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TError"></typeparam>
    /// <returns></returns>
    IAndThen<TResult> DoesNotThrow<TError>();

    /// <summary>
    /// TODO
    /// </summary>
    /// <returns></returns>
    IAndThen<TResult> DoesNotThrow();
}