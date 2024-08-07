﻿using Moq;
using XspecT.Internal.Pipelines;
using XspecT.Internal.TestData;

namespace XspecT;

public abstract partial class Spec<TSUT, TResult> : ITestPipeline<TSUT, TResult>
{
    /// <summary>
    /// Provide any arrangement to the test, which will be applied during test execution in reverse order of where in the test-pipleine it was provided
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    public IGivenTestPipeline<TSUT, TResult> Given<TValue>(Action<TValue> setup) where TValue : class
    {
        _pipeline.SetDefault(setup);
        return new GivenTestPipeline<TSUT, TResult>(this);
    }

    /// <summary>
    /// Transform any value and use the transformed value as default
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <returns></returns>
    public IGivenTestPipeline<TSUT, TResult> Given<TValue>(Func<TValue, TValue> transform)
    {
        _pipeline.SetDefault(transform);
        return new GivenTestPipeline<TSUT, TResult>(this);
    }

    /// <summary>
    /// Provide a default value, that will be applied in all mocks and auto-generated test-data, where no specific value or setup is given.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public IGivenTestPipeline<TSUT, TResult> Given<TValue>(TValue defaultValue)
        => Given(defaultValue, ApplyTo.All);

    /// <summary>
    /// Provide an array of default values, that will be applied in all mocks and auto-generated test-data, where no specific value or setup is given.
    /// It is also mentioned by position so the values can be retreived by A, ASecond, AThird etc.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="defaultValues"></param>
    /// <returns></returns>
    public IGivenTestPipeline<TSUT, TResult> Given<TValue>(params TValue[] defaultValues)
    {
        _pipeline.SetDefault(defaultValues, ApplyTo.All);
        defaultValues.Take(5).Select((v, i) => _pipeline.Mention(i, v)).Count();
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
    /// <param name="value"></param>
    /// <returns></returns>
    public IGivenTestPipeline<TSUT, TResult> Given<TValue>(Func<TValue> value)
        => Given(value, ApplyTo.All);

    internal IGivenTestPipeline<TSUT, TResult> Given<TValue>(TValue defaultValue, ApplyTo applyTo)
    {
        _pipeline.SetDefault(defaultValue, applyTo);
        return new GivenTestPipeline<TSUT, TResult>(this);
    }

    internal IGivenTestPipeline<TSUT, TResult> Given<TValue>(Func<TValue> value, ApplyTo applyTo)
    {
        _pipeline.PushArrangement(() => _pipeline.SetDefault(value(), applyTo));
        return new GivenTestPipeline<TSUT, TResult>(this);
    }

    internal Mock<TService> GetMock<TService>() where TService : class 
        => _pipeline.GetMock<TService>();

    internal IGivenTestPipeline<TSUT, TResult> PushArrangement(Action setup)
    {
        _pipeline.PushArrangement(setup);
        return new GivenTestPipeline<TSUT, TResult>(this);
    }

    internal IGivenTestPipeline<TSUT, TResult> AddArrangement(Action setup)
    {
        _pipeline.AddArrangement(setup);
        return new GivenTestPipeline<TSUT, TResult>(this);
    }

    internal void SetupThrows<TService>(Func<Exception> ex) => _pipeline.SetupThrows<TService>(ex);
}