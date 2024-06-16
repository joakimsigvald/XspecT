using AutoFixture;
using AutoFixture.Kernel;
using Moq;
using Moq.AutoMock;
using Moq.AutoMock.Resolvers;

namespace XspecT.Internal.TestData;

internal class TestDataGenerator
{
    private readonly Fixture _fixture;
    private readonly AutoMocker _mocker;
    private readonly Context _context;

    internal TestDataGenerator(Context context)
    {
        _context = context;
        _mocker = CreateAutoMocker();
        _fixture = CreateAutoFixture();
    }

    internal TValue Create<TValue>()
        => typeof(TValue).IsInterface ? _mocker.Get<TValue>() : CreateValue<TValue>();

    internal void Use(Type type, object value) => _mocker.Use(type, value);

    internal TValue Instantiate<TValue>()
    {
        try
        {
            var type = typeof(TValue);
            var instance = _context.TryGetDefault(typeof(TValue), out var val)
                ? val
                : _mocker.CreateInstance(typeof(TValue));
            return (TValue)_context.ApplyDefaultSetup(type, instance);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("Did not find a best constructor for"))
        {
            throw new SetupFailed($"Failed to auto-generate mock for {ex.Message.Split('`')[1]}.", ex);
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
        catch
        {
            throw new SetupFailed($"Failed to create value for type {type.Name}", ex);
        }
    }

    internal Mock<TObject> GetMock<TObject>() where TObject : class => _mocker.GetMock<TObject>();
    internal Mock GetMock(Type type) => _mocker.GetMock(type);

    private Fixture CreateAutoFixture()
    {
        Fixture fixture = new() { RepeatCount = 0 };
        fixture.Customizations.Add(new DefaultValueCustimization(_context));
        fixture.Customizations.Add(new InterfaceCustimization(_context));
        new SupportMutableValueTypesCustomization().Customize(fixture);
        return fixture;
    }

    private AutoMocker CreateAutoMocker()
    {
        var autoMocker = new AutoMocker(
            MockBehavior.Loose,
            DefaultValue.Custom,
            new FluentDefaultProvider(_context),
            false);
        CustomizeResolvers(autoMocker);
        return autoMocker;
    }

    private void CustomizeResolvers(AutoMocker autoMocker)
    {
        var resolverList = (List<IMockResolver>)autoMocker.Resolvers;
        AddValueResolver();
        ReplaceArrayResolver();

        void AddValueResolver() =>
            resolverList.Insert(resolverList.Count - 1, new ValueResolver(_context));

        void ReplaceArrayResolver()
            => resolverList[GetArrayResolverIndex()] = new EmptyArrayResolver();

        int GetArrayResolverIndex()
            => resolverList.FindIndex(_ => _.GetType() == typeof(ArrayResolver));
    }
}