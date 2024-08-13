namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided byte
/// </summary>
public record IsByte : IsNumerical<IsByte, byte>
{
    internal IsByte(byte actual) : base(actual) { }
}