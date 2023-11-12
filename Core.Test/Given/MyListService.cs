namespace XspecT.Test.Given;

public class MyListService
{
    private readonly List<int> _list;
    public MyListService(List<int> list) => _list = list;
    public List<int> List => _list;
}