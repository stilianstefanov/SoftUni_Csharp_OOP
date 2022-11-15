namespace Logger.Core.Models.Contracts
{
    using Logger.Core.Enums;

    public interface IMessage
    {
        ReportLevel ReportLevel { get; }

        string Date { get; }

        string Text { get; }

    }
}
