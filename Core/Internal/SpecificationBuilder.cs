using System.Text;
using XspecT.Internal.TestData;

namespace XspecT.Internal;

internal class SpecificationBuilder
{
    private string _description;
    private readonly List<Action> _applications = [];
    private readonly StringBuilder _descriptionBuilder = new();
    private int _givenCount;
    private int _thenCount;
    private string _currentMockSetup;

    public string Specification => _description ??= Build();

    internal void Add(Action apply) => _applications.Add(apply);

    internal string Build()
    {
        foreach (var apply in _applications) apply();
        return _descriptionBuilder.ToString().Trim();
    }

    internal void AddMockSetup<TService>(string callExpr) 
        => AddPhraseOrSentence($"{Given} {GetMockName<TService>('.')}{callExpr.ParseCall(true)}");

    internal void AddMockReturnsDefault<TService>(string returnsExpr)
        => AddPhraseOrSentence($"{Given} {GetMockName<TService>(' ')}returns {returnsExpr.ParseValue()}");

    internal void AddMockReturns(string returnsExpr)
        => AddWord($"returns {returnsExpr.ParseValue()}");

    internal void AddMockThrowsDefault<TService, TException>()
        => AddWord($"{Given} {GetMockName<TService>(' ')}throws {NameOf<TException>()}");

    internal void AddMockThrowsDefault<TService>(string expectedExpr)
        => AddWord($"{Given} {GetMockName<TService>(' ')}throws {expectedExpr.ParseValue()}");

    internal void AddMockThrows<TException>()
        => AddWord($"throws {NameOf<TException>()}");

    internal void AddMockThrows(string expectedExpr)
        => AddWord($"throws {expectedExpr.ParseValue()}");

    internal void AddWhen(string actExpr)
        => AddSentence($"when {actExpr.ParseCall()}");

    internal void AddAfter(string setUpExpr)
        => AddSentence($"after {setUpExpr.ParseCall()}");

    internal void AddBefore(string tearDownExpr)
        => AddSentence($"before {tearDownExpr.ParseCall()}");

    internal void AddAssert(string actual, string verb, string expected)
    {
        AddWord(actual.ParseActual());
        AddWord(verb.AsWords());
        AddWord(expected.ParseValue());
    }

    internal void AddThen() => AddPhraseOrSentence(Then);

    internal void AddGiven(string valueExpr, ApplyTo applyTo)
    {
        _currentMockSetup = null;
        AddPhraseOrSentence(string.Join(' ', GetWords()));

        IEnumerable<string> GetWords()
        {
            yield return Given;
            if (applyTo == ApplyTo.Using)
                yield return "using";
            yield return valueExpr.ParseValue();
            if (applyTo == ApplyTo.Default)
                yield return "as default";
        }
    }

    internal void AddGiven<TModel>(string setupExpr, string article)
    {
        _currentMockSetup = null;
        var articleStr = string.IsNullOrEmpty(article) ? string.Empty : $"{article.AsWords()} ";
        AddPhraseOrSentence($"{Given} {articleStr}{NameOf<TModel>()} {{ {setupExpr.ParseValue()} }}");
    }

    internal void AddVerify<TService>(string expressionExpr)
        => AddWord($"{NameOf<TService>()}.{expressionExpr.ParseCall(true)}");

    internal void AddAssertThrows<TError>(string binder)
        => AddPhraseOrSentence($"{Then} throws {NameOf<TError>()} {binder}".Trim());

    internal void AddAssertThrows(string expectedExpr)
        => AddPhraseOrSentence($"{Then} throws {expectedExpr.ParseValue()}");

    internal void AddTap(string expr) => AddWord($"tap({expr})");

    internal void AddAssert(string assertName) => AddWord(assertName.AsWords());

    private string GetMockName<TService>(char binder)
    {
        var nextMockSetup = NameOf<TService>();
        var mockName = nextMockSetup == _currentMockSetup
            ? ""
            : $"{nextMockSetup}{binder}";
        _currentMockSetup = nextMockSetup;
        return mockName;
    }

    private void AddPhraseOrSentence(string phrase)
    {
        if (char.IsUpper(phrase[0]))
            AddSentence(phrase);
        else AddPhrase(phrase);
    }

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

    private static string NameOf<T>() => typeof(T).Alias();

    private string Given => 0 == _givenCount++ ? "Given" : "and";

    private string Then => 0 == _thenCount++ ? "Then" : "and";
}