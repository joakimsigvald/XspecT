namespace XspecT.Fixture;

/// <summary>
/// Not intended for direct override. Override either TestStatic or TestSubject instead
/// </summary>
public abstract class Spec<TResult> : SpecBase<TResult>
{
    public override sealed void Dispose() => TearDown();
    protected virtual void TearDown() { }
}