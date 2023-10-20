namespace XspecT.Verification.Assertions.Numerical;

public class IsShort : IsNumerical<IsShort, short>
{
    public IsShort(short actual) : base(actual) { }
}