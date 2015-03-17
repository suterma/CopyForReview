using System.Linq;
using System.Windows;
using CopyForReview.Data;
using CopyForReview.Formatters;
using EnvDTE;
using Microsoft.VisualStudio.PlatformUI;

namespace CopyForReview.Controls
{
    // Use this constructor to enable F1 Help. 
    public partial class FormatSelector : DialogWindow
    {
        // Use this constructor to provide a Help button and F1 support. 
        public FormatSelector(string helpTopic)
            : base(helpTopic)
        {
            InitializeComponent();
            InitializeFormatterButtons();

        }

        /// <summary>
        /// Gets the selected formatter.
        /// </summary>
        /// <value>
        /// The selected formatter.
        /// </value>
        public IFormatter SelectedFormatter { get; private set; }

        /// <summary>
        /// Initializes the formatter buttons.
        /// </summary>
        private void InitializeFormatterButtons()
        {
            //Initialize Formatter Buttons
            var formatters = Factory.GetFormatters();
            foreach (var formatter in formatters)
            {
                var button = new FormatterButton(formatter);
                button.Click += ButtonReviewInFoswiki_Click;
                StackPanelFormatters.Children.Add(button);
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="FormatSelector"/> class.
        /// </summary>
        public FormatSelector()
        {

            InitializeComponent();
            InitializeFormatterButtons();
        }

        private void ButtonReviewInFoswiki_Click(object sender, RoutedEventArgs e)
        {
            //find the formatter in question and invoke it
            var button = sender as FormatterButton;
            SelectedFormatter = Factory.GetFormatters().Single(item => item.Name == button.FormatterName); //TODO later use better matching, possibly using a GUID
            DialogResult = true;
            this.Close();
        }
    }
}