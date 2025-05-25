﻿using XspecT.Continuations;
using XspecT.Internal.Specification;

namespace XspecT.Internal.Verification;

internal class AndThen<TSUT, TResult> : IAndThen<TResult>
{
    internal protected readonly TestResult<TSUT, TResult> Parent;
    internal AndThen(TestResult<TSUT, TResult> parent) => Parent = parent;

    /// <summary>
    /// Continuation to make additional assertions on the result
    /// </summary>
    /// <returns></returns>
    public ITestResult<TResult> And()
    {
        SpecificationGenerator.AddThen();
        return Parent;
    }

    /// <summary>
    /// Continuation to make additional assertions on the subject
    /// </summary>
    /// <typeparam name="TSubject"></typeparam>
    /// <param name="subject"></param>
    /// <returns></returns>
    public TSubject And<TSubject>(TSubject subject)
    {
        SpecificationGenerator.AddThen();
        return subject;
    }
}