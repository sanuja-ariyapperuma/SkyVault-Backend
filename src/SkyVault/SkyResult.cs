namespace SkyVault;

[Serializable]
public sealed class SkyResult<T>
{
    public string? Message { get; private set; }
    public string? ErrorCode { get; private set; }
    public string? CorrelationId { get; private set; }
    
    public T? Value { get; private set; }
    public bool Succeeded { get; private set; } = false;

    public SkyResult<T> Fail(string? message, string? errorCode, string? correlationId)
    {
        Message = message;
        ErrorCode = errorCode;
        CorrelationId = correlationId;

        return this;
    }

    public SkyResult<T> SucceededWithValue(T value)
    {
        ArgumentNullException.ThrowIfNull(value);

        Message = string.Empty;
        ErrorCode = string.Empty;
        CorrelationId = string.Empty;
        
        Value = value;
        Succeeded = true;

        return this;
    }
}