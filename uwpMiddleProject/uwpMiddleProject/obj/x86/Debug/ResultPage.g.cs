﻿#pragma checksum "C:\Users\Shinelon\Desktop\期中项目\uwpMiddleProject\uwpMiddleProject\ResultPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "48DC08AE1213222A7EC3C6065B028448"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace uwpMiddleProject
{
    partial class ResultPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.MySplitView = (global::Windows.UI.Xaml.Controls.SplitView)(target);
                }
                break;
            case 2:
                {
                    this.sorceText = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 3:
                {
                    this.MyImage = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 4:
                {
                    this.selectPicture = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 50 "..\..\..\ResultPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.selectPicture).Click += this.selectPictureClick;
                    #line default
                }
                break;
            case 5:
                {
                    this.saveButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 51 "..\..\..\ResultPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.saveButton).Click += this.saveButtonClick;
                    #line default
                }
                break;
            case 6:
                {
                    this.shareButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 52 "..\..\..\ResultPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.shareButton).Click += this.shareButtonClick;
                    #line default
                }
                break;
            case 7:
                {
                    this.returnHome = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 53 "..\..\..\ResultPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.returnHome).Click += this.returnHomeClick;
                    #line default
                }
                break;
            case 8:
                {
                    this.IconsListBox = (global::Windows.UI.Xaml.Controls.ListBox)(target);
                    #line 25 "..\..\..\ResultPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.ListBox)this.IconsListBox).SelectionChanged += this.IconsListBox_SelectionChanged;
                    #line default
                }
                break;
            case 9:
                {
                    this.HomeListBoxItem = (global::Windows.UI.Xaml.Controls.ListBoxItem)(target);
                }
                break;
            case 10:
                {
                    this.RecordListBoxItem = (global::Windows.UI.Xaml.Controls.ListBoxItem)(target);
                }
                break;
            case 11:
                {
                    this.ShareListBoxItem = (global::Windows.UI.Xaml.Controls.ListBoxItem)(target);
                }
                break;
            case 12:
                {
                    this.HamburgerButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 21 "..\..\..\ResultPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.HamburgerButton).Click += this.HamburgerButtonClick;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
