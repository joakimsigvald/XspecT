namespace XspecT.Verification;

public interface ITestResult<TResult>
{
    TResult Result { get; }
    IAndThen<TResult> Throws<TError>();
    IAndThen<TResult> Throws<TError>(Func<TError> error) where TError : Exception;
    IAndThen<TResult> Throws<TError>(Action<TError> assert);
    IAndThen<TResult> Throws();
    IAndThen<TResult> DoesNotThrow<TError>();
    IAndThen<TResult> DoesNotThrow();
}