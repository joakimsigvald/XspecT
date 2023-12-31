﻿using Moq;
using System.Linq.Expressions;

namespace XspecT;

/// <summary>
/// TODO
/// </summary>
/// <typeparam name="TResult"></typeparam>
public interface IPipeline<TResult>
{
    /// <summary>
    /// Run the test-pipeline and return the result
    /// </summary>
    /// <returns></returns>
    ITestResult<TResult> Then();

    /// <summary>
    /// Run the test-pipeline and verify mock invocation.
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IAndVerify<TResult> Then<TService>(Expression<Action<TService>> expression) where TService : class;

    /// <summary>
    /// Run the test-pipeline and verify mock invocation.
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="expression"></param>
    /// <param name="times"></param>
    /// <returns></returns>
    IAndVerify<TResult> Then<TService>(Expression<Action<TService>> expression, Times times) where TService : class;

    /// <summary>
    /// Run the test-pipeline and verify mock invocation.
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="expression"></param>
    /// <param name="times"></param>
    /// <returns></returns>
    IAndVerify<TResult> Then<TService>(Expression<Action<TService>> expression, Func<Times> times) where TService : class;

    /// <summary>
    /// Run the test-pipeline and verify mock invocation.
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IAndVerify<TResult> Then<TService, TReturns>(Expression<Func<TService, TReturns>> expression) where TService : class;

    /// <summary>
    /// Run the test-pipeline and verify mock invocation.
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="expression"></param>
    /// <param name="times"></param>
    /// <returns></returns>
    IAndVerify<TResult> Then<TService, TReturns>(Expression<Func<TService, TReturns>> expression, Times times)
        where TService : class;

    /// <summary>
    /// Run the test-pipeline and verify mock invocation.
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="expression"></param>
    /// <param name="times"></param>
    /// <returns></returns>
    IAndVerify<TResult> Then<TService, TReturns>(Expression<Func<TService, TReturns>> expression, Func<Times> times)
        where TService : class;
}