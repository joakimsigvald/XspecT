namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided sbyte
/// </summary>
public class IsSByte : IsNumerical<IsSByte, sbyte>
{
    internal IsSByte(sbyte actual) : base(actual) { }
}