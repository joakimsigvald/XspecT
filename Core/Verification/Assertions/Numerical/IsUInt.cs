namespace XspecT.Verification.Assertions.Numerical;

public class IsUInt : IsNumerical<IsUInt, uint>
{
    internal IsUInt(uint actual) : base(actual) { }
}