

namespace Logger.Core.Appenders
{
    using System;

    using Contracts;
    using Layouts.Contracts;
    using Enums;
    using Models.Contracts;

    public class ConsoleAppender : BaseAppender, IAppender
    {
        public ConsoleAppender(ILayout layout) : base(layout)
        {
        }

        public override void Append(IMessage message)
        {
            if (CheckReportLevel(message.ReportLevel))
                return;

            Console.WriteLine(string.Format(Layout.Format, message.Date, message.ReportLevel, message.Text));

            MessagesCount++;
        }
    }
}
