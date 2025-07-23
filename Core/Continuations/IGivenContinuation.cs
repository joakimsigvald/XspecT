﻿using System.Runtime.CompilerServices;

namespace XspecT.Continuations;

/// <summary>
/// A continuation object to apply additional arrangements to the test
/// </summary>
public interface IGivenContinuation<TSUT, TResult>
{
    /// <summary>
    /// Provide a default value, that will be applied in all mocks and auto-generated test-data, where no specific value or setup is given.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="defaultValue"></param>
    /// <param name="defaultValueExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Default<TValue>(
        Func<TValue> defaultValue,
        [CallerArgumentExpression(nameof(defaultValue))] string? defaultValueExpr = null);

    /// <summary>
    /// Provide a default value, that will be used as test data where no specific value is given
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="defaultValue"></param>
    /// <param name="defaultValueExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Default<TValue>(
        TValue defaultValue,
        [CallerArgumentExpression(nameof(defaultValue))] string? defaultValueExpr = null);

    /// <summary>
    /// Provide a value or object instance that will be used when creating subject under test
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="defaultValue"></param>
    /// <param name="defaultValueExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Using<TValue>(
        TValue defaultValue,
        [CallerArgumentExpression(nameof(defaultValue))] string? defaultValueExpr = null);

    /// <summary>
    /// Provide a tagged default value, that will be used as test data where no specific value is given
    /// The tagged value is applied lazily while running the pipeline.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="tag">Tag, which associated value will be used when auto-generating objects</param>
    /// <param name="tagExpr">Do not use, will be provided by the compiler</param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Default<TValue>(
        Tag<TValue> tag,
        [CallerArgumentExpression(nameof(tag))] string? tagExpr = null);

    /// <summary>
    /// Provide a tagged value or object instance that will be used when creating subject under test
    /// The tagged value is applied lazily while running the pipeline.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="tag">Tag, which associated value will be used when auto-generating objects</param>
    /// <param name="tagExpr">Do not use, will be provided by the compiler</param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Using<TValue>(
        Tag<TValue> tag,
        [CallerArgumentExpression(nameof(tag))] string? tagExpr = null);

    /// <summary>
    /// Provide a value or object instance that will be used when creating subject under test
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="defaultValue"></param>
    /// <param name="defaultValueExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Using<TValue>(
        Func<TValue> defaultValue, 
        [CallerArgumentExpression(nameof(defaultValue))] string? defaultValueExpr = null);

    /// <summary>
    /// Provide a default setup, that will be applied in all mocks and auto-generated test-data.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <param name="setupExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Default<TValue>(
        Action<TValue> setup,
        [CallerArgumentExpression(nameof(setup))] string? setupExpr = null)
        where TValue : class;

    /// <summary>
    /// Provide a default transform, that will be applied in all mocks and auto-generated test-data.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <param name="transformExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Default<TValue>(
        Func<TValue, TValue> transform,
        [CallerArgumentExpression(nameof(transform))] string? transformExpr = null);

    /// <summary>
    /// Ensure that all values generated of the given type are different with regards to equality.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Unique<TValue>();

    /// <summary>
    /// Provide a value of a given type, that can be mentioned in the test pipeline as A, An, The, or TheFirst.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="valueExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> A<TValue>(
        TValue value,
        [CallerArgumentExpression(nameof(value))] string? valueExpr = null);

    /// <summary>
    /// Provide setup for a value of a given type, that can be mentioned in the test pipeline as A, An, The, or TheFirst.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <param name="setupExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> A<TValue>(
        Action<TValue> setup,
        [CallerArgumentExpression(nameof(setup))] string? setupExpr = null)
        where TValue : class;

    /// <summary>
    /// Provide transform for a value of a given type, that can be mentioned in the test pipeline as A, An, The, or TheFirst.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <param name="transformExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> A<TValue>(
        Func<TValue, TValue> transform,
        [CallerArgumentExpression(nameof(transform))] string? transformExpr = null);

    /// <summary>
    /// Provide a value of a given type, that can be mentioned in the test pipeline as A, An, The, or TheFirst.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="valueExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> An<TValue>(
        TValue value,
        [CallerArgumentExpression(nameof(value))] string? valueExpr = null);

    /// <summary>
    /// Provide setup for a value of a given type, that can be mentioned in the test pipeline as A, An, The, or TheFirst.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <param name="setupExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> An<TValue>(
        Action<TValue> setup,
        [CallerArgumentExpression(nameof(setup))] string? setupExpr = null)
        where TValue : class;

    /// <summary>
    /// Provide transform for a value of a given type, that can be mentioned in the test pipeline as A, An, The, or TheFirst.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <param name="transformExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> An<TValue>(
        Func<TValue, TValue> transform,
        [CallerArgumentExpression(nameof(transform))] string? transformExpr = null);

    /// <summary>
    /// Provide a second value of a given type, that can be mentioned in the test pipeline as ASecond or TheSecond.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="valueExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> ASecond<TValue>(
        TValue value,
        [CallerArgumentExpression(nameof(value))] string? valueExpr = null);

    /// <summary>
    /// Provide setup for a second value of a given type, that can be mentioned in the test pipeline as ASecond or TheSecond.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <param name="setupExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> ASecond<TValue>(
        Action<TValue> setup,
        [CallerArgumentExpression(nameof(setup))] string? setupExpr = null)
        where TValue : class;

    /// <summary>
    /// Provide transform for a second value of a given type, that can be mentioned in the test pipeline as ASecond or TheSecond.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <param name="transformExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> ASecond<TValue>(
        Func<TValue, TValue> transform,
        [CallerArgumentExpression(nameof(transform))] string? transformExpr = null);

    /// <summary>
    /// Provide a third value of a given type, that can be mentioned in the test pipeline as AThird or TheThird.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="valueExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> AThird<TValue>(
        TValue value,
        [CallerArgumentExpression(nameof(value))] string? valueExpr = null);

    /// <summary>
    /// Provide setup for a third value of a given type, that can be mentioned in the test pipeline as AThird or TheThird.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <param name="setupExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> AThird<TValue>(
        Action<TValue> setup,
        [CallerArgumentExpression(nameof(setup))] string? setupExpr = null)
        where TValue : class;

    /// <summary>
    /// Provide transform for a third value of a given type, that can be mentioned in the test pipeline as AThird or TheThird.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <param name="transformExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> AThird<TValue>(
        Func<TValue, TValue> transform,
        [CallerArgumentExpression(nameof(transform))] string? transformExpr = null);

    /// <summary>
    /// Provide a fourth value of a given type, that can be mentioned in the test pipeline as AFourth or TheFourth.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="valueExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> AFourth<TValue>(
        TValue value,
        [CallerArgumentExpression(nameof(value))] string? valueExpr = null);

    /// <summary>
    /// Provide setup for a fourth value of a given type, that can be mentioned in the test pipeline as AFourth or TheFourth.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <param name="setupExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> AFourth<TValue>(
        Action<TValue> setup,
        [CallerArgumentExpression(nameof(setup))] string? setupExpr = null)
        where TValue : class;

    /// <summary>
    /// Provide transform for a fourth value of a given type, that can be mentioned in the test pipeline as AFourth or TheFourth.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <param name="transformExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> AFourth<TValue>(
        Func<TValue, TValue> transform,
        [CallerArgumentExpression(nameof(transform))] string? transformExpr = null);

    /// <summary>
    /// Provide a fifth value of a given type, that can be mentioned in the test pipeline as AFifth or TheFifth.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="valueExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> AFifth<TValue>(
        TValue value,
        [CallerArgumentExpression(nameof(value))] string? valueExpr = null);

    /// <summary>
    /// Provide setup for a fifth value of a given type, that can be mentioned in the test pipeline as AFifth or TheFifth.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <param name="setupExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> AFifth<TValue>(
        Action<TValue> setup,
        [CallerArgumentExpression(nameof(setup))] string? setupExpr = null)
        where TValue : class;

    /// <summary>
    /// Provide transform for a fifth value of a given type, that can be mentioned in the test pipeline as AFifth or TheFifth.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <param name="transformExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> AFifth<TValue>(
        Func<TValue, TValue> transform,
        [CallerArgumentExpression(nameof(transform))] string? transformExpr = null);

    /// <summary>
    /// Specify that referenced collections of the given type have zero elements.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Zero<TValue>();

    /// <summary>
    /// Specify that referenced collections of the given type have one element.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> One<TValue>();

    /// <summary>
    /// Specify that referenced collections of the given type have two elements.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Two<TValue>();

    /// <summary>
    /// Specify that referenced collections of the given type have three elements.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Three<TValue>();

    /// <summary>
    /// Specify that referenced collections of the given type have four elements.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Four<TValue>();

    /// <summary>
    /// Specify that referenced collections of the given type have five elements.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Five<TValue>();

    /// <summary>
    /// Specify that referenced collections of the given type have one element, unless otherwise specified, 
    /// and that the one element is the given value.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="valueExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> One<TValue>(
        TValue value,
        [CallerArgumentExpression(nameof(value))] string? valueExpr = null);

    /// <summary>
    /// Specify that referenced collections of the given type have the given elements, 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="values"></param>
    /// <param name="valueExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Some<TValue>(
        IEnumerable<TValue> values,
        [CallerArgumentExpression(nameof(values))] string? valueExpr = null);

    /// <summary>
    /// Specify that referenced collections of the given type have one element, unless otherwise specified, 
    /// and that the given setup will be applied to the one element.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <param name="setupExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> One<TValue>(
        Action<TValue> setup,
        [CallerArgumentExpression(nameof(setup))] string? setupExpr = null);

    /// <summary>
    /// Specify that referenced collections of the given type have one element, unless otherwise specified, 
    /// and that the given transform will be applied to the one element.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <param name="transformExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> One<TValue>(
        Func<TValue, TValue> transform,
        [CallerArgumentExpression(nameof(transform))] string? transformExpr = null);

    /// <summary>
    /// Specify that referenced collections of the given type have two elements, unless otherwise specified, 
    /// and that the given setup will be applied to the two elements.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <param name="setupExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Two<TValue>(
        Action<TValue, int> setup,
        [CallerArgumentExpression(nameof(setup))] string? setupExpr = null);

    /// <summary>
    /// Specify that referenced collections of the given type have two elements, unless otherwise specified, 
    /// and that the given setup will be applied to the two elements.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <param name="setupExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Two<TValue>(
        Action<TValue> setup,
        [CallerArgumentExpression(nameof(setup))] string? setupExpr = null);

    /// <summary>
    /// Specify that referenced collections of the given type have two elements, unless otherwise specified, 
    /// and that the given transform will be applied to the two elements.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <param name="transformExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Two<TValue>(
        Func<TValue, TValue> transform,
        [CallerArgumentExpression(nameof(transform))] string? transformExpr = null);

    /// <summary>
    /// Specify that referenced collections of the given type have two elements, unless otherwise specified, 
    /// and that the given transform will be applied to the two elements.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <param name="transformExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Two<TValue>(
        Func<TValue, int, TValue> transform,
        [CallerArgumentExpression(nameof(transform))] string? transformExpr = null);

    /// <summary>
    /// Specify that referenced collections of the given type have three elements, unless otherwise specified, 
    /// and that the given setup will be applied to the three elements.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <param name="setupExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Three<TValue>(
        Action<TValue, int> setup,
        [CallerArgumentExpression(nameof(setup))] string? setupExpr = null);

    /// <summary>
    /// Specify that referenced collections of the given type have three elements, unless otherwise specified, 
    /// and that the given setup will be applied to the three elements.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <param name="setupExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Three<TValue>(
        Action<TValue> setup,
        [CallerArgumentExpression(nameof(setup))] string? setupExpr = null);

    /// <summary>
    /// Specify that referenced collections of the given type have three elements, unless otherwise specified, 
    /// and that the given transform will be applied to the three elements.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <param name="transformExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Three<TValue>(
        Func<TValue, TValue> transform,
        [CallerArgumentExpression(nameof(transform))] string? transformExpr = null);

    /// <summary>
    /// Specify that referenced collections of the given type have three elements, unless otherwise specified, 
    /// and that the given transform will be applied to the three elements.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <param name="transformExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Three<TValue>(
        Func<TValue, int, TValue> transform,
        [CallerArgumentExpression(nameof(transform))] string? transformExpr = null);

    /// <summary>
    /// Specify that referenced collections of the given type have four elements, unless otherwise specified, 
    /// and that the given setup will be applied to the four elements.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <param name="setupExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Four<TValue>(
        Action<TValue, int> setup,
        [CallerArgumentExpression(nameof(setup))] string? setupExpr = null);

    /// <summary>
    /// Specify that referenced collections of the given type have four elements, unless otherwise specified, 
    /// and that the given setup will be applied to the four elements.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <param name="setupExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Four<TValue>(
        Action<TValue> setup,
        [CallerArgumentExpression(nameof(setup))] string? setupExpr = null);

    /// <summary>
    /// Specify that referenced collections of the given type have four elements, unless otherwise specified, 
    /// and that the given transform will be applied to the four elements.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <param name="transformExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Four<TValue>(
        Func<TValue, TValue> transform,
        [CallerArgumentExpression(nameof(transform))] string? transformExpr = null);

    /// <summary>
    /// Specify that referenced collections of the given type have four elements, unless otherwise specified, 
    /// and that the given transform will be applied to the four elements.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <param name="transformExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Four<TValue>(
        Func<TValue, int, TValue> transform,
        [CallerArgumentExpression(nameof(transform))] string? transformExpr = null);

    /// <summary>
    /// Specify that referenced collections of the given type have five elements, unless otherwise specified, 
    /// and that the given setup will be applied to the five elements.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <param name="setupExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Five<TValue>(
        Action<TValue, int> setup,
        [CallerArgumentExpression(nameof(setup))] string? setupExpr = null);

    /// <summary>
    /// Specify that referenced collections of the given type have five elements, unless otherwise specified, 
    /// and that the given setup will be applied to the five elements.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <param name="setupExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Five<TValue>(
        Action<TValue> setup,
        [CallerArgumentExpression(nameof(setup))] string? setupExpr = null);

    /// <summary>
    /// Specify that referenced collections of the given type have five elements, unless otherwise specified, 
    /// and that the given transform will be applied to the five elements.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <param name="transformExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Five<TValue>(
        Func<TValue, TValue> transform,
        [CallerArgumentExpression(nameof(transform))] string? transformExpr = null);

    /// <summary>
    /// Specify that referenced collections of the given type have five elements, unless otherwise specified, 
    /// and that the given transform will be applied to the five elements.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <param name="transformExpr"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Five<TValue>(
        Func<TValue, int, TValue> transform,
        [CallerArgumentExpression(nameof(transform))] string? transformExpr = null);
}