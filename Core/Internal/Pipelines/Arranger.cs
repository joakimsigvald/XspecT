using System.Linq.Expressions;

namespace XspecT.Internal.Pipelines;

internal class Arranger
{
    private readonly Stack<Expression<Action>> _arrangements = new();
    internal void Push(Expression<Action> arrangement) => _arrangements.Push(arrangement);
    internal void Arrange() => _arrangements.ToList().ForEach(Apply);
    private void Apply(Expression<Action> arrangement)
    {
        var action = arrangement.Compile();
        action();
    }
}