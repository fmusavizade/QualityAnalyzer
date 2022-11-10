using System;

namespace Utilities
{
    public static class ExceptionExtentionMethods
    {
        public static string GetMessage(this Exception exp)
        {
            string innerMessage = string.Empty;
            if (exp.InnerException != null)
                innerMessage = exp.InnerException.Message;
            return "Exception Message: " + exp.Message + "\r\n Inner Message: " + innerMessage;
        }
    }
}
