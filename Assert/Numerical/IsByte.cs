namespace XspecT.Assert.Numerical;

/// <summary>
/// TODO
/// </summary>
public class IsByte : IsNumerical<IsByte, byte>
{
    internal IsByte(byte actual) : base(actual) { }
}