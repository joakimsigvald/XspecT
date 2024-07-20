using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace XspecT.Internal.TestData;

internal static class Specification
{
    [ThreadStatic]
    private static StringBuilder _specificationBuilder;
    [ThreadStatic]
    private static Stack<string> _fragments;

    internal static void Clear()
    {
        _fragments = null;
        _specificationBuilder = null;
    }

    internal static string Description => Builder.ToString().TrimStart().Capitalize();

    internal static void AddMockSetup<TService, TActualReturns>(Expression<Func<TService, TActualReturns>> expression)
    {
        var sb = new StringBuilder();
        sb.Append($"given {typeof(TService).Name} that {expression.GetName()}");
        AddMethodArguments(sb, expression.Body as MethodCallExpression);
        AddSection(sb.ToString());
    }

    internal static void AddMockReturns<TReturns>(Func<TReturns> returns)
    {
        var sb = new StringBuilder();
        sb.Append($"returns");
        sb.Append(DescribeArgument(returns.Method));
        AddSubSection(sb.ToString());
    }

    internal static void AddWhen<TSUT, TResult>(Expression<Func<TSUT, TResult>> expression)
    {
        var sb = new StringBuilder();
        sb.Append($"when {expression.GetName()}");
        AddMethodArguments(sb, expression.Body as MethodCallExpression);
        AddSection(sb.ToString());
    }

    internal static void AddMethodArguments(StringBuilder sb, MethodCallExpression body)
    {
        if (body is null)
            return;
        sb.Append(" with");
        foreach (var argument in body.Arguments)
            sb.Append(DescribeArgument(argument));
    }

    internal static void AddSection(string section)
    {
        if (HasText)
            Builder.Append(",");
        AddSubSection(section);
    }

    internal static void AddSubSection(string phrase)
    {
        Builder.Append(" ");
        Builder.Append(phrase);
    }

    internal static void PushStop() => PushFragment(null);

    internal static void PushFragment(string fragment) 
        => (_fragments ??= new()).Push(fragment);

    internal static void PopFragments()
    {
        while (_fragments?.Count > 0)
        {
            var fragment = _fragments.Pop();
            if (fragment is null)
                return;
            Builder.Append(fragment);
        }
    }

    private static string DescribeArgument(Expression expr)
        => expr switch
        {
            MethodCallExpression mce => $" {mce.Method.Name.ToLower()} {mce.Method.ReturnType.Alias()}",
            UnaryExpression ue => DescribeArgument(ue.Operand),
            MemberExpression me => "TODO",
            ParameterExpression pe => "TODO",
            ConstantExpression ce => "TODO",
            _ => throw new SetupFailed($"Unknown argument expression: {expr.NodeType}")
        };

    private static string DescribeArgument(MethodInfo method)
        => $" {method.Name.ToLower()} {method.ReturnType.Alias()}";

    private static bool HasText => _specificationBuilder is not null;
    private static StringBuilder Builder => _specificationBuilder ??= new();
}