namespace WpfAppDemo;

using System.Text.RegularExpressions;
using ValidatorBuilder;
using ValidatorBuilder.Core;

public class MainWindowViewModel : ViewModelBase
{ 
    private const string Email = "Email";

    private const int EmailMinLength = 7;
    private const int EmailMaxLength = 100;
    private const string EmailPattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

    private const string Password = "Password";

    private const int PassMinLength = 8;
    private const int PassMaxLength = 16;

    private const string reset = "reset";

    private readonly Validator Validator = ValidatorBuilder
        .Create()
            .RulesFor(Email, "Email requirements:")
                .Rule($"Must contain at least {EmailMinLength} characters.", x => x?.Length >= EmailMinLength)
                .Rule($"Must contain at most {EmailMaxLength} characters.", x => x?.Length <= EmailMaxLength)
                .Rule("The email must be in a valid format.", x => x is not null && new Regex(EmailPattern).Match(x).Success)

            .RulesFor(Password, "Password requirements:")
                .Rule($"Must be at least {PassMinLength} characters.", x => x?.Length >= PassMinLength)
                .Rule($"Must be at most {PassMaxLength} characters.", x => x?.Length <= PassMaxLength)
                .Rule("Must contain number.", x => x is not null && x.ToCharArray().Any(c => char.IsNumber(c)))
                .Rule("Must contain letter.", x => x is not null && x.ToCharArray().Any(c => char.IsLetter(c)))
                .Rule("Must be capitalized.length", x => x is not null && x.ToCharArray().Any(c => char.IsUpper(c)))
                .Rule("Must contain symbol.", x => x is not null && x.IndexOfAny(new[] { '!', '@', '#', '%', '&', '*', '?' }) > -1)

            .RulesFor(reset, string.Empty)
        .Build();


    private RuleGroup? _ruleGroup;

    public RuleGroup? RuleGroup { get => _ruleGroup; set => SetProperty(ref _ruleGroup, value); }


    private string? _header;

    public string? Header { get => _header; set => SetProperty(ref _header, value); }


    private string? _emailTextBoxValue;

    public string? EmailTextBoxValue
    {
        get => _emailTextBoxValue;
        set
        {
            _ = SetProperty(ref _emailTextBoxValue, value);
            (RuleGroup, Header) = (Validator.GetRulesFor(Email), RuleGroup.Header);
            Validator.ValidateFor(Email, _emailTextBoxValue);
        }
    }


    private string? _passwordBoxValue;

    public string? PasswordBoxValue
    {
        get => _passwordBoxValue;
        set
        {
            _ = SetProperty(ref _passwordBoxValue, value);
            (RuleGroup, Header) = (Validator.GetRulesFor(Password), RuleGroup.Header);
            Validator.ValidateFor(Password, _passwordBoxValue);
        }
    }

    public void Reset()
    {
        EmailTextBoxValue = string.Empty;
        (RuleGroup, Header) = (Validator.GetRulesFor(reset), RuleGroup.Header);
        Validator.ValidateFor(reset, string.Empty);
    }
}