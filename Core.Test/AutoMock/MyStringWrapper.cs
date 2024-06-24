namespace XspecT.Test.AutoMock;

public class MyStringWrapper(string use) 
{
    public string GetStrings(string input) => use + input;
}