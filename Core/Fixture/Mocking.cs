using Moq;
using Moq.AutoMock;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using XspecT.Fixture.Exceptions;
using XspecT.Internal;

namespace XspecT.Fixture;

/// <summary>
/// Not intended for direct override. Override one of TestStatic, TestStaticAsync, TestSubject or TestSubjectAsync instead
/// </summary>
public abstract class Mocking : Verification.IMocking
{
    protected readonly AutoMocker Mocker;
    private readonly Context _context;

    protected Mocking()
    {
        CultureInfo.CurrentCulture = GetCulture();
        var defaultProvider = new FluentDefaultProvider();
        Mocker = new(MockBehavior.Loose, DefaultValue.Custom, defaultProvider, false);
        _context = new Context(Mocker);
        defaultProvider.SetContext(_context);
    }

    /// <summary>
    /// Get the mock of the given type
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <returns></returns>
    [Obsolete("Use Given<TService>(_ => _.Setup... instead)")]
    public Mock<TObject> TheMocked<TObject>() where TObject : class => Mocker.GetMock<TObject>();

    /// <summary>
    /// Alias for A
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue The<TValue>() => _context.Retreive<TValue>(0);

    /// <summary>
    /// Alias for A
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue An<TValue>() => A<TValue>();
    protected TValue An<TValue>([NotNull] Action<TValue> setup) => A(setup);

    /// <summary>
    /// Yields a new value of the given type, or same value as previously mentioned, including as part of a Using clause
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue A<TValue>() => _context.Mention<TValue>(null, 0);
    protected TValue A<TValue>([NotNull] Action<TValue> setup) => _context.Mention(setup, 0);

    /// <summary>
    /// Will always yield a new model of the given type, unless TValue is an interface. 
    /// Do not use in combination with Using or reference the generated value twice in the same pipeline, 
    /// since that might give the specification confusing semantics
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue Another<TValue>() => _context.Create<TValue>(null);
    protected TValue Another<TValue>([NotNull] Action<TValue> setup) => _context.Create(setup);

    /// <summary>
    /// Alias for ASecond
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue TheSecond<TValue>() => _context.Retreive<TValue>(1);

    /// <summary>
    /// Yields a new value of the given type, or same value as previously mentioned as second value
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue ASecond<TValue>() => _context.Mention<TValue>(null, 1);
    protected TValue ASecond<TValue>([NotNull] Action<TValue> setup) => _context.Mention(setup, 1);

    /// <summary>
    /// Alias for AThird
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue TheThird<TValue>() => _context.Retreive<TValue>(2);

    /// <summary>
    /// Yields a new value of the given type, or same value as previously mentioned as third value
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue AThird<TValue>() => _context.Mention<TValue>(null, 2);
    protected TValue AThird<TValue>([NotNull] Action<TValue> setup) => _context.Mention(setup, 2);

    /// <summary>
    /// Alias for AFourth
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue TheFourth<TValue>() => _context.Retreive<TValue>(3);

    /// <summary>
    /// Yields a new value of the given type, or same value as previously mentioned as fourth value
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue AFourth<TValue>() => _context.Mention<TValue>(null, 3);
    protected TValue AFourth<TValue>([NotNull] Action<TValue> setup) => _context.Mention(setup, 3);

    /// <summary>
    /// Alias for AFifth
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue TheFifth<TValue>() => _context.Retreive<TValue>(4);

    /// <summary>
    /// Yields a new value of the given type, or same value as previously mentioned as fifth value
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue AFifth<TValue>() => _context.Mention<TValue>(null, 4);
    protected TValue AFifth<TValue>([NotNull] Action<TValue> setup) => _context.Mention(setup, 4);

    /// <summary>
    /// Override this to set different Culture than InvariantCulture during test
    /// </summary>
    /// <returns></returns>
    protected virtual CultureInfo GetCulture() => CultureInfo.InvariantCulture;

    internal protected void Use<TService>(TService service)
    {
        var type = typeof(TService);
        _context.Use(typeof(TService), service);
        if (typeof(Task).IsAssignableFrom(type))
            return;
        if (typeof(Mock).IsAssignableFrom(type))
            return;
        Use(Task.FromResult(service));
    }

    protected internal TValue CreateInstance<TValue>() where TValue : class
    {
        try
        {
            return Mocker.CreateInstance<TValue>();
        }
        catch (ArgumentException ex) when (ex.Message.Contains("Did not find a best constructor for"))
        {
            throw new CreateSubjectUnderTestFailed(ex.Message.Split('`')[1], ex);
        }
    }
}