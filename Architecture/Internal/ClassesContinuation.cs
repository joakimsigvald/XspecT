using NetArchTest.Rules;
using System.Reflection;
namespace XspecT.Architecture.Internal;

/// <summary>
/// TODO
/// </summary>
internal class ClassesContinuation : IClassesContinuation
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public PredicateListContinuation In(Assembly assembly)
        => new(Types.InAssembly(assembly).That().AreClasses());
}