namespace WhereIsTheBus.TelegramBot.Telegram.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
internal sealed class TelegramRoutesAttribute : Attribute
{
    public TelegramRoutesAttribute(params string[] names) => Names = names;

    public IEnumerable<string> Names { get; }
}