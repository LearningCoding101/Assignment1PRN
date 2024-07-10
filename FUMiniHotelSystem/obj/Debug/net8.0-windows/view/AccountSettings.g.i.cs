﻿#pragma checksum "..\..\..\..\view\AccountSettings.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A8AF1E44743193C68CD429E42882E089BA0D57C2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FUMiniHotelSystem.model;
using FUMiniHotelSystem.view;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace FUMiniHotelSystem.view {
    
    
    /// <summary>
    /// AccountSettings
    /// </summary>
    public partial class AccountSettings : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 60 "..\..\..\..\view\AccountSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CustomerFullname;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\..\view\AccountSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Telephone;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\..\view\AccountSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EmailAddress;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\view\AccountSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker CustomerBirthday;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/FUMiniHotelSystem;V1.0.0.0;component/view/accountsettings.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\view\AccountSettings.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 49 "..\..\..\..\view\AccountSettings.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ViewReservationButton_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 50 "..\..\..\..\view\AccountSettings.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SettingButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CustomerFullname = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.Telephone = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.EmailAddress = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.CustomerBirthday = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 7:
            
            #line 76 "..\..\..\..\view\AccountSettings.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveChangesButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

