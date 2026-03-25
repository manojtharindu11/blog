using Application.Common.Result;

namespace Application.Error;

public static class AuthError
{
    public static Common.Result.Error InvalidRegisterRequest => new(ErrorTypeConstant.ValidationError, "Invalid register request");
    public static Common.Result.Error UserAlreadyExist => new(ErrorTypeConstant.ValidationError, "User already exist");
    public static Common.Result.Error InvalidLoginRequest => new(ErrorTypeConstant.ValidationError, "Invalid login request");
    public static Common.Result.Error UserNotFound => new(ErrorTypeConstant.NotFound, "User not found");
    public static Common.Result.Error InvalidPassword => new(ErrorTypeConstant.ValidationError, "Invalid password");
    public static Common.Result.Error CreateInvalidLoginRequestError(IEnumerable<string> errors) => 
        new(ErrorTypeConstant.ValidationError, string.Join(", ", errors));
    public static Common.Result.Error CreateInvalidRegisterRequestError(IEnumerable<string> errors) =>
        new(ErrorTypeConstant.ValidationError, string.Join(", ", errors));
}
