namespace XspecT.Architecture.Exceptions;

/// <summary>
/// TODO
/// </summary>
public class ArchitectureViolation : ApplicationException
{
    internal ArchitectureViolation(string message) : base(message) { }
}