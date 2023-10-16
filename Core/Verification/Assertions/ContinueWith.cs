namespace XspecT.Verification.Assertions;

public class ContinueWith<TContinuation>
{
    private readonly TContinuation _continuation;
    public ContinueWith(TContinuation continuation) => _continuation = continuation;
    public TContinuation And => _continuation;
}