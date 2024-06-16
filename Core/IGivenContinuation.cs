using System.Linq.Expressions;

namespace XspecT;

/// <summary>
/// A continuation object to apply additional arrangements to the test-pipeline
/// </summary>
public interface IGivenContinuation<TSUT, TResult, TService>
    where TService : class
{
    /// <summary>
    /// Setup mock to return a value as default for any invocation where no specific mock-setup has been provided
    /// </summary>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> ReturnsDefault<TReturns>(Func<TReturns> value);

    /// <summary>
    /// Mock the method invocation
    /// </summary>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="expression"></param>
    /// <returns>Continuation for providing method invocation result to mock</returns>
    IGivenThatContinuation<TSUT, TResult, TService, TReturns> That<TReturns>(
        Expression<Func<TService, TReturns>> expression);

    /// <summary>
    /// Provide async method invocation to mock
    /// </summary>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="expression"></param>
    /// <returns>Continuation for providing method invocation result to mock</returns>
    IGivenThatContinuation<TSUT, TResult, TService, TReturns> That<TReturns>(
        Expression<Func<TService, Task<TReturns>>> expression);
}

/// <summary>
/// A continuation object to apply additional arrangements to the test
/// </summary>
public interface IGivenContinuation<TSUT, TResult>
{
    /// <summary>
    /// Provide a setup-action to be applied when executing the test
    /// </summary>
    /// <param name="setup"></param>
    /// <returns>The test-pipeline</returns>
    IGivenTestPipeline<TSUT, TResult> That(Action setup);

    /// <summary>
    /// Provide a default value, that will be applied in all mocks and auto-generated test-data, where no specific value or setup is given.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Default<TValue>(TValue value);

    /// <summary>
    /// Provide a default setup, that will be applied in all mocks and auto-generated test-data.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Default<TValue>(Action<TValue> setup) where TValue : class;

    /// <summary>
    /// Provide a default transform, that will be applied in all mocks and auto-generated test-data.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Default<TValue>(Func<TValue, TValue> transform);

    /// <summary>
    /// Provide a value of a given type, that can be mentioned in the test pipeline as A, An, The, or TheFirst.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> A<TValue>(TValue value);

    /// <summary>
    /// Provide setup for a value of a given type, that can be mentioned in the test pipeline as A, An, The, or TheFirst.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> A<TValue>(Action<TValue> setup) where TValue : class;

    /// <summary>
    /// Provide transform for a value of a given type, that can be mentioned in the test pipeline as A, An, The, or TheFirst.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> A<TValue>(Func<TValue, TValue> transform);

    /// <summary>
    /// Provide a value of a given type, that can be mentioned in the test pipeline as A, An, The, or TheFirst.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> An<TValue>(TValue value);

    /// <summary>
    /// Provide setup for a value of a given type, that can be mentioned in the test pipeline as A, An, The, or TheFirst.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> An<TValue>(Action<TValue> setup) where TValue : class;

    /// <summary>
    /// Provide transform for a value of a given type, that can be mentioned in the test pipeline as A, An, The, or TheFirst.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> An<TValue>(Func<TValue, TValue> transform);

    /// <summary>
    /// Provide a second value of a given type, that can be mentioned in the test pipeline as ASecond or TheSecond.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> ASecond<TValue>(TValue value);

    /// <summary>
    /// Provide setup for a second value of a given type, that can be mentioned in the test pipeline as ASecond or TheSecond.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> ASecond<TValue>(Action<TValue> setup) where TValue : class;

    /// <summary>
    /// Provide transform for a second value of a given type, that can be mentioned in the test pipeline as ASecond or TheSecond.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> ASecond<TValue>(Func<TValue, TValue> transform);

    /// <summary>
    /// Provide a third value of a given type, that can be mentioned in the test pipeline as AThird or TheThird.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> AThird<TValue>(TValue value);

    /// <summary>
    /// Provide setup for a third value of a given type, that can be mentioned in the test pipeline as AThird or TheThird.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> AThird<TValue>(Action<TValue> setup) where TValue : class;

    /// <summary>
    /// Provide transform for a third value of a given type, that can be mentioned in the test pipeline as AThird or TheThird.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> AThird<TValue>(Func<TValue, TValue> transform);

    /// <summary>
    /// Provide a fourth value of a given type, that can be mentioned in the test pipeline as AFourth or TheFourth.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> AFourth<TValue>(TValue value);

    /// <summary>
    /// Provide setup for a fourth value of a given type, that can be mentioned in the test pipeline as AFourth or TheFourth.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> AFourth<TValue>(Action<TValue> setup) where TValue : class;

    /// <summary>
    /// Provide transform for a fourth value of a given type, that can be mentioned in the test pipeline as AFourth or TheFourth.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> AFourth<TValue>(Func<TValue, TValue> transform);

    /// <summary>
    /// Provide a fifth value of a given type, that can be mentioned in the test pipeline as AFifth or TheFifth.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> AFifth<TValue>(TValue value);

    /// <summary>
    /// Provide setup for a fifth value of a given type, that can be mentioned in the test pipeline as AFifth or TheFifth.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> AFifth<TValue>(Action<TValue> setup) where TValue : class;

    /// <summary>
    /// Provide transform for a fifth value of a given type, that can be mentioned in the test pipeline as AFifth or TheFifth.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> AFifth<TValue>(Func<TValue, TValue> transform);
}