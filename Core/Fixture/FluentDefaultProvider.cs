using Moq;
using Moq.AutoMock;
using XspecT.Fixture.Exceptions;

namespace XspecT.Fixture;

public class FluentDefaultProvider : DefaultValueProvider
{
    internal AutoMocker Mocker { private get; set; }
    internal AutoMocker DefaultMocker { get; init; }

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
            if (type.GenericTypeArguments.Length == 1)
            {
                var genericType = type.GenericTypeArguments[0];
                dynamic model = Mocker.Get(genericType);
                if (model.GetType() != genericType)
                    throw new SetupFailed($"Could not resolve generic type {genericType} of Task. Call Using to provide a default value");
                var res = Task.FromResult(model);
                return res;
            }
            throw new NotImplementedException("Unexpected type: Task with more than one generic arguments");
        }
        return DefaultMocker.Get(type);
    }
}
