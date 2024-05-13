using Moq;
using System.Linq.Expressions;

namespace XspecT.Internal.Pipelines;

internal class SubjectPipeline<TSUT, TResult> : Pipeline<TResult>
    where TSUT : class
{
    private readonly Arranger _arranger = new();
    private TSUT _sut;

    internal override sealed void Arrange()
    {
        _arranger.Arrange();
        _sut = CreateInstance<TSUT>();
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
    {
        _arranger.Push(() => GetMock<TService>().SetupSequence(expression)
        .Returns(returns).Returns(returns).Returns(returns).Returns(returns).Returns(returns));
    }

    internal void SetupMock<TService, TReturns>(
        Expression<Func<TService, Task<TReturns>>> expression, Func<TReturns> returns)
        where TService : class
        => _arranger.Push(() => GetMock<TService>().SetupSequence(expression)
        .ReturnsAsync(returns).ReturnsAsync(returns).ReturnsAsync(returns).ReturnsAsync(returns).ReturnsAsync(returns));

    internal void SetAction(Action<TSUT> act) => SetAction(() => act(_sut));
    internal void SetAction(Func<TSUT, TResult> act) => SetAction(() => act(_sut));
    internal void SetAction(Func<TSUT, Task> action) => SetAction(() => action(_sut));
    internal void SetAction(Func<TSUT, Task<TResult>> func) => SetAction(() => func(_sut));
    internal void SetTearDown(Action<TSUT> tearDown) => SetTearDown(() => tearDown(_sut));
    internal void SetTearDown(Func<TSUT, Task> tearDown) => SetTearDown(() => tearDown(_sut));
    internal void PrependSetUp(Action<TSUT> setUp) => PrependSetUp(() => setUp(_sut));
    internal void PrependSetUp(Func<TSUT, Task> setUp) => PrependSetUp(() => setUp(_sut));
}