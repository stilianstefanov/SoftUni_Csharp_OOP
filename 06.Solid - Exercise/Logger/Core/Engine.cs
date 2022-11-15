namespace Logger
{
    
    using System;
    using System.Collections.Generic;

    using Logger.Core.Appenders.Contracts;   
    using Logger.Core.Loggers.Contracts;
    using Logger.Core.Loggers;    
    using Logger.Core.Layouts.Contracts;   
    using Contracts;
    using Logger.Factories.Contracts;

    public class Engine : IEngine
    {
        private int appendersCount;
        private ICollection<IAppender> appenders;
        private ILogger logger;
        private IAppenderFactory appenderFactory;
        private ILayoutFactory layoutFactory;

        public Engine(IAppenderFactory appenderFactory, ILayoutFactory layoutFactory)
        {
            this.appenderFactory = appenderFactory;
            this.layoutFactory = layoutFactory;
        }

        public void Run()
        {
            appendersCount = int.Parse(Console.ReadLine());

            appenders = GetAppenders();

            logger = new Logger(appenders);

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

                layout = layoutFactory.CreateLayout(layoutType);
                appender = appenderFactory.CreateAppender(appenderType, layout, reportLevel);

                appenders.Add(appender);
            }

            return appenders;
        }    
    }
}
