namespace WhereIsTheBus.TelegramBot.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
internal class TelegramRoutesAttribute : Attribute
{
    public TelegramRoutesAttribute(params string[] names) => Names = names;

    public IEnumerable<string> Names { get; }
}