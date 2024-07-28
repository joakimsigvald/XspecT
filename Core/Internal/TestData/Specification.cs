using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit.Sdk;

namespace XspecT.Internal.TestData;

/// <summary>
/// 
/// </summary>
public static partial class Specification
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
        sb.Append($"given {typeof(TService).Name}.{expression.GetMethodName()}");
        AddMethodArguments(sb, expression.Body as MethodCallExpression);
        AddPhrase(sb.ToString());
    }

    internal static void AddMockReturns(string returnsExpr)
    {
        var sb = new StringBuilder();
        sb.Append("returns ");
        sb.Append(returnsExpr.ParseReturnsExpression());
        AddWord(sb.ToString());
    }

    internal static void AddWhen<TSUT, TResult>(Expression<Func<TSUT, TResult>> expression)
    {
        var sb = new StringBuilder();
        sb.Append($"when {expression.GetMethodName()}");
        AddMethodArguments(sb, expression.Body as MethodCallExpression);
        AddPhrase(sb.ToString());
    }

    internal static void AddAssert(Action assert, string actual = null, [CallerMemberName] string verb = "")
    {
        AddWord(actual.ParseActual());
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
        sb.Append('(');
        foreach (var argument in body.Arguments)
            sb.Append(DescribeArgument(argument));
        sb.Append(')');
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
            MethodCallExpression mce => $"{mce.Method.Name.AsWords()} {mce.Method.ReturnType.Alias()}",
            UnaryExpression ue => DescribeArgument(ue.Operand),
            MemberExpression => "TODO",
            ParameterExpression => "TODO",
            ConstantExpression => "TODO",
            LambdaExpression le => DescribeLambdaExpression(le),
            _ => throw new SetupFailed($"Unknown argument expression: {expr.NodeType}")
        };

    private static string DescribeLambdaExpression(LambdaExpression expr)
    {
        var body = expr.Body as MethodCallExpression;
        var methodName = DescribeArgument(body);
        return body.Arguments.Count == 0 
            ? methodName 
            : $"{methodName} {{ {DescribeCriteria(body.Arguments[0] as UnaryExpression)} }}";
    }

    private static string DescribeCriteria(UnaryExpression criteria)
    {
        var operand = criteria.Operand as LambdaExpression;
        var body = operand.Body as BinaryExpression;
        var left = body.Left as MemberExpression;
        var prop = left.Member as PropertyInfo;
        return $"{prop.Name} = {DescribeArgument(body.Right)}";
    }

    private static bool HasText => _specificationBuilder is not null;
    private static StringBuilder Builder => _specificationBuilder ??= new();
}