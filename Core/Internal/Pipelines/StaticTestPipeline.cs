namespace XspecT.Internal.Pipelines;

internal class StaticTestPipeline<TResult> : TestPipeline<TResult, StaticSpec<TResult>>, ITestPipeline<TResult>
{
    internal StaticTestPipeline(StaticSpec<TResult> parent) : base(parent) { }
}