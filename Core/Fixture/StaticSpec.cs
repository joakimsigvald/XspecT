namespace XspecT.Fixture;

public abstract class StaticSpec<TResult> : SpecBase<TResult>
{
    protected override sealed void Instantiate() { }
}