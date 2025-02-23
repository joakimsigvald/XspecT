using System.Runtime.CompilerServices;

namespace XspecT.Assert.Continuations.String;

/// <summary>
/// Object that allows an assertions to be made on the provided string
/// </summary>
public record DoesString : Constraint<string, DoesStringContinuation>
{
    /// <summary>
    /// Asserts that the string contains the expected string
    /// </summary>
    public ContinueWith<DoesStringContinuation> Contain(
        string expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => Assert(
            Describe(expected), 
            () => Xunit.Assert.Contains(expected, Actual), 
            expectedExpr, "", "contains").And();

    /// <summary>
    /// Asserts that the string does not contain the expected string
    /// </summary>
    public ContinueWith<DoesStringContinuation> NotContain(
        string expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => Assert(
            Describe(expected), 
            () => Xunit.Assert.DoesNotContain(expected, Actual!), 
            expectedExpr, "").And();

    /// <summary>
    /// Asserts that the string starts with a prefix
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<DoesStringContinuation> StartWith(
        string expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => Assert(
            Describe(expected),
            () => Xunit.Assert.StartsWith(expected, Actual), 
            expectedExpr, "", "starts with").And();

    /// <summary>
    /// Asserts that the string ends with a suffix
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<DoesStringContinuation> EndWith(
        string expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => Assert(() => Xunit.Assert.EndsWith(expected, Actual), expectedExpr, "ends with").And();

    internal override DoesStringContinuation Continue() => Create(Actual);
    
    private protected override string Describe(string value) 
        => value is null ? "null" : $"\"{value}\"" ?? "null";
}