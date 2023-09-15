namespace XspecT.Internal;

public class Arranger
{
    private readonly List<Action> _arrangements = new();
    internal void Add(Action arrangement) => _arrangements.Add(arrangement);
    internal void Push(Action arrangement) => _arrangements.Insert(0, arrangement);
    internal void Arrange() => _arrangements.ForEach(_ => _());
}