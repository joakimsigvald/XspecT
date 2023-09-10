namespace XspecT.Test.AutoFixture;

public class MyRetreiver
{
    private readonly IMyRepository _repository;

    public MyRetreiver(IMyRepository repository) => _repository = repository;

    public MyModel Get(int id) => _repository.Get(id);
    public MyModel[] List() => _repository.List();
}