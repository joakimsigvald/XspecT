namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided ulong
/// </summary>
public class IsULong : IsNumerical<IsULong, ulong>
{
    internal IsULong(ulong actual) : base(actual) { }
}