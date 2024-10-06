using Moq;
using System.Numerics;
using XspecT.Internal.TestData;
using XspecT.Internal.Verification;

namespace XspecT.Internal.Pipelines;

internal record Command(Delegate Invocation, string Expression);

internal class SpecActor<TSUT, TResult>
{
    private readonly SpecFixture<TSUT, TResult> _fixture;
    private Command _methodUnderTest;
    private Exception _error;
    private TResult _result;

    internal SpecActor(SpecFixture<TSUT, TResult> fixture) => _fixture = fixture;

    internal void When(Command act)
    {
        if (_methodUnderTest is not null)
            throw new SetupFailed("Cannot call When twice in the same pipeline");
        _methodUnderTest = act;
    }

    internal TestResult<TResult> Execute(Context context)
    {
        if (_methodUnderTest is null)
            throw new SetupFailed("When must be called before Then");
        SpecificationGenerator.AddWhen(_methodUnderTest.Expression);
        _fixture.AddToSpecification();
        bool hasResult = false;
        try
        {
            hasResult = GetResult();
        }
        catch (Exception ex) when (ex is not SetupFailed)
        {
            _error = ex;
        }
        return new(_result, _error, context, hasResult);
    }

    private bool GetResult()
    {
        const string cue = "could not resolve to an object. (Parameter 'serviceType')";
        try
        {
            (_result, var hasResult) = _fixture.Invoke(_methodUnderTest);
            return hasResult;
        }
        catch (ArgumentException ex) when (ex.Message.Contains(cue))
        {
            throw new SetupFailed($"Failed to run method under test, because an instance of {ex.Message.Split(cue)[0].Trim()} could not be provided.", ex);
        }
    }
}