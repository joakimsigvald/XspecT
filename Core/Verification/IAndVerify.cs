using Moq;
using System.Linq.Expressions;

namespace XspecT.Verification;

public interface IAndVerify<TResult> : IAndThen<TResult>
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    public IAndVerify<TResult> And<TObject>(Expression<Action<TObject>> expression) where TObject : class;

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <param name="expression"></param>
    /// <param name="times"></param>
    /// <returns></returns>
    public IAndVerify<TResult> And<TObject>(Expression<Action<TObject>> expression, Times times) where TObject : class;

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <param name="expression"></param>
    /// <param name="times"></param>
    /// <returns></returns>
    public IAndVerify<TResult> And<TObject>(Expression<Action<TObject>> expression, Func<Times> times) where TObject : class;

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    public IAndVerify<TResult> And<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression) where TObject : class;

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="expression"></param>
    /// <param name="times"></param>
    /// <returns></returns>
    public IAndVerify<TResult> And<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression, Times times)
        where TObject : class;

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="expression"></param>
    /// <param name="times"></param>
    /// <returns></returns>
    public IAndVerify<TResult> And<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression, Func<Times> times)
        where TObject : class;
}