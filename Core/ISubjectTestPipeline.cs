using XspecT.Internal.Pipelines;

namespace XspecT;

/// <summary>
/// TODO
/// </summary>
/// <typeparam name="TSUT"></typeparam>
/// <typeparam name="TResult"></typeparam>
public interface ISubjectTestPipeline<TSUT, TResult> : ITestPipeline<TResult>
    where TSUT : class
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    ISubjectTestPipeline<TSUT, TResult> When(Action<TSUT> act);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, TResult> act);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task> action);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task<TResult>> func);
    /// <summary>
    /// TODO
    /// </summary>
    /// <returns></returns>
    IGivenContinuation<TSUT, TResult> Given();

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    IGivenContinuation<TSUT, TResult, TService> Given<TService>() where TService : class;

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Action<TValue> setup) where TValue : class;

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Func<TValue> value);

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(TValue value);

}