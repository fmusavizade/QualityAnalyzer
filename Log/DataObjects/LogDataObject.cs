using Log.Common;

namespace Log.DataObjects
{
    public class LogDataObject
    {
        public string Time { get; set; }
        public LogType LogType { get; set; }
        public string ModuleName { get; set; }
        public string SubModuleName { get; set; }
        public string Message { get; set; }

    }

}
