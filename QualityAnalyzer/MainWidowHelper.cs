using Log.Common;
using System;
using System.Reflection;
using System.Threading;
using Utilities;

namespace QualityAnalyzer
{
    public partial class MainWindow
    {
        private void ResetForm()
        {
            chooseFileTabItem.IsSelected = true;
            _inputDate = null;
            resultDataGrid.ItemsSource = null;
            inputDataGrid.ItemsSource = null;
            _progressThread = null;
            messageLable.Reset();
        }
        #region [ProgressBar]
        protected void ShowProgress()
        {
            try
            {
                if (_progressThread != null)
                    return;
                _progressThread = new Thread(() =>
                {
                    while (true)
                    {
                        for (int i = 1; i < 15; i++)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                progressBarControl.Value = i;
                                Thread.Sleep(10);
                            });
                        }
                    }
                });

                _progressThread.SetApartmentState(ApartmentState.STA);
                _progressThread.Start();

            }
            catch (Exception exc)
            {
                _logger.Info(LogType.Error, this.GetType().Name, MethodBase.GetCurrentMethod().Name, exc.GetMessage());
            }
        }
        protected void HideProgress()
        {
            if (_progressThread == null)
                return;
            _progressThread.Abort();
            _progressThread = null;
        }
        #endregion
    }
}
