using XspecT.Architecture.Internal;

namespace XspecT.Architecture;

/// <summary>
/// TODO
/// </summary>
public class ArchSpec
{
    /// <summary>
    /// TODO
    /// </summary>
    protected AssemblyContinuation Assembly => new();

    /// <summary>
    /// TODO
    /// </summary>
    protected IClassesContinuation Classes => new ClassesContinuation();

    /// <summary>
    /// TODO
    /// </summary>
    protected InterfacesContinuation Interfaces => new();
}