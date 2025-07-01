namespace XspecT;

public abstract partial class Spec<TSUT, TResult> : ITestPipeline<TSUT, TResult>
{
    /// <summary>
    /// Yields an array with one element of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected internal TValue[] One<TValue>() => _pipeline.MentionMany<TValue>(1);

    /// <summary>
    /// Yields an array with the provided element
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected internal TValue[] One<TValue>(TValue value)
    {
        _pipeline.Assign(0, value);
        return _pipeline.MentionMany<TValue>(1);
    }

    /// <summary>
    /// Yields an array with the provided elements
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected internal TValue[] Some<TValue>(IEnumerable<TValue> values)
    {
        var arr = values.Take(5).ToArray();
        for (var i = 0; i < arr.Length; i++)
            _pipeline.Assign(i, arr[i]);
        return _pipeline.AssignMany(arr);
    }

    /// <summary>
    /// Yields an array with one customized element of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected internal TValue[] One<TValue>(Action<TValue> setup) => _pipeline.ApplyMany(setup, 1);

    /// <summary>
    /// Yields an array with one customized element of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <returns></returns>
    protected internal TValue[] One<TValue>(Func<TValue, TValue> transform) => _pipeline.ApplyMany(transform, 1);

    /// <summary>
    /// Yields an array with two elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected internal TValue[] Two<TValue>() => _pipeline.MentionMany<TValue>(2);

    /// <summary>
    /// Yields an array with two customized elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected internal TValue[] Two<TValue>(Action<TValue> setup) => _pipeline.ApplyMany(setup, 2);

    /// <summary>
    /// Yields an array with two individually customized elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected internal TValue[] Two<TValue>(Action<TValue, int> setup) => _pipeline.ApplyMany(setup, 2);

    /// <summary>
    /// Yields an array with two customized elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <returns></returns>
    protected internal TValue[] Two<TValue>(Func<TValue, TValue> transform) => _pipeline.ApplyMany(transform, 2);

    /// <summary>
    /// Yields an array with two customized elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <returns></returns>
    protected internal TValue[] Two<TValue>(Func<TValue, int, TValue> transform) 
        => _pipeline.ApplyMany(transform, 2);

    /// <summary>
    /// Yields an array with three elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected internal TValue[] Three<TValue>() => _pipeline.MentionMany<TValue>(3);

    /// <summary>
    /// Yields an array with three customized elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected internal TValue[] Three<TValue>(Action<TValue> setup) => _pipeline.ApplyMany(setup, 3);

    /// <summary>
    /// Yields an array with three individually customized elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected internal TValue[] Three<TValue>(Action<TValue, int> setup) => _pipeline.ApplyMany(setup, 3);

    /// <summary>
    /// Yields an array with three customized elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <returns></returns>
    protected internal TValue[] Three<TValue>(Func<TValue, TValue> transform) => _pipeline.ApplyMany(transform, 3);

    /// <summary>
    /// Yields an array with three customized elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <returns></returns>
    protected internal TValue[] Three<TValue>(Func<TValue, int, TValue> transform) 
        => _pipeline.ApplyMany(transform, 3);

    /// <summary>
    /// Yields an array with four elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected internal TValue[] Four<TValue>() => _pipeline.MentionMany<TValue>(4);

    /// <summary>
    /// Yields an array with four customized elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected internal TValue[] Four<TValue>(Action<TValue> setup) => _pipeline.ApplyMany(setup, 4);

    /// <summary>
    /// Yields an array with four individually customized elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected internal TValue[] Four<TValue>(Action<TValue, int> setup) => _pipeline.ApplyMany(setup, 4);

    /// <summary>
    /// Yields an array with four customized elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <returns></returns>
    protected internal TValue[] Four<TValue>(Func<TValue, TValue> transform) => _pipeline.ApplyMany(transform, 4);

    /// <summary>
    /// Yields an array with four customized elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <returns></returns>
    protected internal TValue[] Four<TValue>(Func<TValue, int, TValue> transform) 
        => _pipeline.ApplyMany(transform, 4);

    /// <summary>
    /// Yields an array with five elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected internal TValue[] Five<TValue>() => _pipeline.MentionMany<TValue>(5);

    /// <summary>
    /// Yields an array with five customized elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected internal TValue[] Five<TValue>(Action<TValue> setup) => _pipeline.ApplyMany(setup, 5);

    /// <summary>
    /// Yields an array with five individually customized elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected internal TValue[] Five<TValue>(Action<TValue, int> setup) => _pipeline.ApplyMany(setup, 5);

    /// <summary>
    /// Yields an array with five customized elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <returns></returns>
    protected internal TValue[] Five<TValue>(Func<TValue, TValue> transform) => _pipeline.ApplyMany(transform, 5);

    /// <summary>
    /// Yields an array with five customized elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="transform"></param>
    /// <returns></returns>
    protected internal TValue[] Five<TValue>(Func<TValue, int, TValue> transform)
        => _pipeline.ApplyMany(transform, 5);

    /// <summary>
    /// Yields an array with zero elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected internal TValue[] Zero<TValue>() => _pipeline.MentionMany<TValue>(0);

    /// <summary>
    /// Yields an array with at least one elements of the given type. 
    /// If such an array exists it will be returned, otherwise and array with two elements will be returned.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected internal TValue[] Some<TValue>() => _pipeline.MentionMany<TValue>(2, 1);

    /// <summary>
    /// Yields an array with at least two elements of the given type. 
    /// If such an array exists it will be returned, otherwise and array with four elements will be returned.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected internal TValue[] Many<TValue>() => _pipeline.MentionMany<TValue>(3, 2);
}