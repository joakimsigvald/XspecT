using System.Reflection;

namespace XspecT.Architecture.Test;

public class InvalidImplementation : IAssemblyReference
{
    public PredicateListContinuation Classes => throw new NotImplementedException();
    public PredicateListContinuation Interfaces => throw new NotImplementedException();
    public Assembly Project => throw new NotImplementedException();
    public Assembly Assembly => throw new NotImplementedException();
    public void DependOn(string otherName) => throw new NotImplementedException();
    public void DoNotDependOn(string otherName) => throw new NotImplementedException();
    public void Use(string otherName) => throw new NotImplementedException();
    public void DoNotUse(string otherName) => throw new NotImplementedException();
}