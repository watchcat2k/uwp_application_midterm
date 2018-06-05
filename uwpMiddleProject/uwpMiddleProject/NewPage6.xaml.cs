using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class NewPage6 : Page
    {
        public NewPage6()
        {
            this.InitializeComponent();
        }

        //该变量用来判断是否做出了选择
        private bool hasSelected = false;
        //该变量用来判断是否已确定了答案
        private bool hasConfirm = false;
        //保存选择的选项，A B C D
        private string myAnswer = "";
        //用来接收上一page的参数
        Models.AnswersModel temp = null;

        //跳转页面后，接收参数
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            temp = (Models.AnswersModel)e.Parameter;
        }

        //以下是为了实现单选效果
        private void answerAClick(object sender, RoutedEventArgs e)
        {
            //已确定答案就不能修改
            if (hasConfirm)
            {
                maintainSelected();
                return;
            }

            if (answerA.IsChecked == true)
            {
                hasSelected = true;
                answerB.IsChecked = false;
                answerC.IsChecked = false;
                answerD.IsChecked = false;
                myAnswer = "A";
            }
            else
            {
                myAnswer = "";
                hasSelected = false;
            }
        }

        //答案设为B
        private void answerBClick(object sender, RoutedEventArgs e)
        {
            //已确定答案就不能修改
            if (hasConfirm)
            {
                maintainSelected();
                return;
            }

            if (answerB.IsChecked == true)
            {
                hasSelected = true;
                answerA.IsChecked = false;
                answerC.IsChecked = false;
                answerD.IsChecked = false;
                myAnswer = "B";
            }
            else
            {
                hasSelected = false;
                myAnswer = "";
            }
        }

        private void answerCClick(object sender, RoutedEventArgs e)
        {
            //已确定答案就不能修改
            if (hasConfirm)
            {
                maintainSelected();
                return;
            }

            if (answerC.IsChecked == true)
            {
                hasSelected = true;
                answerA.IsChecked = false;
                answerB.IsChecked = false;
                answerD.IsChecked = false;
                myAnswer = "C";
            }
            else
            {
                hasSelected = false;
                myAnswer = "";
            }
        }

        private void answerDClick(object sender, RoutedEventArgs e)
        {
            //已确定答案就不能修改
            if (hasConfirm)
            {
                maintainSelected();
                return;
            }

            if (answerD.IsChecked == true)
            {
                hasSelected = true;
                answerA.IsChecked = false;
                answerB.IsChecked = false;
                answerC.IsChecked = false;
                myAnswer = "D";
            }
            else
            {        
                hasSelected = false;
                myAnswer = "";
            }
        }

        //确保答案不再修改
        private void maintainSelected()
        {
            if (myAnswer == "A")
            {
                answerA.IsChecked = true;
                answerB.IsChecked = false;
                answerC.IsChecked = false;
                answerD.IsChecked = false;
            }
            else if (myAnswer == "B")
            {
                answerA.IsChecked = false;
                answerB.IsChecked = true;
                answerC.IsChecked = false;
                answerD.IsChecked = false;
            }
            else if (myAnswer == "C")
            {
                answerA.IsChecked = false;
                answerB.IsChecked = false;
                answerC.IsChecked = true;
                answerD.IsChecked = false;
            }
            else if (myAnswer == "D")
            {
                answerA.IsChecked = false;
                answerB.IsChecked = false;
                answerC.IsChecked = false;
                answerD.IsChecked = true;
            }
        }

        //设正确答案为B,在这里确定答案是否正确
        private void confirmButtonClick(object sender, RoutedEventArgs e)
        {
            //如果没有确定答案，不跳转下一题
            if (hasConfirm == false)
            {
                //如果没有做出选择，不反应
                if (hasSelected == false) return;

                if (answerB.IsChecked == true)
                {
                    trueAnswer.Visibility = Visibility.Visible;
                }
                else
                {
                    falseAnswer.Visibility = Visibility.Visible;
                    falseAnswer.Text = "正确答案是：B";
                }
                hasConfirm = true;
                if (App.num == 10)
                {
                    confirm.Content = "完成";
                }
                else
                {
                    confirm.Content = "下一题";
                }
            }
            else
            {
                temp.answerTo6 = myAnswer;
                if (myAnswer == "B")
                {
                    temp.score += 10;
                }
                //Frame.Navigate(typeof(NewPage7), temp);
                

                if (App.num == 10)
                {
                    Frame.Navigate(typeof(ResultPage), temp);
                    return;
                }

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

        }
    }
}
