namespace XspecT.Verification.Assertions;

public class ContinueWith<TContinuation>
{
    private readonly TContinuation _continuation;
    internal ContinueWith(TContinuation continuation) => _continuation = continuation;

    /// <summary>
    /// TODO
    /// </summary>
    public TContinuation And => _continuation;
}