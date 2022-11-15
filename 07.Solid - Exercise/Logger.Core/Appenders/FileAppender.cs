

namespace Logger.Core.Appenders
{
    using System.IO;

    using Layouts.Contracts;    
    using Models.Contracts;
    using Appenders.Contracts;

    public class FileAppender : BaseAppender, IAppender
    {
        private string path = @"..\..\..\Text.txt";
        private LogFile logfile;
        public FileAppender(ILayout layout, LogFile file) : base(layout)
        {
            this.logfile = file;
        }

        public override void Append(IMessage message)
        {
            if (CheckReportLevel(message.ReportLevel))
                return;
            
            string messageTemp = string.Format(Layout.Format, message.Date, message.ReportLevel , message.Text);

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(messageTemp);
            }

            MessagesCount++;
            logfile.Write(messageTemp);
        }

        public override string ToString()
        {
            return base.ToString() + $", File size {logfile.FileSize}";
        }
    }
}
