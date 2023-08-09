using XspecT.Verification;

namespace XspecT.Fixture.Pipelines;

public abstract class TestPipelineBase<TResult, TParent> where TParent : SpecBase<TResult>
{
    protected readonly TParent Parent;
    protected TestPipelineBase(TParent parent) => Parent = parent;
    public TestResult<TResult> Then() => Parent.Then();
}