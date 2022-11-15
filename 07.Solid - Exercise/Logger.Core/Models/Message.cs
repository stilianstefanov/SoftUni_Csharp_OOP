namespace Logger.Core.Models
{
    using Contracts;
    using Logger.Core.Enums;

    public class Message : IMessage
    {
        public Message(ReportLevel reportLevel, string date, string text)
        {
            ReportLevel = reportLevel;
            Date = date;
            Text = text;
        }

        public ReportLevel ReportLevel { get; private set; }

        public string Date { get; private set; }

        public string Text { get; private set; }
    }
}
