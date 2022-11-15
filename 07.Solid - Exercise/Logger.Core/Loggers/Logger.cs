

namespace Logger.Core.Loggers
{
    using System.Collections.Generic;
    using System.Text;

    using Contracts;
    using Appenders.Contracts;
    using Enums;
    using Models.Contracts;
    using Models;
    

    public class Logger : ILogger
    {
        private ICollection<IAppender> appenders;

        public Logger(params IAppender[] appenders)
        {
            this.appenders = appenders;
        }

        public Logger(ICollection<IAppender> appenders)
        {
            this.appenders = appenders;
        }

        public void Critical(string date, string message)
        {
            foreach (IAppender appender in appenders)
            {
                appender.Append(CreateMessage(ReportLevel.CRITICAL, date, message));
            }
        }

        public void Error(string date, string message)
        {
            foreach (IAppender appender in appenders)
            {
                appender.Append(CreateMessage(ReportLevel.ERROR, date, message));
            }
        }

        public void Fatal(string date, string message)
        {
            foreach (IAppender appender in appenders)
            {
                appender.Append(CreateMessage(ReportLevel.FATAL, date, message));
            }
        }

        public void Info(string date, string message)
        {
            foreach (IAppender appender in appenders)
            {
                appender.Append(CreateMessage(ReportLevel.INFO, date, message));
            }
        }

        public void Warning(string date, string message)
        {
            foreach (IAppender appender in appenders)
            {
                appender.Append(CreateMessage(ReportLevel.WARNING, date, message));
            }
        }

        private IMessage CreateMessage(ReportLevel reportLevel, string date, string message)
            => new Message(reportLevel, date, message);

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Logger info");

            foreach (IAppender appender in appenders)
            {
                sb.AppendLine(appender.ToString());
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
