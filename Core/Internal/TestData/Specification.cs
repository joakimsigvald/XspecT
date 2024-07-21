using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit.Sdk;

namespace XspecT.Internal.TestData;

/// <summary>
/// 
/// </summary>
public static class Specification
{
    [ThreadStatic]
    private static StringBuilder _specificationBuilder;
    [ThreadStatic]
    private static Stack<string> _words;

    internal static void Clear()
    {
        _words = null;
        _specificationBuilder = null;
    }

    /// <summary>
    /// 
    /// </summary>
    public static string Description => Builder.ToString().TrimStart().Capitalize();

    internal static void AddMockSetup<TService, TActualReturns>(Expression<Func<TService, TActualReturns>> expression)
    {
        var sb = new StringBuilder();
        sb.Append($"given {typeof(TService).Name} that {expression.GetMethodName()}");
        AddMethodArguments(sb, expression.Body as MethodCallExpression);
        AddPhrase(sb.ToString());
    }

    internal static void AddMockReturns<TReturns>(Func<TReturns> returns)
    {
        var sb = new StringBuilder();
        sb.Append($"returns");
        sb.Append(DescribeArgument(returns.Method));
        AddWord(sb.ToString());
    }

    internal static void AddWhen<TSUT, TResult>(Expression<Func<TSUT, TResult>> expression)
    {
        var sb = new StringBuilder();
        sb.Append($"when {expression.GetMethodName()}");
        AddMethodArguments(sb, expression.Body as MethodCallExpression);
        AddPhrase(sb.ToString());
    }

    internal static void AddAssert(Action assert, [CallerMemberName] string verb = "")
    {
        AddWord(verb.AsWords());
        PopWords();
        try
        {
            assert();
        }
        catch (XunitException ex)
        {
            throw new XunitException(Description, ex);
        }
    }

    internal static void AddMethodArguments(StringBuilder sb, MethodCallExpression body)
    {
        if (body is null)
            return;
        sb.Append(" with");
        foreach (var argument in body.Arguments)
            sb.Append(DescribeArgument(argument));
    }

    internal static void AddPhrase(string phrase)
    {
        if (HasText)
            Builder.Append($",{Environment.NewLine}");
        AddWord(phrase);
    }

    internal static void AddWord(string word)
    {
        Builder.Append(' ');
        Builder.Append(word);
    }

    internal static void PushStop() => PushWord(null);

    internal static void PushWord(string fragment) 
        => (_words ??= new()).Push(fragment);

    internal static void PopWords()
    {
        while (_words?.Count > 0)
        {
            var word = _words.Pop();
            if (word is null)
                return;
            AddWord(word);
        }
    }

    private static string DescribeArgument(Expression expr)
        => expr switch
        {
            MethodCallExpression mce => $" {mce.Method.Name.ToLower()} {mce.Method.ReturnType.Alias()}",
            UnaryExpression ue => DescribeArgument(ue.Operand),
            MemberExpression => "TODO",
            ParameterExpression => "TODO",
            ConstantExpression => "TODO",
            _ => throw new SetupFailed($"Unknown argument expression: {expr.NodeType}")
        };

    private static string DescribeArgument(MethodInfo method)
        => $" {method.Name.AsWords()} {method.ReturnType.Alias()}";

    private static bool HasText => _specificationBuilder is not null;
    private static StringBuilder Builder => _specificationBuilder ??= new();
}