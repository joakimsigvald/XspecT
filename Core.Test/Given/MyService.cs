namespace XspecT.Test.Given;

public class MyService
{
    private readonly IMyRepository _repo;
    private readonly IMySettings _settings;
    private readonly Func<DateTime> _getTime;

    public MyService(IMyRepository repo, IMySettings settings, Func<DateTime> getTime)
    {
        _repo = repo;
        _settings = settings;
        _getTime = getTime;
    }

    public string GetConnectionString() => _settings.ConnectionString;
    public MyModel GetModel() => _repo.GetModel();
    public MyModel[] GetModels() => _repo.GetModels();
    public int GetNextId() => _repo.GetNextId();
    public DateTime GetTime() => _getTime();
    public int[] GetIds() => _repo.GetIds();

    public static MyModel Echo(MyModel model) => model;
}