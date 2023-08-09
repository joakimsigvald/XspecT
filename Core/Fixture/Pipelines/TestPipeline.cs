using XspecT.Verification;

namespace XspecT.Fixture.Pipelines;

public abstract class TestPipeline<TResult, TParent> where TParent : SpecBase<TResult>
{
    protected readonly TParent Parent;
    protected TestPipeline(TParent parent) => Parent = parent;
    public TestResult<TResult> Then() => Parent.Then();
}

public class TestPipeline<TResult> : TestPipeline<TResult, SpecBase<TResult>>
{
    protected TestPipeline(SpecBase<TResult> parent) : base(parent) { }
}