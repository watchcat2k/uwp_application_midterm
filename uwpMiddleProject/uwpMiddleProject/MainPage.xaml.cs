using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace uwpMiddleProject
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.ViewModel = ViewModels.AnswersViewModels.GetInstance();
            //分享功能要用到的图片
            Uri imageUri = new Uri("ms-appx:///Assets/cup.png");
            this.ImageStreamRef = RandomAccessStreamReference.CreateFromUri(imageUri);
            //磁帖创建函数
            tileCreate();
            //天气查询
            WeatherQuery();
        }

        //分享功能用到的图片
        public RandomAccessStreamReference ImageStreamRef;
        //获得viewmodel单例
        private ViewModels.AnswersViewModels ViewModel;

        private void startButtonClick(object sender, RoutedEventArgs e)
        {
            App.num = 0;
            for (int i = 0; i < 10; i++)
            {
                App.order[i] = i + 1;
            }
            Random rd = new Random();
            for (int i = 0; i < 10; i++)
            {
                int index_ = rd.Next(0, 10);
                if (i != index_)
                {
                    int a = App.order[i];
                    App.order[i] = App.order[index_];
                    App.order[index_] = a;
                }
            }


            Models.AnswersModel temp = new Models.AnswersModel();
            //Frame.Navigate(typeof(NewPage1), temp);
            int index = App.order[App.num];
            App.num++;
           
            if (index == 1)
            {
                Frame.Navigate(typeof(NewPage1), temp);
            }
            else if (index == 2)
            {
                Frame.Navigate(typeof(NewPage2), temp);
            }
            else if (index == 3)
            {
                Frame.Navigate(typeof(NewPage3), temp);
            }
            else if (index == 4)
            {
                Frame.Navigate(typeof(NewPage4), temp);
            }
            else if (index == 5)
            {
                Frame.Navigate(typeof(NewPage5), temp);
            }
            else if (index == 6)
            {
                Frame.Navigate(typeof(NewPage6), temp);
            }
            else if (index == 7)
            {
                Frame.Navigate(typeof(NewPage7), temp);
            }
            else if (index == 8)
            {
                Frame.Navigate(typeof(NewPage8), temp);
            }
            else if (index == 9)
            {
                Frame.Navigate(typeof(NewPage9), temp);
            }
            else
            {
                Frame.Navigate(typeof(NewPage10), temp);
            }

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

        private void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;

            request.Data.SetText("分享此应用");
            request.Data.Properties.Title = "答题模拟器";
            request.Data.Properties.Description = "分享此应用";
            request.Data.SetBitmap(ImageStreamRef);
        }

        private void HamburgerButtonClick(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
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

        private void weatherQueryClick(object sender, RoutedEventArgs e)
        {
            WeatherQuery();
        }
        //查询天气
        async void WeatherQuery()
        {
            if (cityName.Text == "") return;

            string url = "http://api.avatardata.cn/Weather/Query?key=1a2fa02c4cf34aafbb7ff83904a6ac6e&cityname=" + cityName.Text + "&dtype=XML";
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(url);
            Windows.Data.Xml.Dom.XmlDocument document = new Windows.Data.Xml.Dom.XmlDocument();
            document.LoadXml(response);
            Windows.Data.Xml.Dom.XmlNodeList list = document.GetElementsByTagName("temperature");
            if(list.Length == 0)
            {
                var ii = new MessageDialog("The city is not exit!").ShowAsync();
            }
            else
            {
                IXmlNode node = list.Item(0);
                string i = node.InnerText;
                if (i != "")
                {
                    list = document.GetElementsByTagName("info");
                    node = list.Item(0);
                    weatherText.Text = "天气: " + node.InnerText;

                    list = document.GetElementsByTagName("temperature");
                    node = list.Item(0);
                    weatherText.Text += "   温度: " + node.InnerText + "摄氏度";
                }
                else
                {
                    var ii = new MessageDialog("Error!").ShowAsync();
                }
            }
        }

    }
}
