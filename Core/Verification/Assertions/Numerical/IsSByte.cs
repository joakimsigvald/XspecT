namespace XspecT.Verification.Assertions.Numerical;

public class IsSByte : IsNumerical<IsSByte, sbyte>
{
    internal IsSByte(sbyte actual) : base(actual) { }
}