using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CopyForReview.Formatters;
using Microsoft.VisualStudio.PlatformUI;

namespace CopyForReview
{
    /// <summary>
    /// An image button for formatters.
    /// </summary>
    public class FormatterImageButton: ImageButton
    {

        public FormatterImageButton(IFormatter formatter): base()
    {

        using (MemoryStream memory = new MemoryStream())
        {
            formatter.IconImage.Save(memory, ImageFormat.Png);
            memory.Position = 0;
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = memory;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();
            ImageNormal =  bitmapImage;
        }

            this.Content = formatter.Name;
      

        MinWidth = 300;
        MinHeight = 300;
            MaxHeight = 600;
            MaxWidth = 600;
        HorizontalContentAlignment = HorizontalAlignment.Stretch;
        VerticalContentAlignment = VerticalAlignment.Stretch;
        Content = formatter.Name;
        ToolTip = formatter.Description;
        //n.Name = formatter.ToString(); //use the class name TODO later change and probably use some sort of GUID here
        //kPanelFormatters.Children.Add(btn);
            FormatterName = formatter.Name;
    }


        public string FormatterName { get; set; }
    }
}
