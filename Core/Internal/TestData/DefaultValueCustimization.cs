using AutoFixture.Kernel;
using System.Reflection;

namespace XspecT.Internal.TestData;

internal class DefaultValueCustimization : ISpecimenBuilder
{
    private readonly Context _context;

    public DefaultValueCustimization(Context context) => _context = context;

    public object Create(object request, ISpecimenContext context) 
        => request is Type type && TryGetDefault(type, out var val)
        || request is PropertyInfo prop && TryGetDefault(prop.PropertyType, out val)
        ? val
        : new NoSpecimen();

    private bool TryGetDefault(Type type, out object val) => _context.TryGetDefault(type, out val);
}