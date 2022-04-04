using System.Text;

namespace Telegram.Bot.Extensions.FluentMarkdown;

public class TelegramString
{
    private readonly StringBuilder _stringBuilder;

    public TelegramString(int capacity)
    {
        _stringBuilder = new StringBuilder(capacity);
    }

    public string Content => _stringBuilder.ToString();

    #region Formatting

    public TelegramString Bold() => Surround(by: "**");

    public TelegramString Italic() => Surround(by: "__");

    public TelegramString Underline() => Surround(by: "--");

    public TelegramString Strikethrough() => Surround(by: "~~");

    public TelegramString Hyperlink(string to)
    {
        _stringBuilder.Insert(0, '[').Append("](").Append(to).Append(')');
        return this;
    }

    public TelegramString Spoiler() => Surround(by: "||");
    
    #endregion

    private TelegramString Surround(string by)
    {
        _stringBuilder.Insert(0, by).Append(by);
        return this;
    }
}