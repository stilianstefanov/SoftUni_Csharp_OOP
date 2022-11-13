

namespace Logger.Appenders.Contracts
{
    using Logger.Layouts.Contracts;

    public interface IAppender
    {
        ILayout Layout { get; }

        ReportLevel ReportLevel { get; set; }

        void Append(ReportLevel errorType, string errorTime, string errorMessage);
    }
}
