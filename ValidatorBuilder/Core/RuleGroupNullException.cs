namespace ValidatorBuilder.Core;

[Serializable]
internal class RuleGroupNullException : Exception
{
    public string? Key { get; init; }

    public RuleGroupNullException() { }

    public RuleGroupNullException(string? message) : base(message) { }

    public RuleGroupNullException(string? key, string message = "The validation rules for the informed key is null.") : base(message) => Key = key;

    public RuleGroupNullException(string? message, Exception? innerException) : base(message, innerException) { }

    protected RuleGroupNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}