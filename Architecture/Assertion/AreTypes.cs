using NetArchTest.Rules;
using XspecT.Architecture.Exceptions;

namespace XspecT.Architecture.Assertion;

/// <summary>
/// TODO
/// </summary>
public class AreTypes : Constraint<AreTypes, Conditions>
{
    internal AreTypes(Conditions actual) : base(actual) { }

    /// <summary>
    /// TODO
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArchitectureViolation"></exception>
    public ContinueWith<AreTypes> Sealed()
    {
        var result = _actual.BeSealed().GetResult();
        if (!result.IsSuccessful)
            throw new ArchitectureViolation($"The following {result.FailingTypes.Count} classes are not sealed: "
                + string.Join(", ", result.FailingTypeNames));
        return And();
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArchitectureViolation"></exception>
    public ContinueWith<AreTypes> NotSealed()
    {
        var result = _actual.NotBeSealed().GetResult();
        if (!result.IsSuccessful)
            throw new ArchitectureViolation($"The following {result.FailingTypes.Count} classes are sealed: "
                + string.Join(", ", result.FailingTypeNames));
        return And();
    }
}