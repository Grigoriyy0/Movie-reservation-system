namespace MovieReservationSystem.Application.Shared;

public class Result<T>
{
    public T? Value { get; set; }

    public bool Success = true;
    
    public IEnumerable<string> Errors = new List<string>();

    public static Result<T> AsSuccess(T value)
    {
        return new Result<T> { Value = value, Success = true };
    }

    public static Result<T> AsFailure(string error)
    {
        return new Result<T> { Success = false, Errors =  new List<string>{ error } };
    }

    public static implicit operator Result<T>(T value)
    {
        return new Result<T> { Value = value, Success = true, Errors =  [] };
    }
}