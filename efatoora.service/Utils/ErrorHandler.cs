namespace efatoora.Server.Utils;

public class ErrorHandler
{
    public static void HandleValidationErrors(dynamic Errors)
    {
        var errorsString = "";
        foreach (var error in Errors)
        {
            errorsString += error + "\n";
            Console.WriteLine(error);
        }
        throw new Exception(errorsString);
    }
}
