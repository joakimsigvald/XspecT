using System.Reflection;
namespace XspecT.Architecture;

/// <summary>
/// TODO
/// </summary>
public interface IClassesContinuation
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    PredicateListContinuation In(Assembly assembly);
}