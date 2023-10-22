using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using XspecT.Verification;

namespace XspecT.Fixture;

public interface IPipeline<TResult>
{
    bool HasRun { get; }
    ITestResult<TResult> Then();
    IAndVerify<TResult> Then<TService>(Expression<Action<TService>> expression) where TService : class;
    IAndVerify<TResult> Then<TService>(Expression<Action<TService>> expression, Times times) where TService : class;
    IAndVerify<TResult> Then<TService>(Expression<Action<TService>> expression, Func<Times> times) where TService : class;
    IAndVerify<TResult> Then<TService, TReturns>(Expression<Func<TService, TReturns>> expression) where TService : class;
    IAndVerify<TResult> Then<TService, TReturns>(Expression<Func<TService, TReturns>> expression, Times times)
        where TService : class;
    IAndVerify<TResult> Then<TService, TReturns>(Expression<Func<TService, TReturns>> expression, Func<Times> times)
        where TService : class;
    void SetAction(Action act);
    void SetAction(Func<TResult> act);
    void SetAction(Func<Task> action);
    void SetAction(Func<Task<TResult>> func);
    TValue Mention<TValue>(int index);
    TValue Create<TValue>();
    TValue Create<TValue>([NotNull] Action<TValue> setup);
    TValue[] MentionMany<TValue>(int count);
    TValue Mention<TValue>(string label);
    TValue[] MentionMany<TValue>([NotNull] Action<TValue> setup, int count);
    TValue Mention<TValue>(int index, [NotNull] Action<TValue> setup);
    TValue Mention<TValue>(int index, TValue value, bool asDefault = false);
    TValue[] MentionMany<TValue>([NotNull] Action<TValue, int> setup, int count);
}