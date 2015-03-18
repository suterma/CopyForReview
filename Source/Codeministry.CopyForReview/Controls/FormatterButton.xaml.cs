// 
//     Copy for review, code sharing made simple.
//     Copyright (C) 2015 by marcel suter, marcel@codeministry.ch
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Codeministry.CopyForReview.Formatters;

namespace Codeministry.CopyForReview.Controls
{
    /// <summary>
    ///     A Button for invoking a formatter.
    /// </summary>
    public partial class FormatterButton : UserControl
    {
        public IFormatter Formatter { get; private set; }

        /// <summary>
        ///     Occurs when the buttons is clicked.
        /// </summary>
        public event RoutedEventHandler Click;

        /// <summary>
        ///     Initializes a new instance of the <see cref="FormatterButton" /> class.
        /// </summary>
        /// <remarks>Used just for the designer.</remarks>
        public FormatterButton() {}

        /// <summary>
        ///     Initializes a new instance of the <see cref="FormatterButton" /> class.
        /// </summary>
        /// <param name="formatter">The formatter.</param>
        public FormatterButton(IFormatter formatter)
        {
            Formatter = formatter;

            InitializeComponent();

            EmbeddedButton.Click += EmbeddedButton_Click;

            using (MemoryStream memory = new MemoryStream()) {
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
            ToolTip = formatter.Description;
            //n.Name = formatter.ToString(); //use the class name TODO later change and probably use some sort of GUID here
        }

        /// <summary>
        ///     Handles the Click event of the FormatterButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void EmbeddedButton_Click(object sender, RoutedEventArgs e)
        {
            if (Click != null) {
                Click(this, e);
            }
        }

        /// <summary>
        ///     Checks the RadioButton.
        /// </summary>
        internal void CheckRadioButton()
        {
            EmbeddedButton.IsChecked = true;
        }
    }
}