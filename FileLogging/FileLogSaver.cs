using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using Logging;
using System.ComponentModel.Composition;
using static log4net.Appender.FileAppender;

namespace FileLogging
{
    [Export(typeof(ILogSaver))]
    public class FileLogSaver: ILogSaver
    {
        #region static
        private static readonly string LOG_PATTERN = "%date [%thread] %-5level %logger - %message%newline";
        #endregion

        #region fields
        private readonly string fileName;
        #endregion

        #region init
        [ImportingConstructor]
        public FileLogSaver([Import("FileLogSaver.FileName")] string fileName)
        {
            this.fileName = fileName;
            Load();
        }

        private void Load() { 
                Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
                hierarchy.ResetConfiguration();

                PatternLayout patternLayout = new PatternLayout();
                patternLayout.ConversionPattern = LOG_PATTERN;
                patternLayout.ActivateOptions();

                FileAppender fileAppender = new FileAppender();
                fileAppender.LockingModel = new MinimalLock();
                fileAppender.AppendToFile = false;
                fileAppender.File = fileName;
                fileAppender.Layout = patternLayout;
                fileAppender.ActivateOptions();
                hierarchy.Root.AddAppender(fileAppender);

                hierarchy.Root.Level = Level.All;
                hierarchy.Configured = true;
        }
        #endregion

        #region private
        private ILog GetLogger(string loggerName)
        {
            return LogManager.GetLogger(loggerName);
        }
        #endregion

        #region public
        public void Debug(string loggerName, string message)
        {
            GetLogger(loggerName).Debug(message);
        }

        public void Info(string loggerName, string message)
        {
            GetLogger(loggerName).Info(message);
        }

        public void Warn(string loggerName, string message)
        {
            GetLogger(loggerName).Warn(message);
        }

        public void Error(string loggerName, string message)
        {
            GetLogger(loggerName).Error(message);
        }

        public void Fatal(string loggerName, string message)
        {
            GetLogger(loggerName).Fatal(message);
        }
        #endregion
    }
}
