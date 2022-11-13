

namespace Logger.Appenders
{
    using Layouts.Contracts;
    using System.IO;

    public class FileAppender : BaseAppender
    {
        private string path = @"..\..\..\Text.txt";
        private LogFile logfile;
        public FileAppender(ILayout layout, LogFile file) : base(layout)
        {
            this.logfile = file;
        }

        public override void Append(ReportLevel errorType, string errorTime, string errorMessage)
        {
            if (errorType.CompareTo(this.ReportLevel) == -1)
            {
                return;
            }
            string message = string.Format(Layout.Format, errorTime, errorType , errorMessage);

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
            }

            MessagesCount++;
            logfile.Write(message);
        }

        public override string ToString()
        {
            return base.ToString() + $", File size {logfile.FileSize}";
        }
    }
}
