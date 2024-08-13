using Moq;
using System.Runtime.CompilerServices;
using XspecT.Continuations;
using XspecT.Internal.TestData;

namespace XspecT.Internal.Pipelines;

internal class GivenContinuation<TSUT, TResult> : IGivenContinuation<TSUT, TResult> 
{
    private readonly Spec<TSUT, TResult> _spec;

    internal GivenContinuation(Spec<TSUT, TResult> spec) => _spec = spec;

    public IGivenTestPipeline<TSUT, TResult> Default<TValue>(
        TValue defaultValue,
        [CallerArgumentExpression(nameof(defaultValue))] string defaultValueExpr = null)
        => _spec.GivenDefault(defaultValue, ApplyTo.Default, defaultValueExpr);
 
    public IGivenTestPipeline<TSUT, TResult> Default<TValue>(
        Func<TValue> defaultValue,
        [CallerArgumentExpression(nameof(defaultValue))] string defaultValueExpr = null)
        => _spec.GivenDefault(defaultValue, ApplyTo.Default, defaultValueExpr);

    public IGivenTestPipeline<TSUT, TResult> Using<TValue>(
        TValue defaultValue,
        [CallerArgumentExpression(nameof(defaultValue))] string defaultValueExpr = null)
        => _spec.GivenDefault(defaultValue, ApplyTo.Using, defaultValueExpr);
    
    public IGivenTestPipeline<TSUT, TResult> Using<TValue>(
        Func<TValue> defaultValue,
        [CallerArgumentExpression(nameof(defaultValue))] string defaultValueExpr = null)
        => _spec.GivenDefault(defaultValue, ApplyTo.Using, defaultValueExpr);

    public IGivenTestPipeline<TSUT, TResult> Default<TValue>(Action<TValue> defaultSetup)
         where TValue : class
        => _spec.Given(defaultSetup);

    public IGivenTestPipeline<TSUT, TResult> Default<TValue>(Func<TValue, TValue> defaultSetup)
        => _spec.Given(defaultSetup);

    public IGivenTestPipeline<TSUT, TResult> A<TValue>(TValue value)
        => _spec.ArrangeFirst(() => _spec.A(value));

    public IGivenTestPipeline<TSUT, TResult> A<TValue>(Action<TValue> setup)
         where TValue : class
        => _spec.ArrangeFirst(() => _spec.A(setup));

    public IGivenTestPipeline<TSUT, TResult> A<TValue>(
        Func<TValue, TValue> transform,
        [CallerArgumentExpression(nameof(transform))] string transformExpr = null)
        => _spec.Mention<TValue>(() => _spec.A(transform), transformExpr);

    public IGivenTestPipeline<TSUT, TResult> An<TValue>(TValue value)
        => _spec.ArrangeFirst(() => _spec.A(value));

    public IGivenTestPipeline<TSUT, TResult> An<TValue>(Action<TValue> setup)
         where TValue : class
        => _spec.ArrangeFirst(() => _spec.A(setup));

    public IGivenTestPipeline<TSUT, TResult> An<TValue>(Func<TValue, TValue> transform)
        => _spec.ArrangeFirst(() => _spec.A(transform));

    public IGivenTestPipeline<TSUT, TResult> ASecond<TValue>(TValue value)
        => _spec.ArrangeFirst(() => _spec.ASecond(value));

    public IGivenTestPipeline<TSUT, TResult> ASecond<TValue>(Action<TValue> setup)
         where TValue : class
        => _spec.ArrangeFirst(() => _spec.ASecond(setup));

    public IGivenTestPipeline<TSUT, TResult> ASecond<TValue>(Func<TValue, TValue> transform)
        => _spec.ArrangeFirst(() => _spec.ASecond(transform));

    public IGivenTestPipeline<TSUT, TResult> AThird<TValue>(TValue value)
        => _spec.ArrangeFirst(() => _spec.AThird(value));

    public IGivenTestPipeline<TSUT, TResult> AThird<TValue>(Action<TValue> setup)
         where TValue : class
        => _spec.ArrangeFirst(() => _spec.AThird(setup));

    public IGivenTestPipeline<TSUT, TResult> AThird<TValue>(Func<TValue, TValue> transform)
        => _spec.ArrangeFirst(() => _spec.AThird(transform));

    public IGivenTestPipeline<TSUT, TResult> AFourth<TValue>(TValue value)
        => _spec.ArrangeFirst(() => _spec.AFourth(value));

    public IGivenTestPipeline<TSUT, TResult> AFourth<TValue>(Action<TValue> setup)
         where TValue : class
        => _spec.ArrangeFirst(() => _spec.AFourth(setup));

    public IGivenTestPipeline<TSUT, TResult> AFourth<TValue>(Func<TValue, TValue> transform)
        => _spec.ArrangeFirst(() => _spec.AFourth(transform));

    public IGivenTestPipeline<TSUT, TResult> AFifth<TValue>(TValue value)
        => _spec.ArrangeFirst(() => _spec.AFifth(value));

    public IGivenTestPipeline<TSUT, TResult> AFifth<TValue>(Action<TValue> setup)
         where TValue : class
        => _spec.ArrangeFirst(() => _spec.AFifth(setup));

    public IGivenTestPipeline<TSUT, TResult> AFifth<TValue>(Func<TValue, TValue> transform)
        => _spec.ArrangeFirst(() => _spec.AFifth(transform));

    [Obsolete]
    public IGivenTestPipeline<TSUT, TResult> That(Action setup) => _spec.ArrangeFirst(setup);
}