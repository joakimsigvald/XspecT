namespace XspecT.Test.AutoFixture;

public interface IRepository
{
    Model Get(int id);
    Model[] List();
}