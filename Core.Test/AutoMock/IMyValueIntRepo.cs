namespace XspecT.Test.AutoMock;

public interface IMyValueIntRepo
{
    void Set(int value);
    string Get(int value);
    Task SetAsync(int value);
    Task<string> GetAsync(int value);
}