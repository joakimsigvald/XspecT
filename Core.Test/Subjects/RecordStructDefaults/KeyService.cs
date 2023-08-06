namespace XspecT.Test.Subjects.RecordStructDefaults;

public class KeyService
{
    readonly IKeyProvider _keyProvider;

    public KeyService(IKeyProvider keyProvider) => _keyProvider = keyProvider;

    public Key GetKey() => _keyProvider.GetKey();
}