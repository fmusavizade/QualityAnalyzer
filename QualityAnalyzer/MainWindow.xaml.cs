using Log.Base;
using Log.Common;
using Log.NLogLogger;
using QualityAnalyzer.Model;
using QualityAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Utilities;

namespace QualityAnalyzer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BaseLogger _logger;
        int _itemPerPage = 15;
        List<AnalysisData> _inputDate = new List<AnalysisData>();
        private PagingCollectionView _inputCollectionView;
        private PagingCollectionView _resultCollectionView;
        Thread _progressThread;
        #region [Constractor]
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
            _logger = new Logger();
        }

        #endregion

        #region [Data Manipulation]
        private void SetInputData(string fileContect)
        {
            _inputDate = fileContect.JsonStringToObject<List<AnalysisData>>();
            _inputCollectionView = new PagingCollectionView(_inputDate, _itemPerPage);
            inputLable.Content = _inputCollectionView.GetPaginationLableCotent();
            inputDataGrid.ItemsSource = _inputCollectionView;
            inputTotalRowsLable.Content = $"Total Records : {_inputDate.Count()}";
            viewInputTabItem.IsSelected = true;
        }
        private void ProcessFile(string filePath)
        {
            try
            {
                var fileContect = System.IO.File.ReadAllText(filePath);
                if (string.IsNullOrWhiteSpace(fileContect))
                {
                    messageLable.ShowMessage(string.Format(MessageResource.InvalidContent, filePath));
                    return;
                }

                SetInputData(fileContect);

            }
            catch (Exception exc)
            {
                messageLable.ShowMessage(string.Format(MessageResource.InvalidContent, filePath));
                _logger.Info(LogType.Error, this.GetType().Name, MethodBase.GetCurrentMethod().Name, exc.GetMessage());
            }
        }
        #endregion

        #region [Action Events]

        private void chooseFileDropDownBorder_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ResetForm();
            var openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                ProcessFile(openFileDialog.FileName);
        }
        private void Border_Drop(object sender, DragEventArgs e)
        {
            ResetForm();
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (files != null && files.Where(x => GlobalVariables.ValidFileExtentions.Contains(System.IO.Path.GetExtension(x).ToLower())).Any())
                ProcessFile(files.First());
            else
                messageLable.ShowMessage(string.Format(MessageResource.InvalidFileExtention, string.Join(", ", GlobalVariables.ValidFileExtentions)));
        }
        private void ProcessButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProgressBarTabItem.IsSelected = true;
                ShowProgress();
                var result = new List<AnalyzeResult>();
                Thread calculator = new Thread(() =>
                {
                    try
                    {
                        result = _inputDate.GroupBy(x => x.Axis).Select(g => new { key = g.Key, value = g.OrderBy(o => o.Measurement).Select(v => v.Value).ToList() }).Select(item => new AnalyzeResult()
                        {
                            AxisLable = item.key,
                            Variance = item.value.CalculateVariance(),
                            TrendStatus = item.value.DetectValueChangeTrend(),
                            Outliers = string.Join(", ", item.value.CalculateOutLiers()),

                        }).ToList();
                        //Thread.Sleep(10000);
                    }

                    finally
                    {
                        if (result != null)
                            this.Dispatcher.Invoke(() =>
                            {
                                _resultCollectionView = new PagingCollectionView(result, _itemPerPage);
                                resultDataGrid.ItemsSource = _resultCollectionView;
                                resltLable.Content = this._resultCollectionView.GetPaginationLableCotent();
                                HideProgress();
                                viewResultTabItem.IsSelected = true;
                            });
                    }
                });

                calculator.SetApartmentState(ApartmentState.STA);
                calculator.Start();

            }
            catch (Exception exc)
            {
                messageLable.ShowMessage(string.Format(MessageResource.InvalidContent, ""));
                _logger.Info(LogType.Error, this.GetType().Name, MethodBase.GetCurrentMethod().Name, exc.GetMessage());
            }
        }
        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            _inputCollectionView.MoveToPreviousPage();
            inputLable.Content = _inputCollectionView.GetPaginationLableCotent();
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            _inputCollectionView.MoveToNextPage();
            inputLable.Content = _inputCollectionView.GetPaginationLableCotent();
        }
        private void ResultPreviousBtn_Click(object sender, RoutedEventArgs e)
        {
            _resultCollectionView.MoveToPreviousPage();
            resltLable.Content = _resultCollectionView.GetPaginationLableCotent();
        }
        private void ResultNextBtn_Click(object sender, RoutedEventArgs e)
        {
            _resultCollectionView.MoveToNextPage();
            resltLable.Content = _resultCollectionView.GetPaginationLableCotent();
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e) =>
            ResetForm();
        private void Start_Click(object sender, RoutedEventArgs e) =>
            chooseFileTabItem.IsSelected = true;
        #endregion

        #region [Simple Window Events]
        private void PART_Resiize_Click(object sender, RoutedEventArgs e) =>
                 this.WindowState = this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        private void PART_Minimize_Click(object sender, RoutedEventArgs e) =>
                this.WindowState = this.WindowState == WindowState.Minimized ? WindowState.Maximized : WindowState.Minimized;
        private void PART_CLOSE_Click(object sender, RoutedEventArgs e) =>
            this.Close();

        #endregion

    }
}
