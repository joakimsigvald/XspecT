using NetArchTest.Rules;
using XspecT.Architecture.Exceptions;

namespace XspecT.Architecture.Assertion;

/// <summary>
/// TODO
/// </summary>
public class DoesTypes : Constraint<DoesTypes, Conditions>
{
    internal DoesTypes(Conditions actual) : base(actual) { }

    /// <summary>
    /// TODO
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArchitectureViolation"></exception>
    public ContinueWith<DoesTypes> NotImplement(PredicateListContinuation continuation)
    {
        var interfaces = continuation._predicateList.Should().NotBeInterfaces().GetResult().FailingTypes;
        if (interfaces != null)
            foreach (var interfaceType in interfaces)
            {
                var result = _actual.NotImplementInterface(interfaceType).GetResult();
                if (!result.IsSuccessful)
                    throw new ArchitectureViolation($"The following {result.FailingTypes.Count} classes implement interface {interfaceType.Name}: "
                        + string.Join(", ", result.FailingTypeNames));
            }
        return And();
    }
}