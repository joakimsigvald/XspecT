﻿using Moq;
using System.Runtime.CompilerServices;
using XspecT.Continuations;
using XspecT.Internal.Pipelines;
using XspecT.Internal.TestData;

namespace XspecT;

public abstract partial class Spec<TSUT, TResult> : ITestPipeline<TSUT, TResult>
{
    /// <summary>
    /// Provide any arrangement to the test, which will be applied during test execution in reverse order of where in the test-pipeline it was provided
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <param name="setupExpr"></param>
    /// <returns></returns>
    public IGivenTestPipeline<TSUT, TResult> Given<TValue>(
        Action<TValue> setup,
        [CallerArgumentExpression(nameof(setup))] string setupExpr = null) 
        where TValue : class
    {
        _pipeline.SetDefault(setup, setupExpr);
        return new GivenTestPipeline<TSUT, TResult>(this);
    }

    /// <summary>
    /// Transform any value and use the transformed value as default
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <param name="transformExpr"></param>
    /// <returns></returns>
    public IGivenTestPipeline<TSUT, TResult> Given<TValue>(
        Func<TValue, TValue> transform,
        [CallerArgumentExpression(nameof(transform))] string transformExpr = null)
    {
        _pipeline.SetDefault(transform, transformExpr);
        return new GivenTestPipeline<TSUT, TResult>(this);
    }

    /// <summary>
    /// Provide a default value, that will be applied in all mocks and auto-generated test-data, where no specific value or setup is given.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="defaultValue"></param>
    /// <param name="defaultValueExpr"></param>
    /// <returns></returns>
    public IGivenTestPipeline<TSUT, TResult> Given<TValue>(
        TValue defaultValue,
        [CallerArgumentExpression(nameof(defaultValue))] string defaultValueExpr = null) 
        => GivenDefault(defaultValue, ApplyTo.All, defaultValueExpr);

    /// <summary>
    /// Provide an array of default values, that will be applied in all mocks and auto-generated test-data, where no specific value or setup is given.
    /// It is also mentioned by position so the values can be retrieved by A, ASecond, AThird etc.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="defaultValues"></param>
    /// <param name="defaultValuesExpr"></param>
    /// <param name=""></param>
    /// <returns></returns>
    public IGivenTestPipeline<TSUT, TResult> Given<TValue>(
        TValue[] defaultValues,
        [CallerArgumentExpression(nameof(defaultValues))] string defaultValuesExpr = null)
    {
        _pipeline.SetDefault(defaultValues, ApplyTo.All, defaultValuesExpr);
        var mentions = defaultValues.Take(5).Select((value, i) => (i, value));
        foreach (var (i, value) in mentions)
            _pipeline.Mention(i, value);
        return new GivenTestPipeline<TSUT, TResult>(this);
    }

    /// <summary>
    /// A continuation for providing mock-setup for the given type
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    /// <exception cref="SetupFailed"></exception>
    public IGivenServiceContinuation<TSUT, TResult, TService> Given<TService>() where TService : class 
        => new GivenServiceContinuation<TSUT, TResult, TService>(this);

    /// <summary>
    /// Return continuation for providing any setup as an action
    /// </summary>
    /// <returns></returns>
    /// <exception cref="SetupFailed"></exception>
    public IGivenContinuation<TSUT, TResult> Given() 
        => new GivenContinuation<TSUT, TResult>(this);

    /// <summary>
    /// Provide a default value as a lambda, to be evaluated during test execution AFTER any subsequently added arrangement.
    /// Providing a default value as a lambda, to defer execution, is useful when the default value is created based on test data that is specified later in the test-pipeline.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="defaultValue"></param>
    /// <param name="defaultValueExpr"></param>
    /// <returns></returns>
    public IGivenTestPipeline<TSUT, TResult> Given<TValue>(
        Func<TValue> defaultValue,
        [CallerArgumentExpression(nameof(defaultValue))] string defaultValueExpr = null) 
        => GivenDefault(defaultValue, ApplyTo.All, defaultValueExpr);

    internal IGivenTestPipeline<TSUT, TResult> GivenDefault<TValue>(TValue defaultValue, ApplyTo applyTo, string defaultValueExpr)
    {
        _pipeline.SetDefault(defaultValue, applyTo, defaultValueExpr);
        return new GivenTestPipeline<TSUT, TResult>(this);
    }

    internal IGivenTestPipeline<TSUT, TResult> GivenDefault<TValue>(Func<TValue> value, ApplyTo applyTo, string defaultValueExpr)
    {
        _pipeline.ArrangeFirst(() => _pipeline.SetDefault(value(), applyTo, defaultValueExpr));
        return new GivenTestPipeline<TSUT, TResult>(this);
    }

    internal Mock<TService> GetMock<TService>() where TService : class 
        => _pipeline.GetMock<TService>();

    internal IGivenTestPipeline<TSUT, TResult> ArrangeFirst(Action setup)
    {
        _pipeline.ArrangeFirst(setup);
        return new GivenTestPipeline<TSUT, TResult>(this);
    }

    internal IGivenTestPipeline<TSUT, TResult> ArrangeLast(Action setup)
    {
        _pipeline.ArrangeLast(setup);
        return new GivenTestPipeline<TSUT, TResult>(this);
    }

    internal void SetupThrows<TService>(Func<Exception> ex) => _pipeline.SetupThrows<TService>(ex);
}