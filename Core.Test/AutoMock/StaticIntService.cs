namespace XspecT.Test.AutoMock;

public class StaticIntService
{
    private readonly int _value;
    public StaticIntService(int value) => _value = value;
    public int GetValue() => _value;
}