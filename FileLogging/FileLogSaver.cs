using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using Logging;

namespace FileLogging
{
    [Export(typeof(ILogSaver))]
    public class FileLogSaver: ILogSaver
    {
        #region static
        private static readonly string LOG_PATTERN = "%date [%thread] %-5level %logger - %message%newline";
        #endregion

        #region fields
        private string fileName;
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

                RollingFileAppender roller = new RollingFileAppender();
                roller.AppendToFile = false;
                roller.File = fileName;
                roller.Layout = patternLayout;
                roller.MaxSizeRollBackups = 5;
                roller.MaximumFileSize = "1GB";
                roller.RollingStyle = RollingFileAppender.RollingMode.Size;
                roller.StaticLogFileName = true;
                roller.ActivateOptions();
                hierarchy.Root.AddAppender(roller);

                hierarchy.Root.Level = Level.Info;
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
