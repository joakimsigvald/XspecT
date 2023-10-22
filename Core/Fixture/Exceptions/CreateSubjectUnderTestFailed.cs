namespace XspecT.Fixture.Exceptions;

public class CreateSubjectUnderTestFailed : SetupFailed
{
    public CreateSubjectUnderTestFailed(string className, Exception ex) : base(CreateMessage(className), ex) { }

    private static string CreateMessage(string className) => @$"Failed to automatically create mock for {className}. 
Make sure it has a public constructor. If any of the constructor arguments are struct types, 
or have generic struct parameters, they have to be provided explicitly using the 'Given' method";
}