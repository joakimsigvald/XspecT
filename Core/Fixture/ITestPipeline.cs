using XspecT.Verification;

namespace XspecT.Fixture;

public interface IResultProvider<TResult>
{
    TestResult<TResult> Then { get; }
}

public interface ITestPipelineBase<TResult, TSelf> : IResultProvider<TResult>
{
    TSelf GivenThat(params Action[] arrangements);
    TSelf Using<TService>(TService service);
}

public interface ITestPipeline<TResult> : ITestPipelineBase<TResult, ITestPipeline<TResult>>
{
    ITestPipeline<TValue, TResult> Given<TValue>(TValue value);
    ITestPipeline<TValue, TResult> When<TValue>(Action<TValue> act);
    ITestPipeline<TValue, TResult> When<TValue>(Func<TValue, TResult> act);
    ITestPipeline<TValue1, TValue2, TResult> Given<TValue1, TValue2>(TValue1 value1, TValue2 value2);
    ITestPipeline<TValue1, TValue2, TResult> When<TValue1, TValue2>(Action<TValue1, TValue2> act);
    ITestPipeline<TValue1, TValue2, TResult> When<TValue1, TValue2>(Func<TValue1, TValue2, TResult> act);
    ITestPipeline<TValue1, TValue2, TValue3, TResult> Given<TValue1, TValue2, TValue3>(
        TValue1 value1, TValue2 value2, TValue3 value3);
    ITestPipeline<TValue1, TValue2, TValue3, TResult> When<TValue1, TValue2, TValue3>(
        Action<TValue1, TValue2, TValue3> act);
    ITestPipeline<TValue1, TValue2, TValue3, TResult> When<TValue1, TValue2, TValue3>(
        Func<TValue1, TValue2, TValue3, TResult> act);
}

public interface ITestPipeline<TValue, TResult> : ITestPipelineBase<TResult, ITestPipeline<TValue, TResult>>
{
    ITestPipeline<TValue, TResult> Given(TValue value);
    ITestPipeline<TValue, TResult> When(Action<TValue> act);
    ITestPipeline<TValue, TResult> When(Func<TValue, TResult> act);
}

public interface ITestPipeline<TValue1, TValue2, TResult> 
    : ITestPipelineBase<TResult, ITestPipeline<TValue1, TValue2, TResult>>
{
    ITestPipeline<TValue1, TValue2, TResult> Given(TValue1 value1, TValue2 value2);
    ITestPipeline<TValue1, TValue2, TResult> When(Action<TValue1, TValue2> act);
    ITestPipeline<TValue1, TValue2, TResult> When(Func<TValue1, TValue2, TResult> act);
}

public interface ITestPipeline<TValue1, TValue2, TValue3, TResult> 
    : ITestPipelineBase<TResult, ITestPipeline<TValue1, TValue2, TValue3, TResult>>
{
    ITestPipeline<TValue1, TValue2, TValue3, TResult> Given(TValue1 value1, TValue2 value2, TValue3 value3);
    ITestPipeline<TValue1, TValue2, TValue3, TResult> When(Action<TValue1, TValue2, TValue3> act);
    ITestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, TResult> act);
}

public class TestPipeline<TResult>
{
    protected readonly SpecBase<TResult> Parent;
    protected TestPipeline(SpecBase<TResult> parent) => Parent = parent;
    public TestResult<TResult> Then => Parent.Then;
}

public class TestPipeline<TValue, TResult> : TestPipeline<TResult>, ITestPipeline<TValue, TResult>
{
    public TestPipeline(SpecBase<TResult> parent) : base(parent) { }

    public ITestPipeline<TValue, TResult> Using<TService>(TService service)
    {
        Parent.Using(service);
        return this;
    }

    public ITestPipeline<TValue, TResult> GivenThat(params Action[] arrangements)
    {
        Parent.GivenThat(arrangements);
        return this;
    }

    public ITestPipeline<TValue, TResult> Given(TValue value)
    {
        Parent.Given(value);
        return this;
    }

    public ITestPipeline<TValue, TResult> When(Action<TValue> act)
    {
        Parent.When(act);
        return this;
    }

    public ITestPipeline<TValue, TResult> When(Func<TValue, TResult> act)
    {
        Parent.When(act);
        return this;
    }
}

public class TestPipeline<TValue1, TValue2, TResult> 
    : TestPipeline<TResult>, ITestPipeline<TValue1, TValue2, TResult>
{
    public TestPipeline(SpecBase<TResult> parent) : base(parent) { }

    public ITestPipeline<TValue1, TValue2, TResult> GivenThat(params Action[] arrangements)
    {
        Parent.GivenThat(arrangements);
        return this;
    }

    public ITestPipeline<TValue1, TValue2, TResult> Given(TValue1 value1, TValue2 value2)
    {
        Parent.Given(value1, value2);
        return this;
    }

    public ITestPipeline<TValue1, TValue2, TResult> Using<TService>(TService service)
    {
        Parent.Using(service);
        return this;
    }

    public ITestPipeline<TValue1, TValue2, TResult> When(Action<TValue1, TValue2> act)
    {
        Parent.When(act);
        return this;
    }

    public ITestPipeline<TValue1, TValue2, TResult> When(Func<TValue1, TValue2, TResult> act)
    {
        Parent.When(act);
        return this;
    }
}

public class TestPipeline<TValue1, TValue2, TValue3, TResult> 
    : TestPipeline<TResult>, ITestPipeline<TValue1, TValue2, TValue3, TResult>
{
    public TestPipeline(SpecBase<TResult> parent) : base(parent) { }

    public ITestPipeline<TValue1, TValue2, TValue3, TResult> GivenThat(params Action[] arrangements)
    {
        Parent.GivenThat(arrangements);
        return this;
    }

    public ITestPipeline<TValue1, TValue2, TValue3, TResult> Given(TValue1 value1, TValue2 value2, TValue3 value3)
    {
        Parent.Given(value1, value2, value3);
        return this;
    }

    public ITestPipeline<TValue1, TValue2, TValue3, TResult> Using<TService>(TService service)
    {
        Parent.Using(service);
        return this;
    }

    public ITestPipeline<TValue1, TValue2, TValue3, TResult> When(Action<TValue1, TValue2, TValue3> act)
    {
        Parent.When(act);
        return this;
    }

    public ITestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, TResult> act)
    {
        Parent.When(act);
        return this;
    }
}