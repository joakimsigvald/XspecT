namespace XspecT.Test.Given;

public interface IMyRepository
{
    MyModel GetModel();
    MyModel SetModel(MyModel model);
    MyModel[] GetModels();
    int GetNextId();
    int[] GetIds();
}