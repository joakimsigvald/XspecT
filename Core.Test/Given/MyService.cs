namespace XspecT.Test.Given;

public class MyService(IMyRepository repo, IMySettings settings, Func<DateTime> getTime)
{
    public string GetConnectionString() => settings.ConnectionString;
    public MyModel GetModel() => repo.GetModel();
    public MyModel[] GetModels() => repo.GetModels();
    public int GetNextId() => repo.GetNextId();
    public DateTime GetTime() => getTime();
    public int[] GetIds() => repo.GetIds();
    public MyModel Echo(MyModel model) => model;
}