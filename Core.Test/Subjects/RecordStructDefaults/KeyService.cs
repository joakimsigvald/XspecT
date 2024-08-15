namespace XspecT.Test.Subjects.RecordStructDefaults;

public class KeyService
{
    readonly IKeyProvider _keyProvider;

    public KeyService(IKeyProvider keyProvider) => _keyProvider = keyProvider;

    public Key<string, string> GetKey() => _keyProvider.GetKey();
}