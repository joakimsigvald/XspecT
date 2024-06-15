using System.Globalization;
using XspecT.Internal.Pipelines;

namespace XspecT;

/// <summary>
/// SubjectSpec and StaticSpec has been merged to Spec, so replace SubjectSpec with Spec in your tests
/// </summary>
/// <typeparam name="TSUT"></typeparam>
/// <typeparam name="TResult"></typeparam>
[Obsolete("Replaced by Spec")]
public abstract class SubjectSpec<TSUT, TResult> : Spec<TSUT, TResult>
{
}

/// <summary>
/// SubjectSpec and StaticSpec has been merged to Spec, so replace StaticSpec with Spec in your tests
/// </summary>
/// <typeparam name="TResult"></typeparam>
[Obsolete("Replaced by Spec")]
public abstract class StaticSpec<TResult> : Spec<object, TResult>
{
}

/// <summary>
/// Base-class for specifying and executing a set of test cases for a specific method-under-test
/// </summary>
/// <typeparam name="TSUTandResult">The return type of the method-under-test is also passed as argument to the test method</typeparam>
public abstract class Spec<TSUTandResult> : Spec<TSUTandResult, TSUTandResult>
{
}

/// <summary>
/// Base-class for specifying and executing a set of test cases for a specific method-under-test
/// </summary>
/// <typeparam name="TSUT">The class to instantiate and execute the method-under-test on</typeparam>
/// <typeparam name="TResult">The return type of the method-under-test</typeparam>
public abstract partial class Spec<TSUT, TResult> : ITestPipeline<TSUT, TResult>
{
    private readonly Pipeline<TSUT, TResult> _pipeline = new();

    /// <summary>
    /// 
    /// </summary>
    protected Spec() => CultureInfo.CurrentCulture = GetCulture();

    /// <summary>
    /// Override this to set different Culture than InvariantCulture during test
    /// </summary>
    /// <returns></returns>
    protected virtual CultureInfo GetCulture() => CultureInfo.InvariantCulture;
}