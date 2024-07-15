namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided ushort
/// </summary>
public class IsUShort : IsNumerical<IsUShort, ushort>
{
    internal IsUShort(ushort actual) : base(actual) { }
}