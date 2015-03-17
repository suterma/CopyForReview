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

namespace CopyForReview
{
    /// <summary>
    /// A Button for invoking a formatter.
    /// </summary>
    public partial class FormatterButton : UserControl
    {
        private readonly IFormatter _formatter;

        /// <summary>
        /// Occurs when the buttons is clicked.
        /// </summary>
        public event RoutedEventHandler Click;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormatterButton"/> class.
        /// </summary>
        /// <param name="formatter">The formatter.</param>
        public FormatterButton(IFormatter formatter)
        {
            _formatter = formatter;

            InitializeComponent();

            EmbeddedButton.Click += EmbeddedButton_Click;

            using (MemoryStream memory = new MemoryStream())
            {
                formatter.IconImage.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                IconImage.Source = bitmapImage;
            }

            Caption.Text = formatter.Name;

            //MinWidth = 300;
            //MinHeight = 300;
            //    MaxHeight = 600;
            //    MaxWidth = 600;
            //HorizontalContentAlignment = HorizontalAlignment.Stretch;
            //VerticalContentAlignment = VerticalAlignment.Stretch;
            //Content = formatter.Name;
            ToolTip = formatter.Description;
            //n.Name = formatter.ToString(); //use the class name TODO later change and probably use some sort of GUID here
            //kPanelFormatters.Children.Add(btn);
        }

        /// <summary>
        /// Handles the Click event of the FormatterButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        void EmbeddedButton_Click(object sender, RoutedEventArgs e)
        {
            if (Click != null)
            {
                Click(this, e);
            }
        }

        /// <summary>
        /// Gets the name of the formatter.
        /// </summary>
        /// <value>
        /// The name of the formatter.
        /// </value>
        public string FormatterName { get { return _formatter.Name; } }
    }
}
