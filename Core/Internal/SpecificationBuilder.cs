using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using XspecT.Internal.TestData;

namespace XspecT.Internal;

internal class SpecificationBuilder
{
    private string _description;
    private readonly List<Action> _applications = [];
    private readonly StringBuilder _descriptionBuilder = new();
    private int _givenCount;

    public string Description => _description ??= Build();

    internal void Add(Action apply) => _applications.Add(apply);

    internal string Build()
    {
        foreach (var apply in _applications) apply();
        return _descriptionBuilder.ToString().Trim(',').Trim().Capitalize();
    }

    internal void AddMockSetup<TService>(string callExpr)
        => AddPhrase($"{Given} {typeof(TService).Name}.{callExpr.ParseCall()}");

    internal void AddMockReturns(string returnsExpr)
        => AddWord($"returns {returnsExpr.ParseValue()}");

    internal void AddWhen<TDelegate>(Expression<TDelegate> act)
    {
        var sb = new StringBuilder();
        sb.Append($"when {act.GetMethodName()}");
        AddMethodArguments(sb, act.Body as MethodCallExpression);
        AddSentence(sb.ToString());
    }

    internal void AddAssert(string actual, string verb, string expected)
    {
        AddWord(actual.ParseActual());
        AddWord(verb.AsWords());
        AddWord(expected.ParseValue());
    }

    internal void AddAnd() => AddWord("and");

    internal void AddThen(string subjectExpr)
        => AddSentence($"then {subjectExpr.ParseValue()}".Trim());

    internal void AddGiven(string valueExpr, ApplyTo applyTo)
    {
        AddPhrase(string.Join(' ', GetWords()));

        IEnumerable<string> GetWords()
        {
            yield return Given;
            if (applyTo == ApplyTo.Default)
                yield return "default";
            else if (applyTo == ApplyTo.Using)
                yield return "using";
            yield return valueExpr.ParseValue();
        }
    }

    internal void AddVerify<TService>(string expressionExpr)
        => AddWord($"{typeof(TService).Name}.{expressionExpr.ParseCall()}");

    internal void AddThrows<TError>()
        => AddSentence($"then throws {typeof(TError).Name}");

    private void AddPhrase(string phrase)
        => _descriptionBuilder.Append($"{Environment.NewLine} {phrase}");

    private void AddSentence(string phrase)
        => _descriptionBuilder.Append($"{Environment.NewLine}{phrase.Capitalize()}");

    private void AddWord(string word)
    {
        if (string.IsNullOrEmpty(word))
            return;
        _descriptionBuilder.Append(' ');
        _descriptionBuilder.Append(word);
    }

    private string Given => 0 == _givenCount++ ? "given" : "and";

    private static void AddMethodArguments(StringBuilder sb, MethodCallExpression body)
    {
        if (body is null)
            return;
        sb.Append('(');
        foreach (var argument in body.Arguments)
            sb.Append(DescribeArgument(argument));
        sb.Append(')');
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
}