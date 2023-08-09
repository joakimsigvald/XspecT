namespace XspecT.Fixture.Pipelines;

public class TestPipeline<TResult> : TestPipelineBase<TResult, SpecBase<TResult>>
{
    protected TestPipeline(SpecBase<TResult> parent) : base(parent) { }
}