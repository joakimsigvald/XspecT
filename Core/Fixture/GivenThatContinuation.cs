﻿using System.Linq.Expressions;
using XspecT.Fixture.Pipelines;

namespace XspecT.Fixture;

public class GivenThatContinuation<TSUT, TResult, TService, TReturns>
    : IGivenThatContinuation<TSUT, TResult, TService, TReturns>
    where TSUT : class
    where TService : class
{
    private readonly SubjectSpec<TSUT, TResult> _subjectSpec;
    private readonly Expression<Func<TService, TReturns>> _expression;

    public GivenThatContinuation(
        SubjectSpec<TSUT, TResult> subjectSpec, Expression<Func<TService, TReturns>> expression)
    {
        _subjectSpec = subjectSpec;
        _expression = expression;
    }

    public IGivenSubjectTestPipeline<TSUT, TResult> Returns(Func<TReturns> returns)
    {
        _subjectSpec.SetupMock<TService>(_ => _.Setup(_expression).Returns(returns()));
        return new GivenSubjectTestPipeline<TSUT, TResult>(_subjectSpec);
    }
}