using System.Reflection;

namespace XspecT.Architecture.Test;

public class InvalidImplementation : IClassesContinuation
{
    public PredicateListContinuation In(Assembly assembly)
    {
        throw new NotImplementedException();
    }
}