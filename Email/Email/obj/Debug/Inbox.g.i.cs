<<<<<<< Updated upstream
﻿#pragma checksum "..\..\Inbox.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4ACAC819A2472FBD3345014E7BDEA07B639AD4B9B155FAF4F5603D30E9D75F9A"
=======
﻿#pragma checksum "..\..\Inbox.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8AF159623658AF6162133DEEF518245FFA1AE8509410E9594DD682F18CA7FBE8"
>>>>>>> Stashed changes
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


namespace CLIENT {
    
    
    /// <summary>
    /// Inbox
    /// </summary>
    public partial class Inbox : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\Inbox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SentFolder;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\Inbox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SendMail;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\Inbox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button MarkFunc;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\Inbox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Label1;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\Inbox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Label2;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\Inbox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Label3;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\Inbox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button delete;
        
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
            System.Uri resourceLocater = new System.Uri("/Email;component/inbox.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Inbox.xaml"
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
            this.SentFolder = ((System.Windows.Controls.Button)(target));
            
            #line 10 "..\..\Inbox.xaml"
            this.SentFolder.Click += new System.Windows.RoutedEventHandler(this.SentFolder_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.SendMail = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\Inbox.xaml"
            this.SendMail.Click += new System.Windows.RoutedEventHandler(this.NewEmail_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.MarkFunc = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\Inbox.xaml"
            this.MarkFunc.Click += new System.Windows.RoutedEventHandler(this.Mark_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Label1 = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.Label2 = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.Label3 = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.delete = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\Inbox.xaml"
            this.delete.Click += new System.Windows.RoutedEventHandler(this.delete_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

