using AutoFixture;
using AutoFixture.Kernel;
using Moq;
using Moq.AutoMock;
using Moq.AutoMock.Resolvers;
using XspecT.Fixture.Exceptions;

namespace XspecT.Internal;

internal class TestDataGenerator
{
    private readonly IDictionary<Type, object> _usings = new Dictionary<Type, object>();
    private readonly IFixture _fixture = CreateAutoFixture();
    private readonly AutoMocker _mocker;

    internal TestDataGenerator() => _mocker = CreateAutoMocker(new FluentDefaultProvider(this));

    internal TValue Create<TValue>()
        => typeof(TValue).IsInterface
        ? _mocker.Get<TValue>()
        : _fixture.Create<TValue>();

    internal bool TryUse(Type type, out object val) => _usings.TryGetValue(type, out val);

    internal void Use(Type type, object value)
    {
        _usings[type] = value;
        _mocker.Use(type, value);
    }

    internal TValue CreateInstance<TValue>() where TValue : class
    {
        try
        {
            return _mocker.CreateInstance<TValue>();
        }
        catch (ArgumentException ex) when (ex.Message.Contains("Did not find a best constructor for"))
        {
            throw new CreateSubjectUnderTestFailed(ex.Message.Split('`')[1], ex);
        }
    }

    internal object CreateDefaultValue(Type type)
    {
        try
        {
            return _fixture.Create(type, new SpecimenContext(_fixture));
        }
        catch (Exception ex)
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
    }

    internal Mock<TObject> GetMock<TObject>() where TObject : class => _mocker.GetMock<TObject>();

    private static IFixture CreateAutoFixture()
    {
        AutoFixture.Fixture fixture = new() { RepeatCount = 0 };
        var customization = new SupportMutableValueTypesCustomization();
        customization.Customize(fixture);
        return fixture;
    }

    private static AutoMocker CreateAutoMocker(DefaultValueProvider defaultProvider)
    {
        var autoMocker = new AutoMocker(MockBehavior.Loose, DefaultValue.Custom, defaultProvider, false);
        ReplaceArrayResolver(autoMocker);
        return autoMocker;
    }

    private static void ReplaceArrayResolver(AutoMocker autoMocker)
    {
        var resolverList = (List<IMockResolver>)autoMocker.Resolvers;
        var arrayResolverIndex = resolverList.FindIndex(_ => _.GetType() == typeof(ArrayResolver));
        if (arrayResolverIndex < 0)
            return;

        //Remove the Moq ArrayResolver, which create an array with one mocked element for reference types
        resolverList.RemoveAt(arrayResolverIndex);
        //replace it with ArrayResolver that creates empty array
        resolverList.Insert(arrayResolverIndex, new EmptyArrayResolver());
    }
}