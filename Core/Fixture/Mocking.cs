using Moq;
using Moq.AutoMock;
using Moq.AutoMock.Resolvers;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using XspecT.Fixture.Exceptions;
using XspecT.Internal;

namespace XspecT.Fixture;

/// <summary>
/// Not intended for direct override. Override one of TestStatic, TestStaticAsync, TestSubject or TestSubjectAsync instead
/// </summary>
public abstract class Mocking
{
    protected readonly AutoMocker Mocker;
    private readonly Context _context;

    protected Mocking()
    {
        CultureInfo.CurrentCulture = GetCulture();
        var defaultProvider = new FluentDefaultProvider();
        Mocker = CreateAutoMocker(defaultProvider);
        _context = new Context(Mocker);
        defaultProvider.SetContext(_context);
    }

    /// <summary>
    /// Alias for A
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue The<TValue>() => A<TValue>();

    /// <summary>
    /// Alias for A
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue An<TValue>() => A<TValue>();

    /// <summary>
    /// Alias for A
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue An<TValue>([NotNull] Action<TValue> setup) => A(setup);

    /// <summary>
    /// Yields a new value of the given type, or same value as previously mentioned, including as part of a Using clause
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue A<TValue>() => _context.Mention<TValue>(null, 0);

    /// <summary>
    /// Yields a new customized value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue A<TValue>([NotNull] Action<TValue> setup) => _context.Mention(setup, 0);

    /// <summary>
    /// Will always yield a new model of the given type, unless TValue is an interface. 
    /// Do not use in combination with Using or reference the generated value twice in the same pipeline, 
    /// since that might give the specification confusing semantics
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue Another<TValue>() => _context.Create<TValue>(null);

    /// <summary>
    /// Yields a new customized value of the given type, which cannot be reused
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue Another<TValue>([NotNull] Action<TValue> setup) => _context.Create(setup);

    /// <summary>
    /// Alias for ASecond
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue TheSecond<TValue>() => ASecond<TValue>();

    /// <summary>
    /// Yields a new value of the given type, or same value as previously mentioned as second value
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue ASecond<TValue>() => _context.Mention<TValue>(null, 1);

    /// <summary>
    /// Yields a new customized second value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue ASecond<TValue>([NotNull] Action<TValue> setup) => _context.Mention(setup, 1);

    /// <summary>
    /// Alias for AThird
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue TheThird<TValue>() => AThird<TValue>();

    /// <summary>
    /// Yields a new value of the given type, or same value as previously mentioned as third value
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue AThird<TValue>() => _context.Mention<TValue>(null, 2);

    /// <summary>
    /// Yields a new customized third value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue AThird<TValue>([NotNull] Action<TValue> setup) => _context.Mention(setup, 2);

    /// <summary>
    /// Alias for AFourth
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue TheFourth<TValue>() => AFourth<TValue>();

    /// <summary>
    /// Yields a new value of the given type, or same value as previously mentioned as fourth value
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue AFourth<TValue>() => _context.Mention<TValue>(null, 3);

    /// <summary>
    /// Yields a new customized fourth value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue AFourth<TValue>([NotNull] Action<TValue> setup) => _context.Mention(setup, 3);

    /// <summary>
    /// Alias for AFifth
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue TheFifth<TValue>() => AFifth<TValue>();

    /// <summary>
    /// Yields a new value of the given type, or same value as previously mentioned as fifth value
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue AFifth<TValue>() => _context.Mention<TValue>(null, 4);

    /// <summary>
    /// Yields a new customized fifth value of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue AFifth<TValue>([NotNull] Action<TValue> setup) => _context.Mention(setup, 4);

    /// <summary>
    /// Yields an array with one element of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue[] One<TValue>() => _context.MentionMany<TValue>(1);

    /// <summary>
    /// Yields an array with one customized element of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue[] One<TValue>([NotNull] Action<TValue> setup) => _context.MentionMany(setup, 1);

    /// <summary>
    /// Yields an array with two element of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue[] Two<TValue>() => _context.MentionMany<TValue>(2);

    /// <summary>
    /// Yields an array with two customized element of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue[] Two<TValue>([NotNull] Action<TValue> setup) => _context.MentionMany(setup, 2);

    /// <summary>
    /// Yields an array with two individually customized elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue[] Two<TValue>([NotNull] Action<TValue, int> setup) => _context.MentionMany(setup, 2);

    /// <summary>
    /// Yields an array with three element of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue[] Three<TValue>() => _context.MentionMany<TValue>(3);

    /// <summary>
    /// Yields an array with three customized element of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue[] Three<TValue>([NotNull] Action<TValue> setup) => _context.MentionMany(setup, 3);

    /// <summary>
    /// Yields an array with three individually customized elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue[] Three<TValue>([NotNull] Action<TValue, int> setup) => _context.MentionMany(setup, 3);

    /// <summary>
    /// Yields an array with four element of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue[] Four<TValue>() => _context.MentionMany<TValue>(4);

    /// <summary>
    /// Yields an array with four customized element of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue[] Four<TValue>([NotNull] Action<TValue> setup) => _context.MentionMany(setup, 4);

    /// <summary>
    /// Yields an array with four individually customized elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue[] Four<TValue>([NotNull] Action<TValue, int> setup) => _context.MentionMany(setup, 4);

    /// <summary>
    /// Yields an array with five element of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue[] Five<TValue>() => _context.MentionMany<TValue>(5);

    /// <summary>
    /// Yields an array with five customized element of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue[] Five<TValue>([NotNull] Action<TValue> setup) => _context.MentionMany(setup, 5);

    /// <summary>
    /// Yields an array with five individually customized elements of the given type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue[] Five<TValue>([NotNull] Action<TValue, int> setup) => _context.MentionMany(setup, 5);

    /// <summary>
    /// Alias for Three
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    protected TValue[] Many<TValue>() => Three<TValue>();

    /// <summary>
    /// Alias for Three
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue[] Many<TValue>([NotNull] Action<TValue> setup) => Three(setup);

    /// <summary>
    /// Alias for Three
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    protected TValue[] Many<TValue>([NotNull] Action<TValue, int> setup) => Three(setup);

    protected TValue The<TValue>(string label) => _context.Mention<TValue>(label);

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