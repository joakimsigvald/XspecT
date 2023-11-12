using Moq;
using System.Linq.Expressions;
using XspecT;

namespace XspecT.Internal.Pipelines;

internal class SubjectPipeline<TSUT, TResult> : Pipeline<TResult>
    where TSUT : class
{
    private readonly Arranger _arranger = new();
    public TSUT SUT { get; private set; }

    protected override void Arrange()
    {
        _arranger.Arrange();
        SUT = CreateInstance<TSUT>();
    }

    internal TValue CreateInstance<TValue>() where TValue : class
        => _context.CreateInstance<TValue>();

    internal Mock<TObject> GetMock<TObject>() where TObject : class => _context.GetMock<TObject>();

    internal void Given(Action arrangement)
    {
        if (HasRun)
            throw new SetupFailed("Given must be called before Then");
        _arranger.Push(arrangement);
    }

    internal void SetupMock<TService>(Action<Mock<TService>> setup) where TService : class
        => _arranger.Push(() => setup(GetMock<TService>()));

    internal void SetupMock<TService, TReturns>(
        Expression<Func<TService, TReturns>> expression, Func<TReturns> returns)
        where TService : class
        => _arranger.Push(() => GetMock<TService>().Setup(expression).Returns(returns()));

    internal void SetupMock<TService, TReturns>(
        Expression<Func<TService, Task<TReturns>>> expression, Func<TReturns> returns)
        where TService : class
        => _arranger.Push(() => GetMock<TService>().Setup(expression).ReturnsAsync(returns()));
}