namespace SkyVault;

[Serializable]
public sealed class SkyResult<T>
{
    public string? Message { get; private set; }
    public string? ErrorCode { get; private set; }
    public string? TraceId { get; private set; }
    
    public T Value { get; private set; } = default!;
    public bool Succeeded { get; private set; } = false;

    public SkyResult<T> Fail(string message, string errorCode, string traceId)
    {
        Message = message;
        ErrorCode = errorCode;
        TraceId = traceId;

        return this;
    }

    public SkyResult<T> SucceededWithValue(T value)
    {
        ArgumentNullException.ThrowIfNull(value);

        Message = string.Empty;
        ErrorCode = string.Empty;
        TraceId = string.Empty;
        
        Value = value;
        Succeeded = true;

        return this;
    }
}