namespace XspecT.Continuations;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TSUT"></typeparam>
/// <typeparam name="TResult"></typeparam>
public interface ITestResultWithSUT<TSUT, TResult> : ITestResult<TResult> 
{ 
    /// <summary>
    /// 
    /// </summary>
    TSUT SubjectUnderTest { get; }
}