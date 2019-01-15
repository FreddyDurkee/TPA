using log4net;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using log4net.Appender;
using Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;
using System.Data.SqlClient;

namespace DbLogging
{
    [Export(typeof(ILogSaver))]
    public class DbLogSaver: ILogSaver
    {
        private const string CREATEDB_QUERY = "CREATE TABLE [dbo].[Log] ([Id] [int] IDENTITY (1, 1) NOT NULL,[Date] [datetime] NOT NULL,[Thread] [varchar] (255) NOT NULL,[Level] [varchar] (50) NOT NULL,[Logger] [varchar] (255) NOT NULL,[Message] [varchar] (4000) NOT NULL);";
        private static readonly string LOG_QUERY = "INSERT INTO Log([Date], [Thread], [Level], [Logger], [Message]) VALUES(@log_date, @thread, @log_level, @logger, @message)";
        private string connectionString;

        [ImportingConstructor]
        public DbLogSaver([Import("DbLogSaver.ConnectionString")] string connectionString)
        {
            this.connectionString = connectionString;
            prepareDB();
            load();
        }

        private void prepareDB()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(CREATEDB_QUERY, con))
                {
                    command.ExecuteNonQuery();
                }      
            }
        }

        private void load()
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.ResetConfiguration();

            AdoNetAppender appender = new AdoNetAppender();
            appender.ConnectionString = connectionString;
            appender.CommandText = LOG_QUERY;
            appender.BufferSize = 1;
            addSqlParams(appender);
            appender.ActivateOptions();

            hierarchy.Root.AddAppender(appender);

            hierarchy.Root.Level = Level.Info;
            hierarchy.Configured = true;
        }

        private static void addSqlParams(AdoNetAppender appender)
        {
            AdoNetAppenderParameter dateParam = new AdoNetAppenderParameter();
            dateParam.DbType = System.Data.DbType.DateTime;
            dateParam.ParameterName = "@log_date";
            dateParam.Layout = new RawTimeStampLayout();
            appender.AddParameter(dateParam);

            AdoNetAppenderParameter thread = new AdoNetAppenderParameter();
            thread.DbType = System.Data.DbType.String;
            thread.Size = 255;
            thread.ParameterName = "@thread";
            thread.Layout = new Layout2RawLayoutAdapter(new PatternLayout("%thread"));
            appender.AddParameter(thread);

            AdoNetAppenderParameter logLvl = new AdoNetAppenderParameter();
            logLvl.DbType = System.Data.DbType.String;
            logLvl.Size = 50;
            logLvl.ParameterName = "@log_level";
            logLvl.Layout = new Layout2RawLayoutAdapter(new PatternLayout("%level"));
            appender.AddParameter(logLvl);

            AdoNetAppenderParameter logger = new AdoNetAppenderParameter();
            logger.DbType = System.Data.DbType.String;
            logger.Size = 255;
            logger.ParameterName = "@logger";
            logger.Layout = new Layout2RawLayoutAdapter(new PatternLayout("%logger"));
            appender.AddParameter(logger);

            AdoNetAppenderParameter message = new AdoNetAppenderParameter();
            message.DbType = System.Data.DbType.String;
            message.Size = 4000;
            message.ParameterName = "@message";
            message.Layout = new Layout2RawLayoutAdapter(new PatternLayout("%message"));
            appender.AddParameter(message);
        }

        private ILog getLogger(string loggerName)
        {
            return LogManager.GetLogger(loggerName);
        }

        public void Debug(string loggerName, string message)
        {
            getLogger(loggerName).Debug(message);
        }

        public void Info(string loggerName, string message)
        {
            getLogger(loggerName).Info(message);
        }

        public void Warn(string loggerName, string message)
        {
            getLogger(loggerName).Warn(message);
        }

        public void Error(string loggerName, string message)
        {
            getLogger(loggerName).Error(message);
        }

        public void Fatal(string loggerName, string message)
        {
            getLogger(loggerName).Fatal(message);
        }
    }
}
