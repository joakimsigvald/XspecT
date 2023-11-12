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
    /// TODO
    /// </summary>
    /// <returns></returns>
    ITestResult<TResult> Then();

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TSubject"></typeparam>
    /// <param name="subject"></param>
    /// <returns></returns>
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