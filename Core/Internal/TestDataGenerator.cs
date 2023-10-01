using AutoFixture;
using AutoFixture.Kernel;
using Moq;
using Moq.AutoMock;
using Moq.AutoMock.Resolvers;
using XspecT.Fixture.Exceptions;
using XspecT.Internal.Resolvers;

namespace XspecT.Internal;

internal class TestDataGenerator
{
    private readonly IFixture _fixture = CreateAutoFixture();
    private readonly AutoMocker _mocker;
    private readonly Context _context;

    internal TestDataGenerator(Context context) 
        => _mocker = CreateAutoMocker(new FluentDefaultProvider(_context = context));

    internal TValue Create<TValue>()
        => typeof(TValue).IsInterface
        ? _mocker.Get<TValue>()
        : _fixture.Create<TValue>();

    internal void Use(Type type, object value) => _mocker.Use(type, value);

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

    private AutoMocker CreateAutoMocker(DefaultValueProvider defaultProvider)
    {
        var autoMocker = new AutoMocker(MockBehavior.Loose, DefaultValue.Custom, defaultProvider, false);
        AddCustomResolvers(autoMocker);
        return autoMocker;
    }

    private void AddCustomResolvers(AutoMocker autoMocker)
    {
        var resolverList = (List<IMockResolver>)autoMocker.Resolvers;
        ReplaceArrayResolver(resolverList);
        resolverList.Insert(resolverList.Count - 1, new ValueResolver(_context));
    }

    private static void ReplaceArrayResolver(List<IMockResolver> resolverList)
    {
        var arrayResolverIndex = resolverList.FindIndex(_ => _.GetType() == typeof(ArrayResolver));

        //Remove the Moq ArrayResolver, which create an array with one mocked element for reference types
        resolverList.RemoveAt(arrayResolverIndex);
        //replace it with ArrayResolver that creates empty array
        resolverList.Insert(arrayResolverIndex, new EmptyArrayResolver());
    }
}