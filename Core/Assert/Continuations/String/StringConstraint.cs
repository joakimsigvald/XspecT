namespace XspecT.Assert.Continuations.String;

/// <summary>
/// Object that allows an assertions to be made on the provided string
/// </summary>
public abstract record StringConstraint<TContinuation> : Constraint<string, TContinuation>
    where TContinuation : StringConstraint<TContinuation>, new()
{
    private protected override string Describe(string value) 
        => value is null ? "null" : $"\"{value}\"" ?? "null";
}