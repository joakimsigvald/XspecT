using AutoFixture.Kernel;
using System.Reflection;

namespace XspecT.Internal.TestData;

internal class DefaultValueCustimization : ISpecimenBuilder
{
    private readonly DataProvider _context;

    public DefaultValueCustimization(DataProvider context) => _context = context;

    public object Create(object request, ISpecimenContext context) 
        => request is Type type && _context.TryGetDefault(type, out var val)
        || request is PropertyInfo prop && _context.TryGetDefault(prop.PropertyType, out val)
        ? val
        : new NoSpecimen();
}