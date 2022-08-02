# ValidatorBuider
It was designed for a system that uses MVVM pattern that applies validation rules as the input is typed, offering feedback to the user as to whether or not these validation rules are met.

## Validator
This class aims to apply all validation rules associated with a given key.

## ValidatorBuilder
Allows build an instance of Validator through fluent design.

## Example
Code snippet in a view model (MainWindowViewModel).

```C#
...

private readonly Validator Validator = ValidatorBuilder
    .Create()
        .RulesFor(Email, "Email requirements:")
            .Rule($"Must contain at least {EmailMinLength} characters.", x => x?.Length >= EmailMinLength)
            .Rule($"Must contain at most {EmailMaxLength} characters.", x => x?.Length <= EmailMaxLength)
            .Rule("The email must be in a valid format.", x => x is not null && new Regex(EmailPattern).Match(x).Success)

        .RulesFor(Password, "Password requirements:")
            .Rule($"Must be at least {PassMinLength} characters.", x => x?.Length >= PassMinLength)
            .Rule($"Must be at most {PassMaxLength} characters.", x => x?.Length <= PassMaxLength)
            .Rule("Must contain a number.", x => x is not null && x.ToCharArray().Any(c => char.IsNumber(c)))
            .Rule("Must contain a letter.", x => x is not null && x.ToCharArray().Any(c => char.IsLetter(c)))
            .Rule("Must contain a capital letter", x => x is not null && x.ToCharArray().Any(c => char.IsUpper(c)))
            .Rule("Must contain a symbol.", x => x is not null && x.IndexOfAny(new[] { '!', '@', '#', '%', '&', '*', '?' }) > -1)

        .RulesFor(reset, string.Empty)
    .Build();
    
...

public RuleGroup? RuleGroup { get => _ruleGroup; set => SetProperty(ref _ruleGroup, value); }

public string? Header { get => _header; set => SetProperty(ref _header, value); }

public string? PasswordBoxValue
{
    get => _passwordBoxValue;
    set
    {
        _ = SetProperty(ref _passwordBoxValue, value);

        RuleGroup = Validator.GetRulesFor(Password);
        Header = RuleGroup.Header;
        Validator.ValidateFor(Password, _passwordBoxValue);
    }
}

...

```

<br/>
Code snipped in UI (MainWindow.xaml)


```C#

<Window.DataContext>
    <local:MainWindowViewModel />
</Window.DataContext>

...

<DataTemplate x:Key="ValidatorViewerItemTemplate">
    <StackPanel Orientation="Horizontal">
        <TextBlock
            x:Name="Glyph"
            VerticalAlignment="Bottom"
            FontFamily="Segoe MDL2 Assets"
            FontWeight="Bold"
            Foreground="Red"
            Text="&#xE894;  "
            TextWrapping="Wrap" />
        <TextBlock VerticalAlignment="Top" Text="{Binding Description}" />
    </StackPanel>
    <DataTemplate.Triggers>
        <DataTrigger Binding="{Binding IsValid}" Value="True">
            <Setter TargetName="Glyph" Property="Text" Value="&#xE73E;" />
            <Setter TargetName="Glyph" Property="TextBlock.Foreground" Value="Blue" />
            <Setter TargetName="Glyph" Property="Margin" Value="0,0,7,0" />
        </DataTrigger>
    </DataTemplate.Triggers>
</DataTemplate>

...

<PasswordBox
    x:Name="LoginPasswordBox"
    Height="Auto"
    Padding="2,2,2,2"
    d:Password="meupassword"
    Background="{x:Null}"
    BorderBrush="#FFCFCFC8"
    FontSize="14"
    Password=""
    PasswordChanged="LoginPasswordBox_PasswordChanged" />

<TextBlock
    x:Name="ValidationInfoTextBlock"
    Margin="0,20,0,10"
    FontSize="14"
    Text="{Binding Header}" />

<ItemsControl
    x:Name="ValidatorViewer"
    IsTabStop="False"
    ItemTemplate="{DynamicResource ValidatorViewerItemTemplate}"
    ItemsSource="{Binding RuleGroup.Rules}" />

...

```

<br/>
Code snipped in UI (MainWindow.xaml.cs)

```C#
private void LoginPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    => ((MainWindowViewModel)DataContext)
        .PasswordBoxValue = ((PasswordBox)sender).Password;

```
<br/>
Validation rules are executed with each keystroke and the validator displays whether the rule was met or not.

![ValidatorScreenPassword](https://user-images.githubusercontent.com/10555640/182276264-5e655630-4f5b-4bc2-ba6e-3775dfc6bfc2.png)
