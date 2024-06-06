namespace XspecT;

/// <summary>
/// A continuation to provide further arrangement
/// </summary>
public interface IGivenSubjectTestPipeline<TSUT, TResult> : ISubjectTestPipeline<TSUT, TResult>
    where TSUT : class
{
    /// <summary>
    /// Access the mock of the given type to provide mock-setup
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns>A continuation to provide mock-setup for the given type</returns>
    IGivenContinuation<TSUT, TResult, TService> And<TService>() where TService : class;

    /// <summary>
    /// A continuation to provide further arrangement to the test
    /// </summary>
    /// <returns></returns>
    IGivenContinuation<TSUT, TResult> And();

    /// <summary>
    /// Provide any arrangement to the test, which will be applied during test execution in reverse order of where in the test-pipleine it was provided
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(Action<TValue> setup) where TValue : class;

    /// <summary>
    /// Transform any value and use the transformed value as default
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(Func<TValue, TValue> setup);

    /// <summary>
    /// Provide a default value as a lambda, to be evaluated during test execution AFTER any subsequently added arrangement.
    /// Providing a default value as a lambda, to defer execution, is useful when the default value is created based on test data that is specified later in the test-pipeline.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(Func<TValue> value);

    /// <summary>
    /// Provide a default value as a lambda, to be evaluated during test execution AFTER any subsequently added arrangement.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(TValue value);
}