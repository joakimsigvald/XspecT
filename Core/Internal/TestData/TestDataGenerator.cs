using AutoFixture;
using AutoFixture.Kernel;
using Moq;
using Moq.AutoMock;

namespace XspecT.Internal.TestData;

internal class TestDataGenerator
{
    private readonly AutoMocker _mocker;
    private readonly Fixture _fixture;

    internal TestDataGenerator(Fixture fixture, AutoMocker mocker)
    {
        _mocker = mocker;
        _fixture = fixture;
    }

    internal TValue Create<TValue>()
        => typeof(TValue).IsInterface ? _mocker.Get<TValue>() : CreateValue<TValue>();

    internal void Use<TService>(TService service)
    {
        _mocker.Use(service);
        var type = typeof(TService);
        if (type != service.GetType()) //Explicit cast was provided, so don't use implicit cast to all interfaces
            return;
        var allInterfaces = type.GetInterfaces();
        foreach (var anInterface in allInterfaces)
            _mocker.Use(anInterface, service);
    }

    internal TValue Instantiate<TValue>()
    {
        try
        {
            return (TValue)_mocker.CreateInstance(typeof(TValue));
        }
        catch (ArgumentException ex) when (ex.Message.Contains("Did not find a best constructor for"))
        {
            return (TValue)Create(typeof(TValue));
        }
    }

    internal object Create(Type type)
    {
        try
        {
            return _fixture.Create(type, new SpecimenContext(_fixture));
        }
        catch (Exception ex)
        {
            return CreateDefaultValue(type, ex);
        }
    }

    private TValue CreateValue<TValue>()
    {
        try
        {
            return _fixture.Create<TValue>();
        }
        catch (Exception ex)
        {
            return (TValue)CreateDefaultValue(typeof(TValue), ex);
        }
    }

    private static object CreateDefaultValue(Type type, Exception ex)
    {
        try
        {
            return Activator.CreateInstance(type);
        }
        catch (ArgumentException)
        {
            throw new SetupFailed($"Failed to create value for type {type.Name}", ex);
        }
    }

    internal Mock<TObject> GetMock<TObject>() where TObject : class => _mocker.GetMock<TObject>();
    internal Mock GetMock(Type type) => _mocker.GetMock(type);
}