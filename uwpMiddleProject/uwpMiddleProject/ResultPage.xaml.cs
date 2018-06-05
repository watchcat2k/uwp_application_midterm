using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace uwpMiddleProject
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ResultPage : Page
    {
        public ResultPage()
        {
            this.InitializeComponent();
            this.ViewModel = ViewModels.AnswersViewModels.GetInstance();
            //分享功能要用到的图片
            Uri imageUri = new Uri("ms-appx:///Assets/cup.png");
            this.ImageStreamRef = RandomAccessStreamReference.CreateFromUri(imageUri);
        }

        //用来接收上一page的参数
        Models.AnswersModel temp = null;
        //获得viewmodel单例
        private ViewModels.AnswersViewModels ViewModel;
        //分享功能用到的图片
        public RandomAccessStreamReference ImageStreamRef;
        //是否已经点击了保存
        private bool hasSave = false;
        //保存选择的图片
        private StorageFile imageFile = null;

        //跳转页面后，接收参数
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            temp = (Models.AnswersModel)e.Parameter;
            sorceText.Text = "你的成绩：" + temp.score;
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
        }

        private void HamburgerButtonClick(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private async void saveButtonClick(object sender, RoutedEventArgs e)
        {
            if (hasSave == false)
            {
                if (imageFile != null)
                {
                    string imageName = imageFile.Name;
                    StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                    StorageFile newImageFile = await imageFile.CopyAsync(localFolder, imageName, NameCollisionOption.ReplaceExisting);
                    temp.imauri = new Uri(newImageFile.Path);
                }
 
                temp.coverImage = MyImage.Source as BitmapImage;
                ViewModel.AddRecord(temp);
                tileCreate();
                saveButton.Content = "查看记录";
                hasSave = true;
            }
            else
            {
                Frame.Navigate(typeof(RecordPage));
            }
        }

        private void shareButtonClick(object sender, RoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += DataTransferManager_DataRequested;
        }

        private void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;

            request.Data.SetText("你的成绩为：" + temp.score);
            request.Data.Properties.Title = "答题模拟器";
            request.Data.Properties.Description = "你的成绩为：" + temp.score; 
            request.Data.SetBitmap(ImageStreamRef);
        }

        private void returnHomeClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void selectPictureClick(object sender, RoutedEventArgs e)
        {
            var srcImage = new BitmapImage();
            FileOpenPicker openPicker = new FileOpenPicker();
            //选择视图模式  
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            //初始位置  
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            //添加文件类型  
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            var file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                imageFile = file;
                using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
                {
                    await srcImage.SetSourceAsync(stream);
                    MyImage.Source = srcImage;
                }
            }
        }

        //磁铁创建函数
        private void tileCreate()
        {
            Windows.Data.Xml.Dom.XmlDocument document = new Windows.Data.Xml.Dom.XmlDocument();
            document.LoadXml(System.IO.File.ReadAllText("XMLFile1.xml"));
            Windows.Data.Xml.Dom.XmlNodeList Texttitle = document.GetElementsByTagName("text");

            TileUpdateManager.CreateTileUpdaterForApplication().Clear();
            TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueue(true);
            for (int i = 0; i < ViewModel.AllRecords.Count; i++)
            {
                if (i < 5)
                {
                    Texttitle[0].InnerText = Texttitle[2].InnerText = Texttitle[4].InnerText = ViewModel.AllRecords[i].score.ToString();
                    Texttitle[1].InnerText = Texttitle[3].InnerText = Texttitle[5].InnerText = ViewModel.AllRecords[i].date.ToString();
                    TileNotification newTile = new TileNotification(document);
                    TileUpdateManager.CreateTileUpdaterForApplication().Update(newTile);
                }
            }
        }

    }
}
