


namespace Logger.Appenders
{
 
    using Contracts;
    using Logger.Layouts.Contracts;


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

        public abstract void Append(ReportLevel errorType, string errorTime, string errorMessage);

        public override string ToString()
        {
            return $"Appender type: {this.GetType().Name}, Layout type: {Layout.GetType().Name}, Report level: {this.ReportLevel}, Messages appended: {this.MessagesCount}";
        }
    }
}
