using NetArchTest.Rules;
using System.Reflection;
namespace XspecT.Architecture;

/// <summary>
/// TODO
/// </summary>
public class ClassesContinuation
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public PredicateListContinuation In(Assembly assembly) 
        => new(Types.InAssembly(assembly).That().AreClasses());
}