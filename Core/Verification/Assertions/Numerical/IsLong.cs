namespace XspecT.Verification.Assertions.Numerical;

public class IsLong : IsNumerical<IsLong, long>
{
    internal IsLong(long actual) : base(actual) { }
}