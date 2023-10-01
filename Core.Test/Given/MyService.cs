namespace XspecT.Test.Given;

public class MyService
{
    private readonly IMyRepository _repo;
    private readonly IMySettings _settings;

    public MyService(IMyRepository repo, IMySettings settings)
    {
        _repo = repo;
        _settings = settings;
    }

    public string GetConnectionString() => _settings.ConnectionString;
    public MyModel GetModel() => _repo.GetModel();
    public MyModel[] GetModels() => _repo.GetModels();
    public static MyModel Echo(MyModel model) => model;
}