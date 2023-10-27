﻿namespace XspecT.Fixture.Pipelines;

public interface ISubjectTestPipeline<TSUT, TResult> : ITestPipeline<TResult>
    where TSUT : class
{
    IGivenContinuation<TSUT, TResult, TService> Given<TService>() where TService : class;
    IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Action<TValue> setup) where TValue : class;
    IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Func<TValue> value);
    IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(TValue value);
    IUsingSubjectTestPipeline<TSUT, TResult> Using<TValue>(Func<TValue> value);
    IUsingSubjectTestPipeline<TSUT, TResult> Using<TValue>(TValue value);
    ISubjectTestPipeline<TSUT, TResult> When(Action<TSUT> act);
    ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, TResult> act);
    ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task> action);
    ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task<TResult>> func);
}