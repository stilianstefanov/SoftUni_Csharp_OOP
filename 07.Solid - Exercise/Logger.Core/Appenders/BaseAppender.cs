


namespace Logger.Core.Appenders
{
 
    using Contracts;
    using Layouts.Contracts;
    using Enums;
    using Logger.Core.Models.Contracts;

    public abstract class BaseAppender : IAppender
    {
        protected BaseAppender(ILayout layout)
        {
            Layout = layout;
            ReportLevel = ReportLevel.INFO;
        }

        public ILayout Layout { get; private set; }

        public ReportLevel ReportLevel { get; set; }

        public int MessagesCount { get; protected set; }

        public abstract void Append(IMessage message);

        protected bool CheckReportLevel(ReportLevel reportLevel)
            =>  reportLevel.CompareTo(this.ReportLevel) == -1;

        public override string ToString()
        {
            return $"Appender type: {this.GetType().Name}, Layout type: {Layout.GetType().Name}, Report level: {this.ReportLevel}, Messages appended: {this.MessagesCount}";
        }
    }
}
