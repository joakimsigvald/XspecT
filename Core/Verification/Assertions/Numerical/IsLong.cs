namespace XspecT.Verification.Assertions.Numerical;

public class IsLong : IsNumerical<IsLong, long>
{
    public IsLong(long actual) : base(actual) { }
}