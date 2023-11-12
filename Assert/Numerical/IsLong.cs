namespace XspecT.Assert.Numerical;

/// <summary>
/// TODO
/// </summary>
public class IsLong : IsNumerical<IsLong, long>
{
    internal IsLong(long actual) : base(actual) { }
}