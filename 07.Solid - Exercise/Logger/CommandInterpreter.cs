

namespace Logger
{
    using System;
    using System.Collections.Generic;


    using Appenders;
    using Appenders.Contracts;
    using Layouts;
    using Layouts.Contracts;
    using Loggers.Contracts;   
    using Loggers;


    public class CommandInterpreter
    {
        private int appendersCount;

        public void Run()
        {
            appendersCount = int.Parse(Console.ReadLine());

            ICollection<IAppender> appenders = GetAppenders();

            ILogger logger = new Logger(appenders);

            ProcessCommand(logger);

            Console.WriteLine(logger);
        }

        private static void ProcessCommand(ILogger logger)
        {
            string command;
            while ((command = Console.ReadLine()) != "END")
            {
                string[] tokens = command.Split('|', StringSplitOptions.RemoveEmptyEntries);

                string reportLevel = tokens[0];
                string date = tokens[1];
                string message = tokens[2];

                switch (reportLevel)
                {
                    case "INFO":
                        logger.Info(date, message);
                        break;
                    case "WARNING":
                        logger.Warning(date, message);
                        break;
                    case "ERROR":
                        logger.Error(date, message);
                        break;
                    case "CRITICAL":
                        logger.Critical(date, message);
                        break;
                    case "FATAL":
                        logger.Fatal(date, message);
                        break;
                    default:
                        break;
                }
            }
        }

        private ICollection<IAppender> GetAppenders()
        {
            var appenders = new List<IAppender>();

            for (int i = 0; i < appendersCount; i++)
            {
                ILayout layout = null;
                IAppender appender = null;

                string[] appenderTokens = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                string appenderType = appenderTokens[0];
                string layoutType = appenderTokens[1];
                string reportLevel = string.Empty;

                if (appenderTokens.Length == 3)
                    reportLevel = appenderTokens[2];

                layout = CreateLayout(layoutType);
                appender = CreateAppender(appenderType, layout, reportLevel);

                appenders.Add(appender);
            }

            return appenders;
        }

        private IAppender CreateAppender(string appenderType, ILayout layout, string reportLevel)
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

        private ILayout CreateLayout(string layoutType)
        {
            switch (layoutType)
            {
                case "SimpleLayout":
                    return new SimpleLayout();
                case "XmlLayout":
                    return new XmlLayout();
                default:
                    return null;                    
            }
        }
    }
}
