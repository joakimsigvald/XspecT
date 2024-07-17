using System.Diagnostics.CodeAnalysis;
using XspecT.Internal.TestData;

namespace XspecT;

public abstract partial class Spec<TSUT, TResult> : ITestPipeline<TSUT, TResult>
{
    /// <summary>
    /// Yields a value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected internal TValue The<TValue>() => A<TValue>();

    /// <summary>
    /// Yields a value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected internal TValue TheFirst<TValue>() => A<TValue>();

    /// <summary>
    /// Yields a value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected internal TValue An<TValue>()
    {
        Context.AddFragment($" an {typeof(TValue).Alias()}");
        return A<TValue>();
    }

    /// <summary>
    /// Yields a value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected internal TValue A<TValue>() => _pipeline.Mention<TValue>(0);

    /// <summary>
    /// Yields a customized value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected internal TValue An<TValue>([NotNull] Action<TValue> setup) => A(setup);

    /// <summary>
    /// Yields a customized value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected internal TValue A<TValue>([NotNull] Action<TValue> setup) => _pipeline.Mention(0, setup);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected internal TValue An<TValue>([NotNull] Func<TValue, TValue> setup)
    {
        return A(setup);
    }

    /// <summary>
    /// Yields a customized value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected internal TValue A<TValue>([NotNull] Func<TValue, TValue> setup) => _pipeline.Mention(0, setup);

    /// <summary>
    /// Provide a specific value for the first instance of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    protected internal TValue A<TValue>(TValue value) => _pipeline.Mention(0, value);

    /// <summary>
    /// Yields a second value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected internal TValue TheSecond<TValue>() => ASecond<TValue>();

    /// <summary>
    /// Yields a second value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected internal TValue ASecond<TValue>() => _pipeline.Mention<TValue>(1);

    /// <summary>
    /// Yields a customized second value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected internal TValue ASecond<TValue>([NotNull] Action<TValue> setup) => _pipeline.Mention(1, setup);

    /// <summary>
    /// Provide transform for a second value of a given type, that can be mentioned in the test pipeline as ASecond or TheSecond.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <returns></returns>
    protected internal TValue ASecond<TValue>([NotNull] Func<TValue, TValue> transform) => _pipeline.Mention(1, transform);

    /// <summary>
    /// Provide a specific value for the second instance of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    protected internal TValue ASecond<TValue>(TValue value) => _pipeline.Mention(1, value);

    /// <summary>
    /// Yields a third value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected internal TValue TheThird<TValue>() => AThird<TValue>();

    /// <summary>
    /// Yields a third value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected internal TValue AThird<TValue>() => _pipeline.Mention<TValue>(2);

    /// <summary>
    /// Yields a customized third value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected internal TValue AThird<TValue>([NotNull] Action<TValue> setup) => _pipeline.Mention(2, setup);

    /// <summary>
    /// Provide transform for a third value of a given type, that can be mentioned in the test pipeline as AThird or TheThird.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <returns></returns>
    protected internal TValue AThird<TValue>([NotNull] Func<TValue, TValue> transform) => _pipeline.Mention(2, transform);

    /// <summary>
    /// Provide a specific value for the third instance of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    protected internal TValue AThird<TValue>(TValue value) => _pipeline.Mention(2, value);

    /// <summary>
    /// Yields a fourth value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected internal TValue TheFourth<TValue>() => AFourth<TValue>();

    /// <summary>
    /// Yields a fourth value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected internal TValue AFourth<TValue>() => _pipeline.Mention<TValue>(3);

    /// <summary>
    /// Yields a customized fourth value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected internal TValue AFourth<TValue>([NotNull] Action<TValue> setup) => _pipeline.Mention(3, setup);

    /// <summary>
    /// Provide transform for a fourth value of a given type, that can be mentioned in the test pipeline as AFourth or TheFourth.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <returns></returns>
    protected internal TValue AFourth<TValue>([NotNull] Func<TValue, TValue> transform) => _pipeline.Mention(3, transform);

    /// <summary>
    /// Provide a specific value for the fourth instance of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    protected internal TValue AFourth<TValue>(TValue value) => _pipeline.Mention(3, value);

    /// <summary>
    /// Yields a fifth value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected internal TValue TheFifth<TValue>() => AFifth<TValue>();

    /// <summary>
    /// Yields a fifth value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected internal TValue AFifth<TValue>() => _pipeline.Mention<TValue>(4);

    /// <summary>
    /// Yields a customized fifth value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected internal TValue AFifth<TValue>([NotNull] Action<TValue> setup) => _pipeline.Mention(4, setup);

    /// <summary>
    /// Provide a specific value for the fifth instance of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    protected internal TValue AFifth<TValue>(TValue value) => _pipeline.Mention(4, value);

    /// <summary>
    /// Provide transform for a fifth value of a given type, that can be mentioned in the test pipeline as AFifth or TheFifth.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <returns></returns>
    protected internal TValue AFifth<TValue>([NotNull] Func<TValue, TValue> transform) => _pipeline.Mention(4, transform);

    /// <summary>
    /// Yields a value of the given type that cannot be retreived again
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected internal TValue Another<TValue>() => _pipeline.Create<TValue>();

    /// <summary>
    /// Yields a customized value of the given type that cannot be retreived again
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected internal TValue Another<TValue>([NotNull] Action<TValue> setup) => _pipeline.Create(setup);
}