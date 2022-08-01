namespace WpfAppDemo;

using System.Text.RegularExpressions;
using ValidatorBuilder;
using ValidatorBuilder.Core;

public class MainWindowViewModel : ViewModelBase
{
    const string Email = "Email";

    const int EmailMinLength = 7;
    const int EmailMaxLength = 100;
    const string EmailPattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

    static readonly Predicate<string?> EmailMinLenghtFilter = x => x?.Length >= EmailMinLength;
    static readonly Predicate<string?> EmailMaxLenghtFilter = x => x?.Length <= EmailMaxLength;
    static readonly Predicate<string?> EmailFormatFilter = x => x is not null && new Regex(EmailPattern).Match(x).Success;

    const string Password = "Password";

    const int PasswordMinLength = 8;
    const int PasswordMaxLength = 16;

    static readonly Predicate<string?> PasswordMinLenghtFilter = x => x?.Length >= PasswordMinLength;
    static readonly Predicate<string?> PasswordMaxLenghtFilter = x => x?.Length <= PasswordMaxLength;
    static readonly Predicate<string?> PasswordNumberFilter = x => x is not null && x.ToCharArray().Any(c => char.IsNumber(c));
    static readonly Predicate<string?> PasswordLetterFilter = x => x is not null && x.ToCharArray().Any(c => char.IsLetter(c));
    static readonly Predicate<string?> PasswordCapsFilter = x => x is not null && x.ToCharArray().Any(c => char.IsUpper(c));
    static readonly Predicate<string?> PasswordSymbolFilter = x => x is not null && x.IndexOfAny(new[] { '!', '@', '#', '%', '&', '*', '?' }) > -1;

    readonly Validator Validator = ValidatorBuilder
        .Create()
            .RulesFor(Email, "Email requirements:")
                .Rule($"Must contain at least {EmailMinLength} characters.", EmailMinLenghtFilter)
                .Rule($"Must contain at most {EmailMaxLength} characters.", EmailMaxLenghtFilter)
                .Rule("The email must be in a valid format.", EmailFormatFilter)

            .RulesFor(Password, "Password requirements:")
                .Rule($"Must be at least {PasswordMinLength} characters.", PasswordMinLenghtFilter)
                .Rule($"Must be at most {PasswordMaxLength} characters.", PasswordMaxLenghtFilter)
                .Rule("Must contain number.", PasswordNumberFilter)
                .Rule("Must contain letter.", PasswordLetterFilter)
                .Rule("Must be capitalized.length", PasswordCapsFilter)
                .Rule("Must contain symbol.", PasswordSymbolFilter)

            .RulesFor("reset", string.Empty)

        .Build();

    RuleGroup? _ruleGroup;
    string? _header;
    string? _email_TextBoxValue;
    string? _password_PassworBoxValue;

    public RuleGroup? RuleGroup { get => _ruleGroup; set => SetProperty(ref _ruleGroup, value); }

    public string? Header { get => _header; set => SetProperty(ref _header, value); }

    public string? EmailTextBoxValue
    {
        get => _email_TextBoxValue;
        set
        {
            _ = SetProperty(ref _email_TextBoxValue, value);

            RuleGroup = Validator.GetRulesFor(Email);
            Header = RuleGroup.Header;
            Validator.ValidateFor(Email, _email_TextBoxValue);
        }
    }

    public string? PasswordBoxValue
    {
        get => _password_PassworBoxValue;
        set
        {
            _ = SetProperty(ref _password_PassworBoxValue, value);

            RuleGroup = Validator.GetRulesFor(Password);
            Header = RuleGroup.Header;
            Validator.ValidateFor(Password, _password_PassworBoxValue);
        }
    }

    internal void Reset()
    {
        EmailTextBoxValue = string.Empty;
        RuleGroup = Validator.GetRulesFor("reset");
        Header = RuleGroup.Header;
        Validator.ValidateFor("reset", string.Empty);
    }
}