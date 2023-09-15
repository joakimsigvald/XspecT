namespace XspecT.Test.Given;

public class MyService
{
    private readonly IMyRepository _repo;
    public MyService(IMyRepository repo) => _repo = repo;
    public MyModel GetModel() => _repo.GetModel();
    public MyModel[] GetModels() => _repo.GetModels();
    public static MyModel Echo(MyModel model) => model;
}