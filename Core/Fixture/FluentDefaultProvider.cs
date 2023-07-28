using Moq;
using Moq.AutoMock;

namespace XspecT.Fixture;

public class FluentDefaultProvider : DefaultValueProvider
{
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
        if (typeof(Task).IsAssignableFrom(type) && type.GenericTypeArguments.Length > 0)
        {
            dynamic model = DefaultMocker.Get(type.GenericTypeArguments[0]);
            var res = Task.FromResult(model);
            return res;
        }
        return DefaultMocker.Get(type);
    }

    //protected override object GetDefaultValue(Type type, Mock mock)
    //    => type.IsAssignableFrom(mock.Object.GetType())
    //    ? mock.Object
    //    : type == typeof(Task)
    //    ? Task.CompletedTask
    //    : type.IsAssignableFrom(typeof(Task)) && type.GenericTypeArguments.Length > 0
    //    ? Task.FromResult(DefaultMocker.Get(type.GenericTypeArguments[0]))
    //    : DefaultMocker.Get(type);
}