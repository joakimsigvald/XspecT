using AutoFixture;
using Moq;
using Moq.AutoMock;
using Moq.AutoMock.Resolvers;

namespace XspecT.Internal.TestData;

internal static class DataGeneratorFactory
{
    internal static Fixture CreateAutoFixture(this DataProvider context)
    {
        Fixture fixture = new() { RepeatCount = 0 };
        fixture.Customizations.Add(new DefaultValueCustimization(context));
        fixture.Customizations.Add(new InterfaceCustomization(context));
        new SupportMutableValueTypesCustomization().Customize(fixture);
        return fixture;
    }

    internal static AutoMocker CreateAutoMocker(this DataProvider context)
    {
        var autoMocker = new AutoMocker(
            MockBehavior.Loose,
            DefaultValue.Custom,
            new FluentDefaultProvider(context),
            false);
        CustomizeResolvers(autoMocker, context);
        return autoMocker;
    }

    private static void CustomizeResolvers(AutoMocker autoMocker, DataProvider context)
    {
        var resolverList = (List<IMockResolver>)autoMocker.Resolvers;
        AddValueResolver();
        ReplaceArrayResolver();

        void AddValueResolver() =>
            resolverList.Insert(resolverList.Count - 1, new ValueResolver(context));

        void ReplaceArrayResolver()
            => resolverList[GetArrayResolverIndex()] = new EmptyArrayResolver();

        int GetArrayResolverIndex()
            => resolverList.FindIndex(_ => _.GetType() == typeof(ArrayResolver));
    }
}