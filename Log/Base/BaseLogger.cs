using Log.Common;
using System.Collections.Generic;

namespace Log.Base
{
    public abstract class BaseLogger
    {
        protected string debugFileName = string.Empty;
        public BaseLogger()
        {
            SetLogFilesPath();
        }
        private void SetLogFilesPath()
        {
            if (System.IO.Directory.Exists(GlobalVariable.LogDefaultPath))
            {
                debugFileName = GlobalVariable.DebugFileName;
            }
            else
            {
                debugFileName = GlobalVariable.DebugBaseDirFileName;
            }
        }
        #region Abstract Methods
        public abstract void Info(LogType logType, string currentClassName, string currentMethodName, string message);
        #endregion
    }
}
