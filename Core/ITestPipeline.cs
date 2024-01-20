using Moq;
using System.Linq.Expressions;

namespace XspecT;

/// <summary>
/// TODO
/// </summary>
/// <typeparam name="TResult"></typeparam>
public interface ITestPipeline<TResult>
{
    /// <summary>
    /// Runs the test and provides the result. When the test is run, any provided arrengement will be applied in reverse order, then the subject-under-test will be created and the method-under-test called
    /// </summary>
    /// <returns>The test result, containing any return values or exceptions thrown, upon which assertions can be made</returns>
    ITestResult<TResult> Then();

    /// <summary>
    /// Runs the test and provides the result. When the test is run, any provided arrengement will be applied in reverse order, then the subject-under-test will be created and the method-under-test called
    /// </summary>
    /// <typeparam name="TSubject"></typeparam>
    /// <param name="subject"></param>
    /// <returns>The provided argument is returned, allowing assertions on the provided arguments to be chained</returns>
    TSubject Then<TSubject>(TSubject subject);

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IAndVerify<TResult> Then<TService>(Expression<Action<TService>> expression)
        where TService : class;

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="expression"></param>
    /// <param name="times"></param>
    /// <returns></returns>
    IAndVerify<TResult> Then<TService>(Expression<Action<TService>> expression, Times times)
        where TService : class;

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="expression"></param>
    /// <param name="times"></param>
    /// <returns></returns>
    IAndVerify<TResult> Then<TService>(Expression<Action<TService>> expression, Func<Times> times)
        where TService : class;

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IAndVerify<TResult> Then<TService, TReturns>(Expression<Func<TService, TReturns>> expression)
        where TService : class;

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="expression"></param>
    /// <param name="times"></param>
    /// <returns></returns>
    IAndVerify<TResult> Then<TService, TReturns>(Expression<Func<TService, TReturns>> expression, Times times)
        where TService : class;

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="expression"></param>
    /// <param name="times"></param>
    /// <returns></returns>
    IAndVerify<TResult> Then<TService, TReturns>(Expression<Func<TService, TReturns>> expression, Func<Times> times)
        where TService : class;
}