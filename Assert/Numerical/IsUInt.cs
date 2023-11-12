namespace XspecT.Assert.Numerical;

/// <summary>
/// TODO
/// </summary>
public class IsUInt : IsNumerical<IsUInt, uint>
{
    internal IsUInt(uint actual) : base(actual) { }
}