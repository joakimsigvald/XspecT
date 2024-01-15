using System.Reflection;
namespace XspecT.Architecture;

/// <summary>
/// TODO
/// </summary>
public class InterfacesContinuation
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public PredicateListContinuation In(Assembly assembly)
        => new(NetArchTest.Rules.Types.InAssembly(assembly).That().AreInterfaces());
}