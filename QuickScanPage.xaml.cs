using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Security.Cryptography;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Vigilante
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class QuickScanPage : Page
    {
        public QuickScanPage()
        {
            this.InitializeComponent();
        }

        private void Grid_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;                    
            e.DragUIOverride.Caption = "Scan file hash."; // Sets custom UI text
            e.DragUIOverride.IsCaptionVisible = true; // Sets if the caption is visible
            e.DragUIOverride.IsContentVisible = true; // Sets if the dragged content is visible
            e.DragUIOverride.IsGlyphVisible = true; // Sets if the glyph is visibile
        }

        private async void Grid_Drop(object sender, DragEventArgs e)
        {
            string hashString = string.Empty;
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();
                if (items.Any())
                {
                    var storageFile = items[0] as StorageFile;
                    
                    string path = storageFile.Path;
                    FileInfo targetFile = new FileInfo(path);

                    using(FileStream fs  = targetFile.Open(FileMode.Open))
                    {
                        byte[] hash = MD5.Create().ComputeHash(fs);
                        hashString = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                        
                    }


                    if (storageFile != null)
                    {
                        DropArea.Text = "File dropped: " + storageFile.Name + "and its SHA256:" + hashString;
                    }
                }
            }
        }


    }
}
