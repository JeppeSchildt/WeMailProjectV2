﻿#pragma checksum "..\..\Create.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "DB148727D2DE2CB01219D2D8A38F4E95ACCF15A6C68585FA4C7F40041C394C97"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Email;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace Email {
    
    
    /// <summary>
    /// Create
    /// </summary>
    public partial class Create : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\Create.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CUserName;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\Create.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CPassword;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\Create.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Tlf;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\Create.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Done;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\Create.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Label1;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\Create.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Code;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Email;component/create.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Create.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.CUserName = ((System.Windows.Controls.TextBox)(target));
            
            #line 14 "..\..\Create.xaml"
            this.CUserName.GotFocus += new System.Windows.RoutedEventHandler(this.CUserName_GotFocus);
            
            #line default
            #line hidden
            
            #line 14 "..\..\Create.xaml"
            this.CUserName.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Text_changed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.CPassword = ((System.Windows.Controls.TextBox)(target));
            
            #line 15 "..\..\Create.xaml"
            this.CPassword.GotFocus += new System.Windows.RoutedEventHandler(this.CPassword_GotFocus);
            
            #line default
            #line hidden
            
            #line 15 "..\..\Create.xaml"
            this.CPassword.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Text_changed);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Tlf = ((System.Windows.Controls.TextBox)(target));
            
            #line 16 "..\..\Create.xaml"
            this.Tlf.GotFocus += new System.Windows.RoutedEventHandler(this.Tlf_GotFocus);
            
            #line default
            #line hidden
            
            #line 16 "..\..\Create.xaml"
            this.Tlf.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Text_changed);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Done = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\Create.xaml"
            this.Done.Click += new System.Windows.RoutedEventHandler(this.Done_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Label1 = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.Code = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

