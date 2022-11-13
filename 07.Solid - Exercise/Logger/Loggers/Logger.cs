

namespace Logger.Loggers
{
    using System.Collections.Generic;
    

    using Contracts;
    using Appenders.Contracts;
    using System.Text;

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
                appender.Append(ReportLevel.CRITICAL, date, message);
            }
        }

        public void Error(string date, string message)
        {
            foreach (IAppender appender in appenders)
            {
                appender.Append(ReportLevel.ERROR, date, message);
            }
        }

        public void Fatal(string date, string message)
        {
            foreach (IAppender appender in appenders)
            {
                appender.Append(ReportLevel.FATAL, date, message);
            }
        }

        public void Info(string date, string message)
        {
            foreach (IAppender appender in appenders)
            {
                appender.Append(ReportLevel.INFO, date, message);
            }
        }

        public void Warning(string date, string message)
        {
            foreach (IAppender appender in appenders)
            {
                appender.Append(ReportLevel.WARNING, date, message);
            }
        }

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
