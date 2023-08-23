﻿using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using XspecT.Fixture.Exceptions;
using XspecT.Fixture.Pipelines;

namespace XspecT.Fixture;

public class GivenThatSubjectTestPipeline<TSUT, TResult> 
    : TestPipeline<TResult, SubjectSpec<TSUT, TResult>>, IGivenThatSubjectTestPipeline<TSUT, TResult>
    where TSUT : class
{
    public GivenThatSubjectTestPipeline(SubjectSpec<TSUT, TResult> parent)
        : base(parent) { }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> When(Action<TSUT> act)
        => Parent.When(act);

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, TResult> act)
        => Parent.When(act);

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task> action)
        => Parent.When(action);

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task<TResult>> func)
        => Parent.When(func);

    /// <summary>
    /// Provide arrangement to the test-pipeline that will be executed before the test-method, in reversed chronological order 
    /// (allowing arrangement added later to be used in arrangement added earlier)
    /// </summary>
    /// <param name="arrangement"></param>
    /// <returns></returns>
    /// <exception cref="SetupFailed"></exception>
    public IGivenThatSubjectTestPipeline<TSUT, TResult> And(Action arrangement)
        => GivenThat(arrangement);

    /// <summary>
    /// Provide setup to the test-pipeline that will be executed before the test-method, in reversed chronological order 
    /// (allowing variables set later to be used in setup added earlier)
    /// </summary>
    /// <param name="arrangement"></param>
    /// <returns></returns>
    /// <exception cref="SetupFailed"></exception>
    public IGivenThatSubjectTestPipeline<TSUT, TResult> And<TService>(Action<Mock<TService>> setup) where TService : class
        => GivenThat(setup);

    /// <summary>
    /// Provide arrangement to the test-pipeline that will be executed before the test-method, in reversed chronological order 
    /// (allowing arrangement added later to be used in arrangement added earlier)
    /// </summary>
    /// <param name="arrangement"></param>
    /// <returns></returns>
    /// <exception cref="SetupFailed"></exception>
    public IGivenThatSubjectTestPipeline<TSUT, TResult> GivenThat(Action arrangement)
    {
        Parent.GivenThat(arrangement);
        return this;
    }

    /// <summary>
    /// Provide setup to the test-pipeline that will be executed before the test-method, in reversed chronological order 
    /// (allowing variables set later to be used in setup added earlier)
    /// </summary>
    /// <param name="arrangement"></param>
    /// <returns></returns>
    /// <exception cref="SetupFailed"></exception>
    public IGivenThatSubjectTestPipeline<TSUT, TResult> GivenThat<TService>(Action<Mock<TService>> setup) where TService : class
    {
        Parent.GivenThat(setup);
        return this;
    }

    /// <summary>
    /// Provide service to the test-pipeline that can be used in auto-mocking
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="service"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> Using<TService>([DisallowNull] Func<TService> service)
        => Parent.Using(service);

    /// <summary>
    /// Provide service to the test-pipeline that can be used in auto-mocking
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="service"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> Using<TService>(TService service)
        => Parent.Using(service);

    /// <summary>
    /// Provide services to the test-pipeline that can be used in auto-mocking
    /// </summary>
    /// <typeparam name="TService1"></typeparam>
    /// <typeparam name="TService2"></typeparam>
    /// <param name="service1"></param>
    /// <param name="service2"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> Using<TService1, TService2>(
        TService1 service1, TService2 service2)
        => Parent.Using(service1, service2);

    /// <summary>
    /// Provide services to the test-pipeline that can be used in auto-mocking
    /// </summary>
    /// <typeparam name="TService1"></typeparam>
    /// <typeparam name="TService2"></typeparam>
    /// <typeparam name="TService3"></typeparam>
    /// <param name="service1"></param>
    /// <param name="service2"></param>
    /// <param name="service3"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> Using<TService1, TService2, TService3>(
        TService1 service1, TService2 service2, TService3 service3)
        => Parent.Using(service1, service2, service3);

    /// <summary>
    /// Provide service to the test-pipeline that can be used in auto-mocking
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="mockedService"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> Using<TService>(Mock<TService> mockedService)
        where TService : class
        => Parent.Using(mockedService);

    /// <summary>
    /// Provide service to the test-pipeline that can be used in auto-mocking
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> Using<TService>(Expression<Func<TService, bool>> setup)
        where TService : class
        => Parent.Using(setup);
}