namespace ValidatorBuilder.Core;

/// <summary>
/// Represents a validation rule that will be applied to a given value.
/// </summary>
public interface IRule
{
    /// <summary>
    /// A message explaining the validation rule.
    /// </summary>
    string? Description { get; set; }

    /// <summary>
    /// <inheritdoc cref="Predicate{T}"/>
    /// </summary>
    Predicate<string?>? Filter { get; set; }

    /// <summary>
    /// Validates a string value through the predicate.
    /// </summary>
    /// <param name="s">The <see cref="string"/> value to be evaluated</param>
    /// <returns><see langword="true"/> if it's a valid value, otherwise, returns <see langword="false"/></returns>
    bool? Validate(string? s);

    /// <summary>
    /// <see langword="true"/> if it's a valid value, otherwise returns <see langword="false"/>.
    /// </summary>
    bool? IsValid { get; set; }
}