namespace XspecT.Verification.Assertions.Numerical;

public class IsInt : IsNumerical<IsInt, int>
{
    public IsInt(int actual) : base(actual) { }
}