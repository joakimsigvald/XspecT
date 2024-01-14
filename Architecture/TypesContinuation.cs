using NetArchTest.Rules;
using System.Reflection;
namespace XspecT.Architecture;

/// <summary>
/// TODO
/// </summary>
public class TypesContinuation 
{ 
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public Types In(Assembly assembly) => Types.InAssembly(assembly);
}