namespace XspecT.Test.AutoFixture;

public class MyMappingRetreiver
{
    private readonly IMyRepository _repository;
    private readonly IMyMapper _mapper;

    public MyMappingRetreiver(IMyRepository repository, IMyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public MyModel Get(int id) => _mapper.Map(_repository.Get(id));
    public MyModel[] List() => _repository.List().Select(_mapper.Map).ToArray();
}