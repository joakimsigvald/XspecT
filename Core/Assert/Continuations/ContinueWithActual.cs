namespace XspecT.Assert.Continuations;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TActual"></typeparam>
/// <param name="actual"></param>
public class ContinueWithActual<TActual>(TActual? actual)
{
    /// <summary>
    /// Continuation to apply additional assertions on the value
    /// </summary>
    public TActual? And => actual;

    /// <summary>
    /// Continuation to apply additional assertions on the value
    /// </summary>
    public TActual? But => actual;
}