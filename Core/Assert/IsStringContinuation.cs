namespace XspecT.Assert;

/// <summary>
/// Object that allows another assertions to be made on the provided string
/// </summary>
public record IsStringContinuation : IsString
{
    /// <summary>
    /// Continuation to assert that the string does satisfy some expectation
    /// </summary>
    /// <returns></returns>
    public DoesString Does() => Actual.Does(actualExpr: ActualExpr);
}