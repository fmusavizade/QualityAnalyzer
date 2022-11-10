
namespace Log.Common
{
    internal class GlobalVariable
    {
        public const string LogDefaultPath = "D:\\";

        #region FileName
        public const string DebugFileName = @"D:/Logs/${shortdate}/${date:format=HH}.txt";
        #endregion

        #region BaseDirFileName
        public const string DebugBaseDirFileName = @"${basedir}/Logs/${shortdate}/${date:format=HH}.txt";
        #endregion
    }
}
