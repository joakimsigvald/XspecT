namespace XspecT.Verification.Assertions.Numerical;

public class IsByte : IsNumerical<IsByte, byte>
{
    public IsByte(byte actual) : base(actual) { }
}