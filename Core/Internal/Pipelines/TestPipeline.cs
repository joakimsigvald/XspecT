﻿using Moq;
using System.Linq.Expressions;

namespace XspecT.Internal.Pipelines;

internal abstract class TestPipeline<TSUT, TResult, TParent> where TParent : Spec<TSUT, TResult>
{
    protected readonly TParent Parent;
    protected TestPipeline(TParent parent) => Parent = parent;
    public ITestResult<TResult> Then() => Parent.Then();
    public TSubject Then<TSubject>(TSubject subject)
        => Parent.Then(subject);

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