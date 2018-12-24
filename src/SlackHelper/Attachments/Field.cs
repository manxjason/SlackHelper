using System;

namespace ManxJason.SlackHelper.Attachments
{
    /// <summary>
    /// https://api.slack.com/docs/message-attachments
    /// </summary>
    [Serializable]
    public sealed class Field
    {
        public Field(string title, string value, bool @short = false)
        {
            Title = title;
            Value = value;
            Short = @short;
        }

        private string Title { get; }
        private string Value { get; }
        private bool Short { get; }
    }
}