namespace ValidatorBuilder.Core;

[Serializable]
internal class RulesNullException : Exception
{
    public string? Key { get; init; }

    public RulesNullException() { }

    public RulesNullException(string? message) : base(message) { }

    public RulesNullException(string? key, string message = "The validation rules for the informed key is null.") : base(message) => Key = key;

    public RulesNullException(string? message, Exception? innerException) : base(message, innerException) { }

    protected RulesNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}