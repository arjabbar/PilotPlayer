﻿#pragma checksum "..\..\..\EditSlideshow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8CC70A80890A681BA4260350480967CC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PilotPlayer;
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


namespace PilotPlayer {
    
    
    /// <summary>
    /// EditSlideshow
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class EditSlideshow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\EditSlideshow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDelete;
        
        #line default
        #line hidden
        
        
        #line 7 "..\..\..\EditSlideshow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\EditSlideshow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition Item;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\EditSlideshow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ColumnDefinition Select;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\EditSlideshow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ColumnDefinition Name;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\EditSlideshow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ColumnDefinition Type;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\EditSlideshow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ColumnDefinition Start;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\EditSlideshow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ColumnDefinition End;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\EditSlideshow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image pageBack;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\EditSlideshow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image pageForward;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PilotPlayer;component/editslideshow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\EditSlideshow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 4 "..\..\..\EditSlideshow.xaml"
            ((PilotPlayer.EditSlideshow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnDelete = ((System.Windows.Controls.Button)(target));
            
            #line 6 "..\..\..\EditSlideshow.xaml"
            this.btnDelete.Click += new System.Windows.RoutedEventHandler(this.btnDelete_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.grid = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.Item = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 5:
            this.Select = ((System.Windows.Controls.ColumnDefinition)(target));
            return;
            case 6:
            this.Name = ((System.Windows.Controls.ColumnDefinition)(target));
            return;
            case 7:
            this.Type = ((System.Windows.Controls.ColumnDefinition)(target));
            return;
            case 8:
            this.Start = ((System.Windows.Controls.ColumnDefinition)(target));
            return;
            case 9:
            this.End = ((System.Windows.Controls.ColumnDefinition)(target));
            return;
            case 10:
            this.pageBack = ((System.Windows.Controls.Image)(target));
            
            #line 19 "..\..\..\EditSlideshow.xaml"
            this.pageBack.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.pageBack_MouseDown);
            
            #line default
            #line hidden
            return;
            case 11:
            this.pageForward = ((System.Windows.Controls.Image)(target));
            
            #line 20 "..\..\..\EditSlideshow.xaml"
            this.pageForward.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.pageForward_MouseDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

