using AutoFixture.Kernel;

namespace XspecT.Internal.TestData;

internal class InterfaceCustimization : ISpecimenBuilder
{
    private readonly Context _context;

    internal InterfaceCustimization(Context context) => _context = context;

    public object Create(object request, ISpecimenContext context)
    {
        if (request is Type type && type.IsInterface)
        {
            var (val, found) = _context.Use(type);
            try
            {
                return found ? val : _context.GetMock(type).Object;
            }
            catch { }
        }
        return new NoSpecimen();
    }
}