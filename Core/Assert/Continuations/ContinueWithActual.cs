namespace XspecT.Assert.Continuations;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TActual"></typeparam>
/// <param name="actual"></param>
public class ContinueWithActual<TActual>(TActual? actual)
{
    /// <summary>
    /// 
    /// </summary>
    public TActual? And => actual;
    /// <summary>
    /// 
    /// </summary>
    public TActual? But => actual;
}