using System.Linq.Expressions;
using XspecT.Internal;
using XspecT.Internal.TestData;

namespace XspecT;

public abstract partial class Spec<TSUT, TResult> : ITestPipeline<TSUT, TResult>
{
    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> When(Action<TSUT> act)
    {
        _pipeline.SetAction(act);
        _pipeline.Context.AddPhrase("when");
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> When(Expression<Func<TSUT, TResult>> act)
    {
        _pipeline.SetAction(act.Compile());
        _pipeline.Context.AddPhrase($"when {GetMethodName(act)}");
        return this;
    }

    private string GetMethodName(Expression<Func<TSUT, TResult>> act)
    {
        const string expression = "Expression";
        var body = act.Body;
        var methodProperty = body.GetType().GetProperty("Method");
        var method = methodProperty?.GetValue(body);
        var nameProperty = method?.GetType().GetProperty("Name");
        var name = nameProperty?.GetValue(method) as string;
        return name?.GetWords() ?? expression;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> When(Func<TSUT, Task> action)
    {
        _pipeline.SetAction(action);
        _pipeline.Context.AddPhrase("when");
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> When(Func<TSUT, Task<TResult>> func)
    {
        _pipeline.SetAction(func);
        _pipeline.Context.AddPhrase("when");
        return this;
    }

    /// <summary>
    /// Provide the tearDown to the test-pipeline
    /// </summary>
    /// <param name="tearDown"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> Before(Action<TSUT> tearDown)
    {
        _pipeline.SetTearDown(tearDown);
        return this;
    }

    /// <summary>
    /// Provide the tearDown to the test-pipeline
    /// </summary>
    /// <param name="tearDown"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> Before(Func<TSUT, Task> tearDown)
    {
        _pipeline.SetTearDown(tearDown);
        return this;
    }

    /// <summary>
    /// Provide the setUp to the test-pipeline
    /// </summary>
    /// <param name="setUp"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> After(Action<TSUT> setUp)
    {
        _pipeline.PrependSetUp(setUp);
        return this;
    }

    /// <summary>
    /// Provide the setUp to the test-pipeline
    /// </summary>
    /// <param name="setUp"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> After(Func<TSUT, Task> setUp)
    {
        _pipeline.PrependSetUp(setUp);
        return this;
    }
}