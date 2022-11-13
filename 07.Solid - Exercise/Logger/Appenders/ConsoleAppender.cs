

namespace Logger.Appenders
{
    using System;

    using Contracts;
    using Logger.Layouts.Contracts;
    

    public class ConsoleAppender : BaseAppender, IAppender
    {
        public ConsoleAppender(ILayout layout) : base(layout)
        {
        }

        public override void Append(ReportLevel errorType, string errorTime, string errorMessage)
        {
            if (errorType.CompareTo(this.ReportLevel) == -1)
            {
                return;
            }

            Console.WriteLine(string.Format(Layout.Format, errorTime, errorType, errorMessage));

            MessagesCount++;
        }
    }
}
