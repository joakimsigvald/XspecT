namespace XspecT.Verification.Assertions.Numerical;

public class IsShort : IsNumerical<IsShort, short>
{
    internal IsShort(short actual) : base(actual) { }
}