using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace QualityAnalyzer
{
    public static class FormHandler
    {
        public static void ShowMessage(this Label lable, string message)
        {
            lable.Content = message;
            lable.Visibility = Visibility.Visible;
        }
        public static void Reset(this Label lable)
        {
            lable.Content = "";
            lable.Visibility = Visibility.Collapsed;
        }
        public static string GetPaginationLableCotent(this PagingCollectionView collectionViewItem)
           => $" {collectionViewItem.CurrentPage} of {collectionViewItem.PageCount} ";
    }
}
