using System.Text;
using XspecT.Internal.TestData;

namespace XspecT.Internal;

internal class SpecificationBuilder
{
    private string _description;
    private readonly List<Action> _applications = [];
    private readonly StringBuilder _descriptionBuilder = new();
    private int _givenCount;
    private bool _isThenReferencingSubject = false;

    public string Description => _description ??= Build();

    internal void Add(Action apply) => _applications.Add(apply);

    internal string Build()
    {
        foreach (var apply in _applications) apply();
        return _descriptionBuilder.ToString().Trim(',').Trim().Capitalize();
    }

    internal void AddMockSetup<TService>(string callExpr)
        => AddPhrase($"{Given} {NameOf<TService>()}.{callExpr.ParseCall()}");

    internal void AddMockReturns(string returnsExpr)
        => AddWord($"returns {returnsExpr.ParseValue()}");

    internal void AddWhen(string actExpr) 
        => AddSentence($"when {actExpr.ParseCall()}");

    internal void AddAssert(string actual, string verb, string expected)
    {
        AddWord(actual.ParseActual(), _isThenReferencingSubject ? "'s " : " ");
        AddWord(verb.AsWords());
        AddWord(expected.ParseValue());
    }

    internal void AddAnd()
    {
        AddWord("and");
        _isThenReferencingSubject = false;
    }

    internal void AddThen(string subjectExpr)
    {
        AddSentence("then");
        AddWord(subjectExpr.ParseValue());
        _isThenReferencingSubject = !string.IsNullOrEmpty(subjectExpr);
    }

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

    internal void AddGivenSetup<TModel>(string setupExpr)
        => AddPhrase($"{Given} {NameOf<TModel>()} {{ {setupExpr.ParseValue()} }}");
    internal void AddVerify<TService>(string expressionExpr)
        => AddWord($"{NameOf<TService>()}.{expressionExpr.ParseCall()}");

    internal void AddThrows<TError>()
        => AddSentence($"then throws {NameOf<TError>()}");

    internal void AddTap(string expr) => AddWord($"tap({expr})");

    private void AddPhrase(string phrase)
        => _descriptionBuilder.Append($"{Environment.NewLine} {phrase}");

    private void AddSentence(string phrase)
        => _descriptionBuilder.Append($"{Environment.NewLine}{phrase.Capitalize()}");

    private void AddWord(string word, string binder = " ")
    {
        if (string.IsNullOrEmpty(word))
            return;
        _descriptionBuilder.Append(binder);
        _descriptionBuilder.Append(word);
    }

    internal void AddMockReturnsDefault<TService>(string returnsExpr)
        => _descriptionBuilder.Append($"{Given} {NameOf<TService>()} returns {returnsExpr.ParseValue()}");

    private static string NameOf<T>() => typeof(T).Alias(); 

    private string Given => 0 == _givenCount++ ? "given" : "and";
}