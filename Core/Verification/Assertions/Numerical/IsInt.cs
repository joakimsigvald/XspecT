namespace XspecT.Verification.Assertions.Numerical;

public class IsInt : IsNumerical<IsInt, int>
{
    internal IsInt(int actual) : base(actual) { }
}