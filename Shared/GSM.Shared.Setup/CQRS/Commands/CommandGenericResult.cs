namespace GSM.Shared.Setup.CQRS.Commands;

public class CommandGenericResult<T>
{
    public CommandGenericResult(bool isSuccess, T result, string message)
    {
        IsSuccess = isSuccess;
        Result = result;
        Message = message;
    }

    public CommandGenericResult()
    {
        IsSuccess = false;
        Result = default;
        Message = string.Empty;
    }
    
    public bool IsSuccess { get; set; }
    public T? Result { get; set; }
    public string Message { get; set; }
}