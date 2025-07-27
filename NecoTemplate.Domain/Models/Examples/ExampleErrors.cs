using NecoTemplate.Domain.Abstractions;

namespace NecoTemplate.Domain.Models.Examplesl;

public class ExampleErrors
{
   public static ErrorResult NotFound(Guid exampleId, string code) =>
   ErrorResult.NotFound($"Example.NotFound.{code}", $"The example with the identifier {exampleId} not found");

   public static ErrorResult NotAuthorized( string code) =>
   ErrorResult.UnAuthorized($"Example.NotFound.{code}", $"You are not allowed to use this action.");

    public static ErrorResult CanNotGetResponse(string code) =>
   ErrorResult.UnAuthorized($"Example.CanNotGetResponse.{code}", $"Server can not get what it needs.");
}
