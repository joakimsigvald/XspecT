namespace XspecT.Verification.Assertions.Numerical;

public class IsULong : IsNumerical<IsULong, ulong>
{
    public IsULong(ulong actual) : base(actual) { }
}