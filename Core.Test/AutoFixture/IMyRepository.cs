namespace XspecT.Test.AutoFixture;

public interface IMyRepository
{
    MyModel Get(int id);
    MyModel[] List();
}