namespace XspecT.Test.Given;

public readonly struct MyName
{
    private readonly string _primitive;

    public string Primitive { get => _primitive; init => _primitive = Trim(value); }

    private static string Trim(string value) => value?[..40] ?? string.Empty;

    public static implicit operator string(MyName value) => value.Primitive;
    public static explicit operator MyName(string value) => new() { Primitive = value };
}