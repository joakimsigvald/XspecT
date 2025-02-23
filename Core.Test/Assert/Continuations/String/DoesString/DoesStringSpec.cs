namespace XspecT.Test.Assert.Continuations.String.DoesString;

public abstract class DoesStringSpec : Spec
{
    protected static string Describe(string value) => value is null ? "null" : $"\"{value}\"";
}