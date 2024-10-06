﻿using XspecT.Internal;
using XspecT.Internal.Pipelines;

namespace XspecT;

/// <summary>
/// Base-class for specifying and executing a set of test cases for a specific method-under-test, with no subject-under-test and no return type
/// </summary>
public abstract class Spec : Spec<object, object>
{
}

/// <summary>
/// Base-class for specifying and executing a set of test cases for a specific method-under-test.
/// This base class should typically be used for static methods, but can also be used to specify subject-under-test but no return type
/// (or when subject-under-test and the return value has the same type)
/// </summary>
/// <typeparam name="TSUTorResult">The return type of the method-under-test is also passed as argument to the test method</typeparam>
public abstract class Spec<TSUTorResult> : Spec<TSUTorResult, TSUTorResult>
{
}

/// <summary>
/// Base-class for specifying and executing a set of test cases for a specific method-under-test
/// </summary>
/// <typeparam name="TSUT">The class to instantiate and execute the method-under-test on</typeparam>
/// <typeparam name="TResult">The return type of the method-under-test</typeparam>
public abstract partial class Spec<TSUT, TResult> : ITestPipeline<TSUT, TResult>, IDisposable
{
    private readonly Pipeline<TSUT, TResult> _pipeline;
    private bool _hasFixture;

    /// <summary>
    /// 
    /// </summary>
    protected Spec()
    {
        SpecificationGenerator.Clear();
        _pipeline = new();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fixture"></param>
    protected Spec(Spec<TSUT, TResult> fixture)
    {
        var fixtureSpec = fixture.Specification;
        SpecificationGenerator.Clear();
        SpecificationGenerator.Init(fixtureSpec);
        _pipeline = fixture._pipeline.AsFixture();
        _hasFixture = true;
    }

    /// <summary>
    /// This property returns the specification of the test, after the test has been run. 
    /// The specification will also be included in the message of the exception if the test fails.
    /// </summary>
    public string Specification => _lazySpecification.Value;
    private readonly Lazy<string> _lazySpecification = new(() => SpecificationGenerator.Builder.Specification);

    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Dispose()
    {
        if (_hasFixture)
            return;
        _pipeline.Dispose();
    }
}