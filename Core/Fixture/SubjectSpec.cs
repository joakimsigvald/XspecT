using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using XspecT.Fixture.Exceptions;
using XspecT.Fixture.Pipelines;
using XspecT.Internal;

namespace XspecT.Fixture;

public abstract class SubjectSpec<TSUT, TResult> : Spec<TResult>, ISubjectTestPipeline<TSUT, TResult>
    where TSUT : class
{
    protected SubjectSpec() : base(new SubjectPipeline<TSUT, TResult>()) { }
    private SubjectPipeline<TSUT, TResult> Pipeline => (SubjectPipeline<TSUT, TResult>)_pipeline;
    protected TSUT SUT => Pipeline.SUT;

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> When(Action<TSUT> act)
    {
        Pipeline.SetAction(() => act(SUT));
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, TResult> act)
    {
        Pipeline.SetAction(() => act(SUT));
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task> action)
    {
        Pipeline.SetAction(() => action(SUT));
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task<TResult>> func)
    {
        Pipeline.SetAction(() => func(SUT));
        return this;
    }

    /// <summary>
    /// Provide arrangement to the test-pipeline that will be executed before the test-method, in reversed chronological order 
    /// (allowing arrangement added later to be used in arrangement added earlier)
    /// </summary>
    /// <param name="arrangement"></param>
    /// <returns></returns>
    /// <exception cref="SetupFailed"></exception>
    public IGivenSubjectTestPipeline<TSUT, TResult> GivenThat(Action arrangement)
    {
        Pipeline.GivenThat(arrangement);
        return new GivenSubjectTestPipeline<TSUT, TResult>(this);
    }

    public IGivenContinuation<TSUT, TResult, TService> Given<TService>() where TService : class
    {
        if (Pipeline.HasRun)
            throw new SetupFailed("Given must be called before Then");
        return new GivenContinuation<TSUT, TResult, TService>(this);
    }

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Action<TValue> setup) where TValue : class
    {
        Pipeline.GivenThat(() => A(setup));
        return new GivenSubjectTestPipeline<TSUT, TResult>(this);
    }

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Func<TValue> value)
    {
        Pipeline.GivenThat(() => A(value()));
        return new GivenSubjectTestPipeline<TSUT, TResult>(this);
    }

    /// <summary>
    /// Provide service to the test-pipeline that can be used in auto-mocking
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="service"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> Using<TService>([DisallowNull] Func<TService> service)
        => Using(() => Pipeline.Use(service()));

    /// <summary>
    /// Provide service to the test-pipeline that can be used in auto-mocking
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="service"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> Using<TService>(TService service)
        => Using(() => Pipeline.Use(service));

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
        => Using(() => Pipeline.Use(service1), () => Pipeline.Use(service2));

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
        => Using(() => Pipeline.Use(service1), () => Pipeline.Use(service2), () => Pipeline.Use(service3));

    /// <summary>
    /// Provide service to the test-pipeline that can be used in auto-mocking
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="mockedService"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> Using<TService>(Mock<TService> mockedService)
        where TService : class
        => Using(() => Pipeline.Use(mockedService));

    /// <summary>
    /// Provide service to the test-pipeline that can be used in auto-mocking
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> Using<TService>(Expression<Func<TService, bool>> setup)
        where TService : class
        => Using(() => Pipeline.Use(setup));

    internal void SetupMock<TService>(Action<Mock<TService>> setup) where TService : class
        => Pipeline.SetupMock(setup);

    private ISubjectTestPipeline<TSUT, TResult> Using(params Action[] usings)
    {
        Pipeline.Using(usings);
        return this;
    }
}