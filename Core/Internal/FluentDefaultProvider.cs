using AutoFixture.Kernel;
using Moq;
using Moq.AutoMock;

namespace XspecT.Internal;

internal class FluentDefaultProvider : DefaultValueProvider
{
    internal AutoMocker Mocker { private get; set; }

    private readonly AutoFixture.Fixture _fixture;

    internal FluentDefaultProvider(AutoFixture.Fixture fixture) => _fixture = fixture;

    protected override object GetDefaultValue(Type type, Mock mock)
    {
        if (type.IsAssignableFrom(mock.Object.GetType()))
        {
            return mock.Object;
        }
        if (type == typeof(Task))
        {
            return Task.CompletedTask;
        }
        if (typeof(Task).IsAssignableFrom(type))
        {
            var genericType = type.GenericTypeArguments.Single();
            dynamic model = GetTaskValue(genericType);
            var res = Task.FromResult(model);
            return res;
        }
        return Create(type);
    }

    private dynamic GetTaskValue(Type type)
    {
        dynamic model = Mocker.Get(type);
        return model.GetType() == type ? model : Create(type);
    }

    private object Create(Type type) => _fixture.Create(type, new SpecimenContext(_fixture));
}