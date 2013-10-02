namespace BuildUtility
{
    using System;
    using System.IO;

    public class Logger : IDisposable
    {
        private StreamWriter m_logFile;

        public Logger()
        {
            try
            {
                if (this.m_logFile == null)
                {
                    this.m_logFile = new StreamWriter("BuildUtitlityLog.txt", true);
                }
            }
            catch
            {
                //Given Try/Catch to suppress exceptions inside logger.
            }
        }

        ~Logger()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.m_logFile != null)
                {
                    this.m_logFile.WriteLine();
                    this.m_logFile.Close();
                    this.m_logFile = null;
                }
            }
        }

        public void Write(string content, bool writeToConsole)
        {
            if (this.m_logFile != null)
            {
                this.m_logFile.WriteLine(content);
            }
            if (writeToConsole)
            {
                Console.WriteLine(content);
            }
        }
    }
}
