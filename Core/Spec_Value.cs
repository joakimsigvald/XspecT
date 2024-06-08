using System.Diagnostics.CodeAnalysis;

namespace XspecT;

public abstract partial class Spec<TSUT, TResult> : ISubjectTestPipeline<TSUT, TResult>
    where TSUT : class
{
    /// <summary>
    /// Yields a value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue The<TValue>() => A<TValue>();

    /// <summary>
    /// Yields a value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue TheFirst<TValue>() => A<TValue>();

    /// <summary>
    /// Yields a value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue An<TValue>() => A<TValue>();

    /// <summary>
    /// Yields a value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue A<TValue>() => _pipeline.Mention<TValue>(0);

    /// <summary>
    /// Yields a customized value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue An<TValue>([NotNull] Action<TValue> setup) => A(setup);

    /// <summary>
    /// Yields a customized value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue A<TValue>([NotNull] Action<TValue> setup) => _pipeline.Mention(0, setup);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue An<TValue>([NotNull] Func<TValue, TValue> setup) => A(setup);

    /// <summary>
    /// Yields a customized value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue A<TValue>([NotNull] Func<TValue, TValue> setup) => _pipeline.Mention(0, setup);

    /// <summary>
    /// Provide a specific value for the first instance of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    protected TValue A<TValue>(TValue value) => _pipeline.Mention(0, value);

    /// <summary>
    /// Yields a second value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue TheSecond<TValue>() => ASecond<TValue>();

    /// <summary>
    /// Yields a second value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue ASecond<TValue>() => _pipeline.Mention<TValue>(1);

    /// <summary>
    /// Yields a customized second value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue ASecond<TValue>([NotNull] Action<TValue> setup) => _pipeline.Mention(1, setup);

    /// <summary>
    /// Provide a specific value for the second instance of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    protected TValue ASecond<TValue>(TValue value) => _pipeline.Mention(1, value);

    /// <summary>
    /// Yields a third value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue TheThird<TValue>() => AThird<TValue>();

    /// <summary>
    /// Yields a third value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue AThird<TValue>() => _pipeline.Mention<TValue>(2);

    /// <summary>
    /// Yields a customized third value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue AThird<TValue>([NotNull] Action<TValue> setup) => _pipeline.Mention(2, setup);

    /// <summary>
    /// Provide a specific value for the third instance of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    protected TValue AThird<TValue>(TValue value) => _pipeline.Mention(2, value);

    /// <summary>
    /// Yields a fourth value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue TheFourth<TValue>() => AFourth<TValue>();

    /// <summary>
    /// Yields a fourth value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue AFourth<TValue>() => _pipeline.Mention<TValue>(3);

    /// <summary>
    /// Yields a customized fourth value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue AFourth<TValue>([NotNull] Action<TValue> setup) => _pipeline.Mention(3, setup);

    /// <summary>
    /// Provide a specific value for the fourth instance of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    protected TValue AFourth<TValue>(TValue value) => _pipeline.Mention(3, value);

    /// <summary>
    /// Yields a fifth value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue TheFifth<TValue>() => AFifth<TValue>();

    /// <summary>
    /// Yields a fifth value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue AFifth<TValue>() => _pipeline.Mention<TValue>(4);

    /// <summary>
    /// Yields a customized fifth value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue AFifth<TValue>([NotNull] Action<TValue> setup) => _pipeline.Mention(4, setup);

    /// <summary>
    /// Provide a specific value for the fifth instance of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    protected TValue AFifth<TValue>(TValue value) => _pipeline.Mention(4, value);

    /// <summary>
    /// Yields a value of the given type that cannot be retreived again
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue Another<TValue>() => _pipeline.Create<TValue>();

    /// <summary>
    /// Yields a customized value of the given type that cannot be retreived again
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue Another<TValue>([NotNull] Action<TValue> setup) => _pipeline.Create(setup);

    /// <summary>
    /// Yields a value of the given type by label, that can be retrived again given the same label
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="label"></param>
    /// <returns></returns>
    protected TValue The<TValue>(string label) => _pipeline.Mention<TValue>(label);
}