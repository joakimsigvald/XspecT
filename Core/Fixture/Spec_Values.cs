using System.Diagnostics.CodeAnalysis;
using XspecT.Fixture.Pipelines;

namespace XspecT.Fixture;

/// <summary>
/// Not intended for direct override. Override TestStatic or TestSubject instead
/// </summary>
public abstract partial class Spec<TResult> : ITestPipeline<TResult>, IDisposable
{
    /// <summary>
    /// Alias for A
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue The<TValue>() => A<TValue>();

    /// <summary>
    /// Alias for A
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue TheFirst<TValue>() => A<TValue>();

    /// <summary>
    /// Alias for A
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue An<TValue>() => A<TValue>();

    /// <summary>
    /// Alias for A
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue An<TValue>([NotNull] Action<TValue> setup) => A(setup);

    /// <summary>
    /// Yields a new value of the given type, or same value as previously mentioned, including as part of a Using clause
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue A<TValue>() => _pipeline.Mention<TValue>(0);

    /// <summary>
    /// Yields a new customized value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue A<TValue>([NotNull] Action<TValue> setup) => _pipeline.Mention(0, setup);

    /// <summary>
    /// Provide a value that can later be mentioned
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue A<TValue>(TValue value) => _pipeline.Mention(0, value);

    /// <summary>
    /// Will always yield a new model of the given type, unless TValue is an interface. 
    /// Do not use in combination with Using or reference the generated value twice in the same pipeline, 
    /// since that might give the specification confusing semantics
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue Another<TValue>() => _pipeline.Create<TValue>();

    /// <summary>
    /// Yields a new customized value of the given type, which cannot be reused
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue Another<TValue>([NotNull] Action<TValue> setup) => _pipeline.Create(setup);

    /// <summary>
    /// Mention a value by label, that can be reused, given the same label
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="label"></param>
    /// <returns></returns>
    protected TValue The<TValue>(string label) => _pipeline.Mention<TValue>(label);

    /// <summary>
    /// Alias for ASecond
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue TheSecond<TValue>() => ASecond<TValue>();

    /// <summary>
    /// Yields a new value of the given type, or same value as previously mentioned as second value
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue ASecond<TValue>() => _pipeline.Mention<TValue>(1);

    /// <summary>
    /// Yields a new customized second value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue ASecond<TValue>([NotNull] Action<TValue> setup) => _pipeline.Mention(1, setup);

    /// <summary>
    /// Alias for AThird
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue TheThird<TValue>() => AThird<TValue>();

    /// <summary>
    /// Yields a new value of the given type, or same value as previously mentioned as third value
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue AThird<TValue>() => _pipeline.Mention<TValue>(2);

    /// <summary>
    /// Yields a new customized third value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue AThird<TValue>([NotNull] Action<TValue> setup) => _pipeline.Mention(2, setup);

    /// <summary>
    /// Alias for AFourth
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue TheFourth<TValue>() => AFourth<TValue>();

    /// <summary>
    /// Yields a new value of the given type, or same value as previously mentioned as fourth value
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue AFourth<TValue>() => _pipeline.Mention<TValue>(3);

    /// <summary>
    /// Yields a new customized fourth value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue AFourth<TValue>([NotNull] Action<TValue> setup) => _pipeline.Mention(3, setup);

    /// <summary>
    /// Alias for AFifth
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue TheFifth<TValue>() => AFifth<TValue>();

    /// <summary>
    /// Yields a new value of the given type, or same value as previously mentioned as fifth value
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue AFifth<TValue>() => _pipeline.Mention<TValue>(4);

    /// <summary>
    /// Yields a new customized fifth value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue AFifth<TValue>([NotNull] Action<TValue> setup) => _pipeline.Mention(4, setup);

    /// <summary>
    /// Yields an array with one element of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue[] One<TValue>() => _pipeline.MentionMany<TValue>(1);

    /// <summary>
    /// Yields an array with one customized element of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue[] One<TValue>([NotNull] Action<TValue> setup) => _pipeline.MentionMany(setup, 1);

    /// <summary>
    /// Yields an array with two element of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue[] Two<TValue>() => _pipeline.MentionMany<TValue>(2);

    /// <summary>
    /// Yields an array with two customized element of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue[] Two<TValue>([NotNull] Action<TValue> setup) => _pipeline.MentionMany(setup, 2);

    /// <summary>
    /// Yields an array with two individually customized elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue[] Two<TValue>([NotNull] Action<TValue, int> setup) => _pipeline.MentionMany(setup, 2);

    /// <summary>
    /// Yields an array with three element of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue[] Three<TValue>() => _pipeline.MentionMany<TValue>(3);

    /// <summary>
    /// Yields an array with three customized element of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue[] Three<TValue>([NotNull] Action<TValue> setup) => _pipeline.MentionMany(setup, 3);

    /// <summary>
    /// Yields an array with three individually customized elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue[] Three<TValue>([NotNull] Action<TValue, int> setup) => _pipeline.MentionMany(setup, 3);

    /// <summary>
    /// Yields an array with four element of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue[] Four<TValue>() => _pipeline.MentionMany<TValue>(4);

    /// <summary>
    /// Yields an array with four customized element of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue[] Four<TValue>([NotNull] Action<TValue> setup) => _pipeline.MentionMany(setup, 4);

    /// <summary>
    /// Yields an array with four individually customized elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue[] Four<TValue>([NotNull] Action<TValue, int> setup) => _pipeline.MentionMany(setup, 4);

    /// <summary>
    /// Yields an array with five element of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue[] Five<TValue>() => _pipeline.MentionMany<TValue>(5);

    /// <summary>
    /// Yields an array with five customized element of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue[] Five<TValue>([NotNull] Action<TValue> setup) => _pipeline.MentionMany(setup, 5);

    /// <summary>
    /// Yields an array with five individually customized elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue[] Five<TValue>([NotNull] Action<TValue, int> setup) => _pipeline.MentionMany(setup, 5);

    /// <summary>
    /// Alias for Three
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue[] Many<TValue>() => Three<TValue>();

    /// <summary>
    /// Alias for Three
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue[] Many<TValue>([NotNull] Action<TValue> setup) => Three(setup);

    /// <summary>
    /// Alias for Three
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue[] Many<TValue>([NotNull] Action<TValue, int> setup) => Three(setup);
}