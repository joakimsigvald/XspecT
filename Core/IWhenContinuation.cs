namespace XspecT;

/// <summary>
/// TODO
/// </summary>
/// <typeparam name="TSUT"></typeparam>
/// <typeparam name="TResult"></typeparam>
public interface IWhenContinuation<TSUT, TResult> : ISubjectTestPipelineBase<TSUT, TResult>
    where TSUT : class
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IWhenContinuation<TSUT, TResult> And(Action<TSUT> act);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IWhenContinuation<TSUT, TResult> And(Func<TSUT, TResult> act);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    IWhenContinuation<TSUT, TResult> And(Func<TSUT, Task> action);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    IWhenContinuation<TSUT, TResult> And(Func<TSUT, Task<TResult>> func);
}