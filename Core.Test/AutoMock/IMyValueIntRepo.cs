namespace XspecT.Test.AutoMock;

public interface IMyValueIntRepo
{
    void Set(int value);
    string Get(int value);
    Task SetAsync(int value);
    Task<string> GetAsync(int value);
    IMyValueIntRepo GetMe();
    Task<IMyValueIntRepo> GetMeAsync();
    object GetObject();
    Task<object> GetObjectAsync();
}