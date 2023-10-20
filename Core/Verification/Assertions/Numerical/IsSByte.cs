namespace XspecT.Verification.Assertions.Numerical;

public class IsSByte : IsNumerical<IsSByte, sbyte>
{
    public IsSByte(sbyte actual) : base(actual) { }
}