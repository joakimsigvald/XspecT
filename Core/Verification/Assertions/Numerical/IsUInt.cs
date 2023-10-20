namespace XspecT.Verification.Assertions.Numerical;

public class IsUInt : IsNumerical<IsUInt, uint>
{
    public IsUInt(uint actual) : base(actual) { }
}