using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace uwpMiddleProject
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    /// 
    public class dateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTimeOffset date = (DateTimeOffset)value;
            return date.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public sealed partial class RecordPage : Page
    {
        public RecordPage()
        {
            this.InitializeComponent();
            this.ViewModel = ViewModels.AnswersViewModels.GetInstance();
            //分享功能要用到的图片
            Uri imageUri = new Uri("ms-appx:///Assets/cup.png");
            this.ImageStreamRef = RandomAccessStreamReference.CreateFromUri(imageUri);

        }

        //分享功能用到的图片
        public RandomAccessStreamReference ImageStreamRef;
        //获得viewmodel单例
        private ViewModels.AnswersViewModels ViewModel;

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ViewModel.SelectedRecord = null;
        }

        private void IconsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HomeListBoxItem.IsSelected)
            {
                Frame.Navigate(typeof(MainPage));
            }
            else if (RecordListBoxItem.IsSelected)
            {
                Frame.Navigate(typeof(RecordPage));
            }
            else if (ShareListBoxItem.IsSelected)
            {
                DataTransferManager.ShowShareUI();
                DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
                dataTransferManager.DataRequested += DataTransferManager_DataRequested;
            }
            else if (DeleteListBoxItem.IsSelected)
            {
                ViewModel.RemoveRecord();
                DeleteListBoxItem.Visibility = Visibility.Collapsed;
                RecordListBoxItem.IsSelected = true;
            }
        }

        private void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;

            if (ViewModel.SelectedRecord == null)
            {
                request.Data.SetText("分享此应用");
                request.Data.Properties.Title = "答题模拟器";
                request.Data.Properties.Description = "分享此应用";
                request.Data.SetBitmap(ImageStreamRef);
            }
            else
            {
                request.Data.SetText("你的成绩：" + ViewModel.SelectedRecord.score);
                request.Data.Properties.Title = "答题模拟器";
                request.Data.Properties.Description = "你的成绩：" + ViewModel.SelectedRecord.score;
                request.Data.SetBitmap(ImageStreamRef);
            }
        }

        private void HamburgerButtonClick(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ViewModel.SelectedRecord = (Models.AnswersModel)(e.ClickedItem);
            DeleteListBoxItem.Visibility = Visibility.Visible;
        }


    }
}
