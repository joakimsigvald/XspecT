using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using XspecT.Fixture.Exceptions;
using XspecT.Fixture.Pipelines;

namespace XspecT.Fixture;

public abstract class SubjectSpec<TSUT, TResult> : SpecBase<TResult>, ISubjectTestPipeline<TSUT, TResult>
    where TSUT : class
{
    private readonly List<Action> _arrangements = new();
    private TSUT _sut;

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> When(Action<TSUT> act)
    {
        SetAction(() => act(_sut));
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, TResult> act)
    {
        SetAction(() => act(_sut));
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task> action)
    {
        SetAction(() => action(_sut));
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task<TResult>> func)
    {
        SetAction(() => func(_sut));
        return this;
    }

    /// <summary>
    /// Provide arrangement to the test-pipeline that will be executed before the test-method, in reversed chronological order 
    /// (allowing arrangement added later to be used in arrangement added earlier)
    /// </summary>
    /// <param name="arrangement"></param>
    /// <returns></returns>
    /// <exception cref="SetupFailed"></exception>
    public ISubjectTestPipeline<TSUT, TResult> GivenThat(Action arrangement)
    {
        if (HasRun)
            throw new SetupFailed("Given must be called before Then");
        _arrangements.Insert(0, arrangement);
        return this;
    }

    /// <summary>
    /// Provide service to the test-pipeline that can be used in auto-mocking
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="service"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> Using<TService>([DisallowNull] Func<TService> service)
        => Using(() => Use(service()));

    /// <summary>
    /// Provide service to the test-pipeline that can be used in auto-mocking
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="service"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> Using<TService>(TService service)
        => Using(() => Use(service));

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
        => Using(() => Use(service1), () => Use(service2));

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
        => Using(() => Use(service1), () => Use(service2), () => Use(service3));

    /// <summary>
    /// Provide service to the test-pipeline that can be used in auto-mocking
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="mockedService"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> Using<TService>(Mock<TService> mockedService)
        where TService : class
        => Using(() => Use(mockedService));

    /// <summary>
    /// Provide service to the test-pipeline that can be used in auto-mocking
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> Using<TService>(Expression<Func<TService, bool>> setup)
        where TService : class
        => Using(() => Use(setup));

    private ISubjectTestPipeline<TSUT, TResult> Using(params Action[] usings)
    {
        if (HasRun)
            throw new SetupFailed("Use must be called before Then");
        foreach (var use in usings)
            _arrangements.Add(use);
        return this;
    }

    /// <summary>
    /// Convenience method for supplying setup (typically specifiing behaviour of mocks 
    /// that will be used and or verified during the test execution)
    /// Will be called during pipeline execution right after the last arrangement
    /// </summary>
    protected virtual void Setup() { }

    /// <summary>
    /// Not intended to call
    /// </summary>
    protected internal override sealed void Arrange()
    {
        foreach (var arrange in _arrangements) arrange();
        Setup();
        _sut = CreateInstance<TSUT>();
    }
}