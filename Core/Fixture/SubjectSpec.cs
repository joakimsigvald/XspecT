namespace XspecT.Fixture;

public abstract class SubjectSpec<ISUT, TResult> : Spec<TResult> where ISUT : class
{
    protected ISUT SUT { get; private set; }
    protected override sealed void Instantiate() => SUT = Mocker.CreateInstance<ISUT>();
}