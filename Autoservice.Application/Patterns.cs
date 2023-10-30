namespace Autoservice.Application;

internal sealed partial class Patterns
{
    private const string _addressPatternRaw = @"^(?i)ул\. [a-zа-яё]{4,20}, д\. ([1-9]|[1-9]\d{1,3}),(| к\. ([1-9]|1\d),) кв\. ([1-9]|[1-9]\d{1,3})$";
    private const string _emailPatternRaw = 
        @"^(?i)(([a-zа-яё]|(?<Digital>\d))(\.|)){5,29}((\.|)[a-zа-яё]|(?<Digital>\d))@(gmail|mail|yandex|yahoo)\.(ru|by|com|ua|fr|en)$";
    private const string _phonePatternRaw = @"^\+375 (44|33|29|17|25) [1-9]\d{2}-\d{2}-\d{2}$";
    private const string _alphabetPatternRaw = @"^(?i)[a-zа-яё]([a-zа-яё]|(?<Space> ))+[a-zа-яё]$";

    [GeneratedRegex(_addressPatternRaw)]
    public static partial Regex AddressPattern();

    [GeneratedRegex(_emailPatternRaw)]
    public static partial Regex EmailPattern();

    [GeneratedRegex(_phonePatternRaw)]
    public static partial Regex PhonePattern();

    [GeneratedRegex(_alphabetPatternRaw)]
    public static partial Regex AlphabetPattern();
}