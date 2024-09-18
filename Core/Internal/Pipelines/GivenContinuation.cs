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

    public IGivenTestPipeline<TSUT, TResult> Default<TValue>(
        Action<TValue> defaultSetup,
        [CallerArgumentExpression(nameof(defaultSetup))] string defaultSetupExpr = null)
         where TValue : class
        => _spec.Given(defaultSetup, defaultSetupExpr);

    public IGivenTestPipeline<TSUT, TResult> Default<TValue>(
        Func<TValue, TValue> defaultTransform,
        [CallerArgumentExpression(nameof(defaultTransform))] string defaultTransformExpr = null)
        => _spec.Given(defaultTransform, defaultTransformExpr);

    public IGivenTestPipeline<TSUT, TResult> A<TValue>(
        TValue value,
        [CallerArgumentExpression(nameof(value))] string valueExpr = null)
        => _spec.Mention<TValue>(() => _spec.A(value), valueExpr);

    public IGivenTestPipeline<TSUT, TResult> A<TValue>(
        Action<TValue> setup,
        [CallerArgumentExpression(nameof(setup))] string setupExpr = null)
         where TValue : class
        => _spec.Mention<TValue>(() => _spec.A(setup), setupExpr);

    public IGivenTestPipeline<TSUT, TResult> A<TValue>(
        Func<TValue, TValue> transform,
        [CallerArgumentExpression(nameof(transform))] string transformExpr = null)
        => _spec.Mention<TValue>(() => _spec.A(transform), transformExpr);

    public IGivenTestPipeline<TSUT, TResult> An<TValue>(
        TValue value,
        [CallerArgumentExpression(nameof(value))] string valueExpr = null)
        => _spec.Mention<TValue>(() => _spec.A(value), valueExpr);

    public IGivenTestPipeline<TSUT, TResult> An<TValue>(
        Action<TValue> setup,
        [CallerArgumentExpression(nameof(setup))] string setupExpr = null)
         where TValue : class
        => _spec.Mention<TValue>(() => _spec.An(setup), setupExpr);

    public IGivenTestPipeline<TSUT, TResult> An<TValue>(
        Func<TValue, TValue> transform,
        [CallerArgumentExpression(nameof(transform))] string transformExpr = null)
        => _spec.Mention<TValue>(() => _spec.An(transform), transformExpr);

    public IGivenTestPipeline<TSUT, TResult> ASecond<TValue>(
        TValue value,
        [CallerArgumentExpression(nameof(value))] string valueExpr = null)
        => _spec.Mention<TValue>(() => _spec.ASecond(value), valueExpr);

    public IGivenTestPipeline<TSUT, TResult> ASecond<TValue>(
        Action<TValue> setup,
        [CallerArgumentExpression(nameof(setup))] string setupExpr = null)
         where TValue : class
        => _spec.Mention<TValue>(() => _spec.ASecond(setup), setupExpr);

    public IGivenTestPipeline<TSUT, TResult> ASecond<TValue>(
        Func<TValue, TValue> transform,
        [CallerArgumentExpression(nameof(transform))] string transformExpr = null)
        => _spec.Mention<TValue>(() => _spec.ASecond(transform), transformExpr);

    public IGivenTestPipeline<TSUT, TResult> AThird<TValue>(
        TValue value,
        [CallerArgumentExpression(nameof(value))] string valueExpr = null)
        => _spec.Mention<TValue>(() => _spec.AThird(value), valueExpr);

    public IGivenTestPipeline<TSUT, TResult> AThird<TValue>(
        Action<TValue> setup,
        [CallerArgumentExpression(nameof(setup))] string setupExpr = null)
         where TValue : class
        => _spec.Mention<TValue>(() => _spec.AThird(setup), setupExpr);

    public IGivenTestPipeline<TSUT, TResult> AThird<TValue>(
        Func<TValue, TValue> transform,
        [CallerArgumentExpression(nameof(transform))] string transformExpr = null)
        => _spec.Mention<TValue>(() => _spec.AThird(transform), transformExpr);

    public IGivenTestPipeline<TSUT, TResult> AFourth<TValue>(
        TValue value,
        [CallerArgumentExpression(nameof(value))] string valueExpr = null)
        => _spec.Mention<TValue>(() => _spec.AFourth(value), valueExpr);

    public IGivenTestPipeline<TSUT, TResult> AFourth<TValue>(
        Action<TValue> setup,
        [CallerArgumentExpression(nameof(setup))] string setupExpr = null)
         where TValue : class
        => _spec.Mention<TValue>(() => _spec.AFourth(setup), setupExpr);

    public IGivenTestPipeline<TSUT, TResult> AFourth<TValue>(
        Func<TValue, TValue> transform,
        [CallerArgumentExpression(nameof(transform))] string transformExpr = null)
        => _spec.Mention<TValue>(() => _spec.AFourth(transform), transformExpr);

    public IGivenTestPipeline<TSUT, TResult> AFifth<TValue>(
        TValue value,
        [CallerArgumentExpression(nameof(value))] string valueExpr = null)
        => _spec.Mention<TValue>(() => _spec.AFifth(value), valueExpr);

    public IGivenTestPipeline<TSUT, TResult> AFifth<TValue>(
        Action<TValue> setup,
        [CallerArgumentExpression(nameof(setup))] string setupExpr = null)
         where TValue : class
        => _spec.Mention<TValue>(() => _spec.AFifth(setup), setupExpr);

    public IGivenTestPipeline<TSUT, TResult> AFifth<TValue>(
        Func<TValue, TValue> transform,
        [CallerArgumentExpression(nameof(transform))] string transformExpr = null)
        => _spec.Mention<TValue>(() => _spec.AFifth(transform), transformExpr);
}