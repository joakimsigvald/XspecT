using AutoFixture.Kernel;
using System.Reflection;

namespace XspecT.Internal.TestData;

internal class DefaultValueCustomization(DataProvider _context) : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        if (request is Type type && _context.TryGetDefault(type, out var val)
            || request is PropertyInfo prop && _context.TryGetDefault(prop.PropertyType, out val))
            return val;

        //object obj = context. composedBuilders[i].Create(request, context);
        //if (!(obj is NoSpecimen))
        //{
        //    return obj;
        //}


        return new NoSpecimen();
    }
}