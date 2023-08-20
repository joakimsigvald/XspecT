using XspecT.Fixture;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using XspecT.Verification;

namespace XspecT.Test.AutoMock;

public class WhenGetRecordWithBsonIdFromMock : SubjectSpec<BsonIdService, RecordMongoDb>
{
    public WhenGetRecordWithBsonIdFromMock() => When(_ => _.GetRecord());
    [Fact] public void ThenGetRecord() => Then().Result.Is().NotNull();
}

public class BsonIdService
{
    private readonly IBsonIdRepository _repo;
    public BsonIdService(IBsonIdRepository repo) => _repo = repo;
    public Task<RecordMongoDb> GetRecord() => _repo.GetRecord();
}

public interface IBsonIdRepository
{
    Task<RecordMongoDb> GetRecord();
}

public class RecordMongoDb
{
    [BsonId] public ObjectId Id { get; set; }
}