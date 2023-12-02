namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided short
/// </summary>
public class IsShort : IsNumerical<IsShort, short>
{
    internal IsShort(short actual) : base(actual) { }
}