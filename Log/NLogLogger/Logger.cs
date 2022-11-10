using Log.Base;
using Log.Common;
using Log.DataObjects;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using Utilities;

namespace Log.NLogLogger
{
    public class Logger : BaseLogger
    {
        #region [Private properties]
        NLog.Logger _logger;
        LoggingRule _rule;
        LoggingConfiguration _config;
        static FileTarget _fileTarget;
        public delegate void LogMessageEvent(string message);
        #endregion

        #region [ Constructor(s) ]
        public Logger() : base()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _config = new LoggingConfiguration();
            _fileTarget = new FileTarget();
            _rule = new LoggingRule("*", LogLevel.Trace, _fileTarget);
            SetLogConfig();
        }
        #endregion

        #region [ Private Method(s) ]
        private void SetLogConfig()
        {
            _config.AddTarget("file", _fileTarget);
            _fileTarget.Layout = @"${date:format=HH\:mm\:ss}  ${message}";
            _fileTarget.FileName = @"${basedir}/Logs/${date:format=yyyy}/${date:format=MM}/${shortdate}/${level}/${level}.txt";
            _config.LoggingRules.Add(_rule);
            LogManager.Configuration = _config;
        }
        #endregion

        #region [ Public Method(s)]
        public override void Info(LogType logType, string currentClassName, string currentMethodName, string message)
        {
            LogDataObject logdataObject = new LogDataObject()
            {
                Time = DateTime.Now.ToString("HH:mm:ss.fff"),
                Message = message,
                ModuleName = currentClassName,
                SubModuleName = currentMethodName,
                LogType = logType
            };
            string logMessage = logdataObject.ToJsonString();
            _fileTarget.Layout = @"${message}";
            _fileTarget.FileName = debugFileName;
            _logger.Debug(logMessage);
        }
        #endregion
    }
}
