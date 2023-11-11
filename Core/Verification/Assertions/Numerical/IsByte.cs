namespace XspecT.Verification.Assertions.Numerical;

public class IsByte : IsNumerical<IsByte, byte>
{
    internal IsByte(byte actual) : base(actual) { }
}