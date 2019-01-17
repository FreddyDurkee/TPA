using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using Logging;
using System.ComponentModel.Composition;
using System.Data.SqlClient;

namespace DbLogging
{
    [Export(typeof(ILogSaver))]
    public class DbLogSaver: ILogSaver
    {
        #region static
        private static readonly string CHECKTABLE_EXISTANCE = "SELECT COUNT(*) FROM sys.tables WHERE [name] = 'Log';";
        private static readonly string CREATETABLE_QUERY = "CREATE TABLE [dbo].[Log] ([Id] [int] IDENTITY (1, 1) NOT NULL,[Date] [datetime] NOT NULL,[Thread] [varchar] (255) NOT NULL,[Level] [varchar] (50) NOT NULL,[Logger] [varchar] (255) NOT NULL,[Message] [varchar] (4000) NOT NULL);";
        private static readonly string CONNECTION_TYPE_DEF = "System.Data.SqlClient.SqlConnection, System.Data, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
        private static readonly string LOG_QUERY = "INSERT INTO Log([Date], [Thread], [Level], [Logger], [Message]) VALUES(@log_date, @thread, @log_level, @logger, @message);";
        #endregion

        #region fields
        private string _connectionString;
        #endregion

        #region init
        [ImportingConstructor]
        public DbLogSaver([Import("DbLogSaver.ConnectionString")] string connectionString)
        {
            this._connectionString = connectionString;
            PrepareDB();
            Load();
        }

        private void PrepareDB()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(CHECKTABLE_EXISTANCE, con))
                {
                    int resultNum = (int)command.ExecuteScalar();
                    if(resultNum > 0)
                    {
                        return;
                    }
                }
                using (SqlCommand command = new SqlCommand(CREATETABLE_QUERY, con))
                {
                    command.ExecuteNonQuery();
                }      
            }
        }

        private void Load()
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.ResetConfiguration();

            AdoNetAppender appender = new AdoNetAppender();
            appender.ConnectionString = _connectionString;
            appender.ConnectionType = CONNECTION_TYPE_DEF;
            appender.CommandText = LOG_QUERY;
            appender.BufferSize = 1;
            AddSqlParams(appender);
            appender.ActivateOptions();

            hierarchy.Root.AddAppender(appender);

            hierarchy.Root.Level = Level.Info;
            hierarchy.Configured = true;
        }

        private static void AddSqlParams(AdoNetAppender appender)
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
