namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided byte
/// </summary>
public class IsByte : IsNumerical<IsByte, byte>
{
    internal IsByte(byte actual) : base(actual) { }
}