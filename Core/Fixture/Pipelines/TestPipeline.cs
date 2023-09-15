using Moq;
using System.Linq.Expressions;
using XspecT.Verification;

namespace XspecT.Fixture.Pipelines;

public abstract class TestPipeline<TResult, TParent> where TParent : Spec<TResult>
{
    protected readonly TParent Parent;
    protected TestPipeline(TParent parent) => Parent = parent;
    public ITestResult<TResult> Then() => Parent.Then();
    public TSpec Then<TSpec>(TSpec spec) where TSpec : Spec<TResult>
        => Parent.Then(spec);

    public IAndVerify<TResult> Then<TService>(
        Expression<Action<TService>> expression) where TService : class
        => Parent.Then(expression);

    public IAndVerify<TResult> Then<TService>(
        Expression<Action<TService>> expression, Times times) where TService : class
        => Parent.Then(expression, times);

    public IAndVerify<TResult> Then<TService>(
        Expression<Action<TService>> expression, Func<Times> times) where TService : class
        => Parent.Then(expression, times);

    public IAndVerify<TResult> Then<TService, TReturns>(
        Expression<Func<TService, TReturns>> expression) where TService : class
        => Parent.Then(expression);

    public IAndVerify<TResult> Then<TService, TReturns>(
        Expression<Func<TService, TReturns>> expression, Times times) where TService : class
        => Parent.Then(expression, times);

    public IAndVerify<TResult> Then<TService, TReturns>(
        Expression<Func<TService, TReturns>> expression, Func<Times> times) where TService : class
        => Parent.Then(expression, times);
}

public class TestPipeline<TResult> : TestPipeline<TResult, Spec<TResult>>
{
    protected TestPipeline(Spec<TResult> parent) : base(parent) { }
}