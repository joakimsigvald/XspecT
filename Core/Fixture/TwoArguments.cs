namespace XspecT.Fixture;

public class TwoArguments<TValue1, TValue2> : IArguments
{
    public TValue1 Arg1 { get; set; }
    public TValue2 Arg2 { get; set; }
}