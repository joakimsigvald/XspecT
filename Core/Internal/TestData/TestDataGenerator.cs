using AutoFixture;
using AutoFixture.Kernel;
using Moq;
using Moq.AutoMock;
using Moq.AutoMock.Resolvers;

namespace XspecT.Internal.TestData;

internal class TestDataGenerator
{
    private readonly Fixture _fixture = CreateAutoFixture();
    private readonly AutoMocker _mocker;
    private readonly Context _context;

    internal TestDataGenerator(Context context)
        => _mocker = CreateAutoMocker(new FluentDefaultProvider(_context = context));

    internal TValue Create<TValue>()
        => typeof(TValue).IsInterface ? _mocker.Get<TValue>() : CreateValue<TValue>();

    internal void Use(Type type, object value) => _mocker.Use(type, value);

    internal TValue CreateInstance<TValue>() where TValue : class
    {
        try
        {
            return _mocker.CreateInstance<TValue>();
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

    private static Fixture CreateAutoFixture()
    {
        Fixture fixture = new() { RepeatCount = 0 };
        var customization = new SupportMutableValueTypesCustomization();
        customization.Customize(fixture);
        return fixture;
    }

    private AutoMocker CreateAutoMocker(DefaultValueProvider defaultProvider)
    {
        var autoMocker = new AutoMocker(MockBehavior.Loose, DefaultValue.Custom, defaultProvider, false);
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