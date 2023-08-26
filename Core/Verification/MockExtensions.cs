using Moq;
using System.Linq.Expressions;

namespace XspecT.Verification;

public static class MockExtensions
{
    public static Moq.Language.Flow.ISetup<TObject, TResult> When<TObject, TResult>(
        this Mock<TObject> mock, Expression<Func<TObject, TResult>> expression)
        where TObject : class
        => mock.Setup(expression);

    public static Moq.Language.Flow.ISetup<TObject> When<TObject>(
        this Mock<TObject> mock, Expression<Action<TObject>> expression)
        where TObject : class
        => mock.Setup(expression);
}