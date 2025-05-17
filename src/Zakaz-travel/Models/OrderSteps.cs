namespace Zakaz_travel.Models
{
    public record OrderStep(
        string Prompt,
        Action<string> ValueSetter,
        Func<string, bool> Validator,
        string ErrorMessage
    );
}
