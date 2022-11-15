

namespace Logger.Core.Appenders
{   
    using System.Text;

    public class LogFile
    {
        private StringBuilder messages;

        public LogFile()
        {
            messages = new StringBuilder();
        }

        public int FileSize
        {
            get
            {
                return GetSize();
            }
        }

        private int GetSize()
        {
            int size = 0;
            string messagesToString = messages.ToString();

            for (int i = 0; i < messagesToString.Length; i++)
            {
                if (char.IsLetter(messagesToString[i]))
                     size += (int)messagesToString[i];
            }

            return size;
        }

        public void Write(string message)
        {
            messages.Append(message);
        }
    }
}
