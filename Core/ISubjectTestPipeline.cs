namespace XspecT;

/// <summary>
/// A continuation to further specify the test
/// </summary>
/// <typeparam name="TSUT"></typeparam>
/// <typeparam name="TResult"></typeparam>
public interface ISubjectTestPipeline<TSUT, TResult> : ITestPipeline<TResult>
    where TSUT : class
{
    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    ISubjectTestPipeline<TSUT, TResult> When(Action<TSUT> act);

    /// <summary>
    /// Provide the method-under-test as a lambda expression
    /// </summary>
    /// <param name="act"></param>
    /// <returns>A continuation for providing further arrangement, or executing the test</returns>
    ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, TResult> act);

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task> action);

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task<TResult>> func);

    /// <summary>
    /// Provide a method to be called BEFORE the method-under-test is called as set-up
    /// </summary>
    /// <param name="setUp">the method to call as setup before executing the method-under-test</param>
    /// <returns>A continuation to provide further arrangement to the test-pipeline</returns>
    ISubjectTestPipeline<TSUT, TResult> After(Action<TSUT> setUp);

    /// <summary>
    /// Provide an async method to be called BEFORE the method-under-test is called as set-up
    /// </summary>
    /// <param name="setUp">the method to call as setup before executing the method-under-test</param>
    /// <returns>A continuation to provide further arrangement to the test-pipeline</returns>
    ISubjectTestPipeline<TSUT, TResult> After(Func<TSUT, Task> setUp);

    /// <summary>
    /// Provide a method to be called AFTER the method-under-test is called as tear-down
    /// </summary>
    /// <param name="tearDown">the method to call as teardown after executing the method-under-test</param>
    /// <returns>A continuation to provide further arrangement to the test-pipeline</returns>
    ISubjectTestPipeline<TSUT, TResult> Before(Action<TSUT> tearDown);

    /// <summary>
    /// Provide an async method to be called AFTER the method-under-test is called as tear-down
    /// </summary>
    /// <param name="tearDown">the method to call as teardown after executing the method-under-test</param>
    /// <returns>A continuation to provide further arrangement to the test-pipeline</returns>
    ISubjectTestPipeline<TSUT, TResult> Before(Func<TSUT, Task> tearDown);

    /// <summary>
    /// Provide any arrangement to the test, which will be applied during test execution in reverse order of where in the test-pipleine it was provided
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Action<TValue> setup) where TValue : class;

    /// <summary>
    /// Provide a default value, that will be applied in all mocks and auto-generated test-data, where no specific value or setup is given.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(TValue defaultValue);

    /// <summary>
    /// A continuation to provide further arrangement
    /// </summary>
    /// <returns></returns>
    IGivenContinuation<TSUT, TResult> Given();

    /// <summary>
    /// A continuation for providing mock-setup for the given type
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    IGivenContinuation<TSUT, TResult, TService> Given<TService>() where TService : class;

    /// <summary>
    /// Provide a default value as a lambda, to be evaluated during test execution AFTER any subsequently added arrangement.
    /// Providing a default value as a lambda, to defer execution, is useful when the default value is created based on test data that is specified later in the test-pipeline.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Func<TValue> value);
}