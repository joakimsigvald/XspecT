using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using XspecT.Assert;

namespace XspecT.Test.AutoMock;

public class WhenGetRecordWithBsonIdFromMock : Spec<BsonIdService, RecordMongoDb>
{
    public WhenGetRecordWithBsonIdFromMock() => Given<RecordMongoDb>(_ => _.Value = "123").When(_ => _.GetRecord());
    [Fact]
    public void ThenGetRecord()
    {
        Then().Result.Is().NotNull();
        Specification.Is(
            """
            Given RecordMongoDb { Value = "123" }
            When _.GetRecord()
            Then Result is not null
            """);
    }
}

public class BsonIdService(IBsonIdRepository repo)
{
    private readonly IBsonIdRepository _repo = repo;
    public Task<RecordMongoDb> GetRecord() => _repo.GetRecord();
}

public interface IBsonIdRepository
{
    Task<RecordMongoDb> GetRecord();
}

public class RecordMongoDb
{
    [BsonId] public ObjectId Id { get; set; }
    public string Value { get; set; }
}