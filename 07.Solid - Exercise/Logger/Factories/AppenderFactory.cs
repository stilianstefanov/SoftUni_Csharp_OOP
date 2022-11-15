



namespace Logger.Factories
{
    using Contracts;
    using Logger.Core.Appenders;
    using Logger.Core.Appenders.Contracts;
    using Logger.Core.Enums;
    using Logger.Core.Layouts.Contracts;
    using System;

    public class AppenderFactory : IAppenderFactory
    {
        public IAppender CreateAppender(string appenderType, ILayout layout, string reportLevel)
        {
            IAppender appender = null;

            switch (appenderType)
            {
                case "ConsoleAppender":
                    appender = new ConsoleAppender(layout);
                    break;
                case "FileAppender":
                    appender = new FileAppender(layout, new LogFile());
                    break;
                default:
                    break;
            }

            if (reportLevel != string.Empty)
                appender.ReportLevel = Enum.Parse<ReportLevel>(reportLevel);

            return appender;
        }
    }
}
