

namespace Logger.Core.Appenders.Contracts
{
    using Layouts.Contracts;
    using Enums;
    using Models.Contracts;

    public interface IAppender
    {
        ILayout Layout { get; }

        ReportLevel ReportLevel { get; set; }

        void Append(IMessage message);
    }
}
