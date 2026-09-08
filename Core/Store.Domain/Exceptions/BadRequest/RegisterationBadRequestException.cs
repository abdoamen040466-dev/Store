namespace Store.Domain.Exceptions.BadRequest;

public class RegisterationBadRequestException(List<string> errors) : BadRequestException(string.Join(", ", errors))
{
}
