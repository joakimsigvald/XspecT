namespace XspecT.Test.AutoFixture;

public class Retreiver
{
    private readonly IRepository _repository;
    public Retreiver(IRepository repository) => _repository = repository;
    public Model Get(int id) => _repository.Get(id);
    public Model[] List() => _repository.List();
}