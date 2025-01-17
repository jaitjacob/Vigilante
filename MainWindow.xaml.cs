using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Vigilante
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        // Keep track of the current page
        private Frame ContentFrame { get; set; }
        public MainWindow()
        {
            this.InitializeComponent();

            // Initialize the frame
            ContentFrame = new Frame();
            MainContent.Children.Add(ContentFrame);

            // Navigate to default page
            ContentFrame.Navigate(typeof(QuickScanPage));

            // Set up SelectorBar event handler
            SelectorBar.SelectionChanged += SelectorBar_SelectionChanged;
        }

        private void SelectorBar_SelectionChanged(object sender, SelectorBarSelectionChangedEventArgs args)
        {
            if (sender is SelectorBar selectorBar)
            {
                var selectedItem = selectorBar.SelectedItem as SelectorBarItem;

                if (selectedItem != null)
                {
                    Type pageType = selectedItem.Name switch
                    {
                        "SelectorBarItemRecent" => typeof(QuickScanPage),
                        "SelectorBarItemShared" => typeof(FileScanPage),
                        "SelectorBarItemFavorites" => typeof(WebScanPage),
                        _ => typeof(QuickScanPage)
                    };

                    ContentFrame.Navigate(pageType);
                }
            }
        }

    }

   


}