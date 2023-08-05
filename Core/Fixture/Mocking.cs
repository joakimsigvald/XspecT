using Moq;
using Moq.AutoMock;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using XspecT.Internal;

namespace XspecT.Fixture;

/// <summary>
/// Not intended for direct override. Override one of TestStatic, TestStaticAsync, TestSubject or TestSubjectAsync instead
/// </summary>
public abstract class Mocking : Verification.IMocked
{
    private readonly AutoMocker _mocker;
    private readonly AutoFixture.Fixture _fixture = new();
    private readonly IDictionary<Type, object> _usings = new Dictionary<Type, object>();

    protected Mocking()
    {
        CultureInfo.CurrentCulture = GetCulture();
        var defaultProvider = new FluentDefaultProvider(_fixture, _usings);
        _mocker = new(MockBehavior.Loose, DefaultValue.Custom, defaultProvider, false);
    }

    public Mock<TObject> The<TObject>() where TObject : class => _mocker.GetMock<TObject>();

    /// <summary>
    /// Override this to set different Culture than InvariantCulture during test
    /// </summary>
    /// <returns></returns>
    protected virtual CultureInfo GetCulture() => CultureInfo.InvariantCulture;

    internal protected void Use<TService>([DisallowNull] TService service)
        => Use(typeof(TService), service ?? throw new ArgumentNullException(nameof(service)));
    internal protected void Use(Type type, object value)
    {
        _usings[type] = value;
        _mocker.Use(type, value);
    }

    internal protected TValue CreateInstance<TValue>() where TValue : class => _mocker.CreateInstance<TValue>();
}