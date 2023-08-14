namespace XspecT.Fixture.Exceptions;

public class ExecuteMethodUnderTestFailed : SetupFailed
{
    public ExecuteMethodUnderTestFailed(string typeName, Exception ex) : base(CreateMessage(typeName), ex) { }

    private static string CreateMessage(string typeName)
    {
        return 
@$"Failed to run method under test, because an instance of {typeName} could not be provided. 
Make sure SUT has a public constructor. If any of the constructor arguments are struct types,
or have generic struct parameters, they have to be provided explicitly using the 'Using' method";
    }
}