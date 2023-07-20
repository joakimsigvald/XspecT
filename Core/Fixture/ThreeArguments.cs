namespace XspecT.Fixture;

public class ThreeArguments<TValue1, TValue2, TValue3> : IArguments
{
    public TValue1 Arg1 { get; set; }
    public TValue2 Arg2 { get; set; }
    public TValue3 Arg3 { get; set; }
}