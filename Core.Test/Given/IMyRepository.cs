namespace XspecT.Test.Given;

public interface IMyRepository
{
    MyModel GetModel();
    MyModel[] GetModels();
    int GetNextId();
    int[] GetIds();
}