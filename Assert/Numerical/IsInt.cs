namespace XspecT.Assert.Numerical;

/// <summary>
/// TODO
/// </summary>
public class IsInt : IsNumerical<IsInt, int>
{
    internal IsInt(int actual) : base(actual) { }
}